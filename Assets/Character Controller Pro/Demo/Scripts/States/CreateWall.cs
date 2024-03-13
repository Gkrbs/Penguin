using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Implementation;

namespace Lightbug.CharacterControllerPro.Demo
{
    [AddComponentMenu("Character Controller Pro/Demo/Character/States/Create Wall")]
    public class CreateWall : CharacterState
    {
        [Header("Planar movement")]

        [SerializeField]
        protected float shoot_force = 5f;

        [SerializeField]
        protected float shoot_rot_force = 5f;

        protected bool _do_create_wall = false;

        [SerializeField]
        protected Transform _shoot_pos_tr;

        [SerializeField]
        protected GameObject[] _wall_prepebs;
        protected GameObject _current_wall;

        protected bool _is_active = false;
        // ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        // ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

        public override string GetInfo()
        {
            return "This state allows the character to imitate a \"JetPack\" type of movement. Basically the character can ascend towards the up direction, " +
            "but also move in the local XZ plane.";
        }

        public override void EnterBehaviour(float dt, CharacterState fromState)
        {
            if (_is_active) return;
            _current_wall = _wall_prepebs[Random.Range(0, _wall_prepebs.Length)];
        }

        public override void ExitBehaviour(float dt, CharacterState toState)
        {
            _current_wall = null;
            _is_active = false;
        }

        public override void UpdateBehaviour(float dt)
        {
            if (_is_active) return;
            if (_current_wall != null)
            {
                _is_active = true;
                GameObject wall = Instantiate<GameObject>(_current_wall, _shoot_pos_tr.position, _shoot_pos_tr.rotation);
                wall.SetActive(true);
            }

        }

        public override void CheckExitTransition()
        {
            CharacterStateController.EnqueueTransition<NormalMovement>();
        }

    }

}
