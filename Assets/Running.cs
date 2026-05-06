using UnityEngine;
using UnityEngine.AI;

public class Running : StateMachineBehaviour
{
    private NavMeshAgent nav;
    private float lastDirectionChangeTime;

    [Header("Movement")]
    public float directionChangeTime = 5f;
    public float roamDistance = 15f;

    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        // bezpieczeństwo – reset stanu
        animator.SetInteger("nextState", 0);

        nav = animator.GetComponent<NavMeshAgent>();
        if (nav == null) return;

        nav.ResetPath();
        nav.isStopped = false;

        SetRandomDestination(animator);
        lastDirectionChangeTime = Time.time;
    }

    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        if (nav == null) return;

        // 👉 JEŚLI GRACZ BLISKO – PRZEJDŹ DO FOLLOWING
        if (animator.GetBool("isNearPlayer"))
        {
            animator.SetInteger("nextState", 4); // FOLLOWING
            return;
        }

        // 👉 LOSOWA ZMIANA KIERUNKU
        if (Time.time - lastDirectionChangeTime > directionChangeTime)
        {
            SetRandomDestination(animator);
            lastDirectionChangeTime = Time.time;
        }
    }

    private void SetRandomDestination(Animator animator)
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamDistance;
        randomDirection += animator.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamDistance, NavMesh.AllAreas))
        {
            nav.SetDestination(hit.position);
        }
    }
}
