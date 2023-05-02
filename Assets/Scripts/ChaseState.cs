using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This state script allows AI to detect player within a certain range and chase them.
/// </summary>
public class ChaseState : StateMachineBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 3.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // move enemy towards player
        agent.SetDestination(player.position);
        
        // each frame calculate distance of AI from player
        var distance = Vector3.Distance(player.position, animator.transform.position);

        // if player is too far away from AI, transition to idle state
        if (distance > 15)
        {
            animator.SetBool("isChasing", false);
        }
        // if player is super close to AI, transition to attack state
        if (distance < 2.5f)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // stops AI when exiting chase state
        agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
