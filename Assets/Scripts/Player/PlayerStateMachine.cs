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

        [SerializeField] ContactFilter2D groundContactFilter;

        [SerializeField] private bool jumpFlagTemp;

        private PlayerInput input;
        private Rigidbody2D body;

        public delegate void inputDelegate();
        public static event inputDelegate OnJump;

        [HideInInspector] public List<Collider2D> grounders = new();

        public PlayerAttacher AttachedTo => GetAttacher();

        private PlayerAttacher GetAttacher()
        {
            if (transform.parent == null) return null;

            if(transform.parent.TryGetComponent<PlayerAttacher>(out PlayerAttacher attacher))
            {
                return attacher;
            }
            return null;
        }


        protected override void Awake()
        {
            base.Awake();

            input = GetComponent<PlayerInput>();
            body = GetComponent<Rigidbody2D>();

            input.actions.FindAction("Jump").started += ((InputAction.CallbackContext c) => OnJump?.Invoke());
        }

        protected override void Start()
        {
            base.Start();
            ChangeState(new PlayerWalkingState(this));
        }

        protected override void Update()
        {
            base.Update();

            GroundedCheck();

            if (jumpFlagTemp)
            {
                jumpFlagTemp = false;
                ChangeState(new PlayerJumpingState(this));
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        private void GroundedCheck()
        {
            Collider2D[] tempContacts = new Collider2D[20];

            Physics2D.OverlapBox(groundedBoxPoint.position, groundedBoxSize, 0, groundContactFilter, tempContacts);

            List<Collider2D> contacts = new();

            
            foreach (var coll in tempContacts)
            {
                if (coll == null) continue;

                //if layer mask contains this layer and if body is touching this collider
                if ((groundContactFilter.layerMask == (groundContactFilter.layerMask | (1 << coll.gameObject.layer))) && body.IsTouching(coll))
                {
                    contacts.Add(coll);
                }
            }

            grounded = (contacts.Count > 0);
            grounders = contacts;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(groundedBoxPoint.position, groundedBoxSize);
        }
    }
}

