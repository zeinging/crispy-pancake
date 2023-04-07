using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubeSoundBehaviour : StateMachineBehaviour
{
     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        if(stateInfo.IsName("Glube_Combo-1")){
            //Debug.Log("Combo 1: " + stateInfo.length);
            
        }

        if(stateInfo.IsName("Glube_Combo-2")){
            //Debug.Log("Combo 2");
        }

        if(stateInfo.IsName("Glube_Combo-3")){
            //Debug.Log("Combo 3");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if(stateInfo.length < 1.2f){
            //Debug.Log("this state is " + animator.);
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
