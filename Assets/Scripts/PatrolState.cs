using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

/// <summary>
/// This state script allows AI to patrol to random path points set in the map.
/// </summary>
public class PatrolState : StateMachineBehaviour
{
    private float timer;
    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange = 8;
    
    // list to store "path points" for AI navigation
    [SerializeField] private List<Transform> pathPoints = new List<Transform>();
    [SerializeField] private NavMeshAgent agent;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // find player and access transform for position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // grab navmesh component on object
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 1.5f;
        timer = 0;
        
        // find parent object holding all the path points 
        var points = GameObject.FindGameObjectWithTag("PathPoints");
        
        // grab all the path point child objects
        foreach (Transform transform in points.transform)
        {
            // add child object transforms to list
            pathPoints.Add(transform);
        }

        // move AI to path point ss
        agent.SetDestination(pathPoints[Random.Range(0, pathPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // move AI to path point ss
            agent.SetDestination(pathPoints[Random.Range(0, pathPoints.Count)].position);
        }
        
        // after 10 seconds change to idle state
        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("isPatrolling", false);
        }
        
        // each frame calculate distance of AI from player
        var distance = Vector3.Distance(player.position, animator.transform.position);

        // if player within range, then AI chases
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // stop AI after exiting patrol state
        agent.SetDestination(agent.transform.position);
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
