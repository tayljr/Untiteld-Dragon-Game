using UnityEngine;

public class IdleTimeout : StateMachineBehaviour
{
    public float timeoutDuration = 5f; // Duration of inactivity before triggering the event
    public float timer = 0f;

    public bool isIdle = false; 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isIdle = true;

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isIdle)
        {
            timer += Time.deltaTime;
            animator.SetLayerWeight(layerIndex, Mathf.Lerp(1f,0f,timer/timeoutDuration));
            if (timer >= timeoutDuration)
            {
                // Trigger your event here
                timer = 0f;
                isIdle = false; // Reset the idle state
                animator.SetTrigger("IdleTimeout");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        isIdle = false;
        animator.SetLayerWeight(layerIndex, 1f);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
