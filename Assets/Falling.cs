using UnityEngine;
using UnityEngine.AI;

public class falling : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<NavMeshAgent>().ResetPath();
    }
}
