using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.Player.Movement;
using Twosies.Player.Items;
using Twosies.Interactables;

namespace Twosies.States.Player
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

        [HideInInspector] public PlayerInput input;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public SpriteRenderer spriter;
        [HideInInspector] public Animator animator;

        public delegate void inputDelegate();
        public static event inputDelegate OnJump;
        public static event inputDelegate OnUseItem;
        public static event inputDelegate OnUnUseItem;

        [HideInInspector] public List<Collider2D> grounders = new();

        [HideInInspector] public List<IJumpModifier> jumpModifiers = new();

        public ItemBase currentItem;

        [HideInInspector] public bool facingRight;
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
            spriter = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            input.actions.FindAction("Jump").started += ((InputAction.CallbackContext c) => OnJump?.Invoke());
            input.actions.FindAction("Use Item").started += ((InputAction.CallbackContext c) => OnUseItem?.Invoke());
            input.actions.FindAction("Use Item").canceled += ((InputAction.CallbackContext c) => OnUnUseItem?.Invoke());

            currentItem = new Parachute(this);
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

            spriter.flipX = facingRight;
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

