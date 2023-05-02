using UnityEngine;

/// <summary>
/// This state script allows AI to remain ide on the map.
/// </summary>
public class IdleState : StateMachineBehaviour
{
    private float timer;
    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange = 8;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        
        // find player and access transform for position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // after 5 seconds change to patrol state
        timer += Time.deltaTime;
        if (timer > 5)
        {
            animator.SetBool("isPatrolling", true);
            
            // each frame calculate distance of AI from player
            var distance = Vector3.Distance(player.position, animator.transform.position);

            // if player within range, then AI chases
            if (distance < chaseRange)
            {
                animator.SetBool("isChasing", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
