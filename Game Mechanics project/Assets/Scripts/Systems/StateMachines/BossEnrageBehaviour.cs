using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnrageBehaviour : StateMachineBehaviour
{
    private DamageController damageController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damageController = animator.GetComponent<DamageController>();
        damageController.InvincibilityTime = 1.5f;
        damageController.IsInvincible = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damageController.IsInvincible = false;
        damageController.InvincibilityTime = 0.25f;
    }
}
