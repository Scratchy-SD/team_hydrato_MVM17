﻿using UnityEngine;
using UnityEngine.InputSystem;

internal class PS_Jump : AbstractUpdatingState
{
    public override void Init(PlayerAnimation _a, PlayerMovement _m, PlayerStateMachine _s)
    {
        base.Init(_a, _m, _s);

        name = "Jumping";
        clip = Resources.Load<AnimationClip>("AnimationClips/Player/Horn/Jump");
    }

    public override void OnStateEnter(PIA actions)
    {
        TurnAdjust(actions.World.Horizontal.ReadValue<float>());
        movement.AddJump();
        base.OnStateEnter(actions);
        anim.PlayAnimation(clip, false);
        movement.falling += Fall;
        actions.World.Horizontal.performed += TurnAdjust;

    }


    public override void OnStateExit(PIA actions)
    {
        base.OnStateExit(actions);
        movement.falling -= Fall;
        actions.World.Horizontal.performed -= TurnAdjust;
    }

    protected override void OnFixedUpdate()
    {

        movement.SolveAerialVelocity();

    }

    protected override void OnUpdate()
    {
    }

    private void TurnAdjust(float obj)
    {
        movement.TurnToPosX(obj > 0);
    }
    private void TurnAdjust(InputAction.CallbackContext obj)
    {
        movement.TurnToPosX(obj.ReadValue<float>() > 0);
    }
    private void Fall()
    {
        OnExit?.Invoke(State.Falling);
    }
}

