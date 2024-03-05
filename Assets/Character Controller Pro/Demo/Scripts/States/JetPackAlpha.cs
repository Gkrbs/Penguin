﻿using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Implementation;

namespace Lightbug.CharacterControllerPro.Demo
{


    [AddComponentMenu("Character Controller Pro/Demo/Character/States/Jet Pack")]
    public class JetPackAlpha : CharacterState
    {
        [Header("Planar movement")]

        [SerializeField]
        protected float targetPlanarSpeed = 5f;

        [Header("Planar movement")]

        [SerializeField]
        protected float targetVerticalSpeed = 5f;

        [SerializeField]
        protected float duration = 1f;

        protected bool _do_jetpack = false;

        [SerializeField]
        protected int delayTime = 3000;

        // ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        // ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────


        protected Vector3 smoothDampVelocity = Vector3.zero;


        public override string GetInfo()
        {
            return "This state allows the character to imitate a \"JetPack\" type of movement. Basically the character can ascend towards the up direction, " +
            "but also move in the local XZ plane.";
        }

        public override void EnterBehaviour(float dt, CharacterState fromState)
        {
            if (_do_jetpack) return;
            CharacterActor.alwaysNotGrounded = true;
            CharacterActor.UseRootMotion = false;

            smoothDampVelocity = CharacterActor.VerticalVelocity;

        }

        public override void ExitBehaviour(float dt, CharacterState toState)
        {
            CharacterActor.alwaysNotGrounded = false;
        }

        public override void UpdateBehaviour(float dt)
        {
            if (!_do_jetpack) return;
            // Vertical movement
            CharacterActor.VerticalVelocity = Vector3.SmoothDamp(CharacterActor.VerticalVelocity, targetVerticalSpeed * CharacterActor.Up, ref smoothDampVelocity, duration);

            // Planar movement
            CharacterActor.PlanarVelocity = Vector3.Lerp(CharacterActor.PlanarVelocity, targetPlanarSpeed * CharacterStateController.InputMovementReference, 7f * dt);

            // Looking direction
            CharacterActor.SetYaw(CharacterActor.PlanarVelocity);
        }

        public override void CheckExitTransition()
        {
            if (CharacterActor.Triggers.Count != 0)
            {
                if (CharacterActions.interact.Started)
                    CharacterStateController.EnqueueTransition<LadderClimbing>();
            }
            else if (!_do_jetpack&& CharacterActions.jetPack.value)
            {
                _do_jetpack = true;

                EndJetPack();
            }

        }

        async void EndJetPack()
        {
            await System.Threading.Tasks.Task.Delay(delayTime);
            CharacterStateController.EnqueueTransition<NormalMovement>();
            _do_jetpack = false;
        }

    }

}