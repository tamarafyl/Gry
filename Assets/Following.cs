using UnityEngine;
using UnityEngine.AI;

public class Following : StateMachineBehaviour
{
    private NavMeshAgent _nav;
    private Transform _player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _nav = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _nav.SetDestination(_player.position);
    }
}
