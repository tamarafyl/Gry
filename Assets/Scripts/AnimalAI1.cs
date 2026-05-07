using UnityEngine;
using UnityEngine.AI;

public class AnimalAI1 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    [Header("Налаштування руху")]
    public float wanderRadius = 15f;
    public float wanderSpeed = 2f;
    public float chaseSpeed = 5.5f;
    public float attackDistance = 2.5f;

    private float wanderTimer = 5f;
    private float timer;
    private bool isAttacking = false;
    
    // Змінна для відстеження поточної анімації
    private string currentAnimation; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        timer = wanderTimer;
        PlayAnimation("Idle");
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh) return;

        // Перевірка на зупинку для Idle
        if (agent.velocity.magnitude < 0.1f && !isAttacking)
        {
            PlayAnimation("Idle");
        }

        // Перевірка існування GameManager перед зверненням
        if (GameManager.instance == null) return;

        bool isDay = GameManager.instance.isDay;

        if (isDay)
        {
            HandleWandering();
        }
        else
        {
            // HandleNightBehavior() закоментований нижче, тому вночі тварина просто стоятиме
            // HandleNightBehavior(); 
        }
    }

    // ЛОГІКА ДНЯ: Блукання
    void HandleWandering()
    {
        agent.speed = wanderSpeed;
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            PlayAnimation("Walk");
            timer = 0;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            PlayAnimation("Idle");
        }
    }

    /* // ПЕРЕСЛІДУВАННЯ ТА АТАКА ЗАКОМЕНТОВАНІ
    void HandleNightBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (GameManager.instance.isTorchBurning)
        {
            if (distanceToPlayer < 7f)
            {
                FleeFromPlayer();
            }
        }
        else
        {
            if (distanceToPlayer > attackDistance)
            {
                isAttacking = false;
                agent.isStopped = false;
                agent.speed = chaseSpeed;
                agent.SetDestination(player.position);
                PlayAnimation("Run");
            }
            else if (!isAttacking)
            {
                StartAttack();
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        agent.isStopped = true;
        PlayAnimation("Attack1");
    }
    */

    void FleeFromPlayer()
    {
        agent.speed = chaseSpeed;
        Vector3 runDirection = (transform.position - player.position).normalized;
        Vector3 runTo = transform.position + runDirection * 5f;
        agent.SetDestination(runTo);
        PlayAnimation("Run");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Колізія також закоментована, щоб тварина не вбивала гравця при випадковому торканні
        /*
        if (collision.gameObject.CompareTag("Player") && !GameManager.instance.isDay)
        {
            if (GameManager.instance.isTorchBurning)
            {
                FleeFromPlayer();
            }
            else
            {
                GameManager.instance.PlayerDeath(); 
            }
        }
        */
    }

    void PlayAnimation(string triggerName)
    {
        if (currentAnimation == triggerName) return;

        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack1");

        animator.SetTrigger(triggerName);
        currentAnimation = triggerName;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}