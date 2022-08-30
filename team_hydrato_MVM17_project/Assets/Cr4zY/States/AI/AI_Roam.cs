using System;
using UnityEngine;

public class AI_Roam : AbstractAIState
{
    public override void Init(AINavigator _n, AIStateMachine _s, AIDetector _d, AIHurtBehaviour _h, CustomAnimationController _a)
    {
        name = "Roam";
        base.Init(_n, _s, _d, _h, _a);
    }
    public override void OnStateEnter()
    {
        nav.StartWalk();
        nav.onRest += Rest;
        anim.PlayAnimation(clip, true);
    }

    private void Rest()
    {
        onExit?.Invoke(AIState.IDLE);
    }

    public override void OnStateExit()
    {
        nav.onRest -= Rest;
    }
}
