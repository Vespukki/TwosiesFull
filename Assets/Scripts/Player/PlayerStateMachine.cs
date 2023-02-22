using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace States
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStats stats;
        [HideInInspector] public bool grounded;

        [SerializeField] Transform groundedBoxPoint;
        [SerializeField] Vector2 groundedBoxSize;
        [SerializeField] LayerMask groundLayer;

        private PlayerInput input;

        public delegate void inputDelegate();
        public static event inputDelegate OnJump;
        protected override void Start()
        {
            input = GetComponent<PlayerInput>();

            base.Start();
            ChangeState(new PlayerWalkingState(this));

            input.actions.FindAction("Jump").started += ((InputAction.CallbackContext c) => OnJump?.Invoke());
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            grounded = (Physics2D.OverlapBox(groundedBoxPoint.position, groundedBoxSize, 0, groundLayer));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(groundedBoxPoint.position, groundedBoxSize);
        }
    }
}