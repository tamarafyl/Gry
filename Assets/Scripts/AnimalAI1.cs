using System.Collections; // Потрібно для роботи корутин (IEnumerator)
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AnimalAI1 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform playerTransform;

    [Header("Ustawienia ruchu (Dzień)")]
    public float walkSpeed = 2f;
    public float patrolRadius = 15f;
    public float minIdlingTime = 2f;
    public float maxIdlingTime = 6f;

    [Header("Ustawienia ataku (Noc)")]
    public float runSpeed = 5f;
    public int maxSimultaneousAttackers = 2; 

    private enum StateNight { Idling, Chasing, Scared, GameOverTriggered }
    private StateNight currentNightState = StateNight.Idling;

    private float idleTimer;
    private bool isIdling = false;
    
    private float scaredTimer = 0f; 
    private bool amIAttacking = false; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        ChooseNewPatrolPoint();
    }

    void Update()
    {
        if (GameManager.instance == null) return;

        // ПРИМУСОВО ЩОКАДРУ: Повністю вимикаємо обхід інших агентів (і вдень, і вночі)
        if (agent != null && agent.obstacleAvoidanceType != ObstacleAvoidanceType.NoObstacleAvoidance)
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }

        // Zarządzanie animacją Speed z плавним згладжуванням тремтіння
        if (animator != null)
        {
            float realSpeed = agent.velocity.magnitude;

            if (isIdling || realSpeed < 0.2f)
            {
                float currentParam = animator.GetFloat("Speed");
                animator.SetFloat("Speed", Mathf.MoveTowards(currentParam, 0f, Time.deltaTime * 5f));
            }
            else if (GameManager.instance.isDay)
            {
                animator.SetFloat("Speed", 2f); // Walk
            }
            else
            {
                animator.SetFloat("Speed", 5f); // Run
            }
        }

        // Podział logiki Dzień/Noc
        if (GameManager.instance.isDay)
        {
            if (amIAttacking) StopAttacking();
            currentNightState = StateNight.Idling;
            
            LogicDay();
        }
        else
        {
            // Якщо вже запустився процес смерті, зупиняємо виконання нічної логіки переслідування
            if (currentNightState != StateNight.GameOverTriggered)
            {
                LogicNight();
            }
        }
    }

    // --- LOGIKA DNIA ---
    void LogicDay()
    {
        agent.speed = walkSpeed;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isIdling)
            {
                isIdling = true;
                idleTimer = Random.Range(minIdlingTime, maxIdlingTime);
            }
        }

        if (isIdling)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                isIdling = false;
                ChooseNewPatrolPoint();
            }
        }
    }

    void ChooseNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    // --- LOGIKA NOCY ---
    void LogicNight()
    {
        if (playerTransform == null) return;
        agent.speed = runSpeed;

        if (currentNightState == StateNight.Scared)
        {
            scaredTimer -= Time.deltaTime;
            
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                RunAwayFromPlayer(); 
            }

            if (scaredTimer <= 0f)
            {
                currentNightState = StateNight.Idling; 
            }
            return; 
        }

        if (currentNightState != StateNight.Chasing)
        {
            if (GameManager.instance.currentAttackersCount < maxSimultaneousAttackers)
            {
                StartAttacking();
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    Vector3 randomNearPlayer = playerTransform.position + Random.insideUnitSphere * 12f;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomNearPlayer, out hit, 12f, NavMesh.AllAreas))
                    {
                        agent.SetDestination(hit.position);
                    }
                }
            }
        }

        if (currentNightState == StateNight.Chasing)
        {
            isIdling = false;
            agent.SetDestination(playerTransform.position);
        }
    }

    void StartAttacking()
    {
        currentNightState = StateNight.Chasing;
        if (!amIAttacking)
        {
            amIAttacking = true;
            GameManager.instance.currentAttackersCount++;
        }
    }

    void StopAttacking()
    {
        currentNightState = StateNight.Idling;
        if (amIAttacking)
        {
            amIAttacking = false;
            GameManager.instance.currentAttackersCount = Mathf.Max(0, GameManager.instance.currentAttackersCount - 1);
        }
    }

    void RunAwayFromPlayer()
    {
        Vector3 directionToPlayer = transform.position - playerTransform.position;
        Vector3 runDestination = transform.position + directionToPlayer.normalized * patrolRadius;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(runDestination, out hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    // --- KOLIZJA (Is Trigger) ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null && GameManager.instance.isDay) return;

            // Якщо ми вже в стані смерті, ігноруємо нові дотики від інших кабанів, щоб не запускати корутину кілька разів поспіль
            if (currentNightState == StateNight.GameOverTriggered) return;

            if (GameManager.instance != null && !GameManager.instance.isDay)
            {
                if (GameManager.instance.isTorchBurning)
                {
                    Debug.Log("Kolizja w nocy: Pochodnia płonie! Odstraszenie na 25s.");
                    
                    StopAttacking(); 
                    currentNightState = StateNight.Scared;
                    scaredTimer = Random.Range(20f, 30f); 
                    
                    RunAwayFromPlayer(); 
                }
                else
                {
                    Debug.Log("Kolizja w nocy: Brak pochodni! Start sekwencji śmierci.");
                    // Замість миттєвого переходу, запускаємо корутину з паузою
                    StartCoroutine(KillPlayerSequence());
                }
            }
        }
    }

    // ДОДАНО: Корутина, яка робить паузу перед завантаженням екрана програшу
   private IEnumerator KillPlayerSequence()
    {
        currentNightState = StateNight.GameOverTriggered;
        
        StopAttacking(); // Звільняємо чергу атак для інших тварин
        
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true; // Зупиняємо кабана, щоб він не біг крізь гравця під час атаки
        }

        // АКТИВУЄМО АНІМАЦІЮ АТАКИ
        if (animator != null)
        {
            animator.SetTrigger("Attack1"); // Вмикаємо тригер Attack1, який видно на вашому скріншоті
        }

        // Чекаємо 2 секунди (поки програється анімація удару та гравець падає)
        yield return new WaitForSeconds(2f);

        // Переходимо на сцену смерті
        SceneManager.LoadScene("Scene_GameOver");
    }
}