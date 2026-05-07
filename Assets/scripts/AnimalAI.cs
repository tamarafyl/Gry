using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AnimalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform gracz;

    [Header("Ustawienia dystansu")]
    public float dystansAtaku = 2.5f; 
    private bool jestMartwy = false;
    private bool atakuje = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        GameObject obiektGracza = GameObject.FindGameObjectWithTag("Player");
        if (obiektGracza != null) gracz = obiektGracza.transform;

        animator.SetTrigger("Walk");
    }

    void Update()
    {
        if (jestMartwy || gracz == null) return;

        float dystansDoGracza = Vector3.Distance(transform.position, gracz.position);

        if (dystansDoGracza > dystansAtaku)
        {
            atakuje = false;
            agent.isStopped = false;
            agent.SetDestination(gracz.position);
            
            animator.SetTrigger("Run");
        }
        else
        {
            if (!atakuje)
            {
                RozpocznijAtak();
            }
        }
    }

    void RozpocznijAtak()
    {
        atakuje = true;
        agent.isStopped = true; 
        
        animator.SetTrigger("Attack1");

        Invoke("ZabijGracza", 0.5f); 
    }

    void ZabijGracza()
    {
        if (!jestMartwy) 
        {
            SceneManager.LoadScene("Scene_GameOver");
        }
    }

    public void OtrzymajObrazenia()
    {
        if (jestMartwy) return;

        jestMartwy = true;
        agent.isStopped = true;
        
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack1");

        int typSmierci = Random.Range(1, 3);
        animator.SetTrigger("Death" + typSmierci);

        Destroy(gameObject, 5f);
        Debug.Log("Bestia została pokonana!");
    }
}