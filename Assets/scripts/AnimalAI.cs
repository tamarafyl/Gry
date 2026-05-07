using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AnimalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform gracz;

    [Header("Ustawienia dystansu")]
    public float dystansAtaku = 2.5f; // Odległość, przy której zwierzę zaczyna atakować
    
    private bool jestMartwy = false;
    private bool atakuje = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        // Szukamy gracza po tagu (upewnij się, że gracz ma tag "Player")
        GameObject obiektGracza = GameObject.FindGameObjectWithTag("Player");
        if (obiektGracza != null) gracz = obiektGracza.transform;

        // Rozpoczynamy od animacji chodzenia
        animator.SetTrigger("Walk");
    }

    void Update()
    {
        if (jestMartwy || gracz == null) return;

        float dystansDoGracza = Vector3.Distance(transform.position, gracz.position);

        if (dystansDoGracza > dystansAtaku)
        {
            // POŚCIG
            atakuje = false;
            agent.isStopped = false;
            agent.SetDestination(gracz.position);
            
            animator.SetTrigger("Run");
        }
        else
        {
            // ATAK
            if (!atakuje)
            {
                RozpocznijAtak();
            }
        }
    }

    void RozpocznijAtak()
    {
        atakuje = true;
        agent.isStopped = true; // Zatrzymujemy się, aby ugryźć
        
        animator.SetTrigger("Attack1");

        // Wywołujemy śmierć gracza po krótkim opóźnieniu (czas na animację ataku)
        Invoke("ZabijGracza", 0.5f); 
    }

    void ZabijGracza()
    {
        if (!jestMartwy) 
        {
            // Ładujemy scenę GameOver
            SceneManager.LoadScene("Scene_GameOver");
        }
    }

    // Ta metoda jest wywoływana przez skrypt strzelania gracza
    public void OtrzymajObrazenia()
    {
        if (jestMartwy) return;

        jestMartwy = true;
        agent.isStopped = true;
        
        // Resetujemy wszystkie pozostałe stany
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack1");

        // Losowa śmierć (Death1 lub Death2)
        int typSmierci = Random.Range(1, 3);
        animator.SetTrigger("Death" + typSmierci);

        // Usuwamy ciało po 5 sekundach
        Destroy(gameObject, 5f);
        Debug.Log("Bestia została pokonana!");
    }
}