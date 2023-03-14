using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.Player.Movement;
using Twosies.Physics;
using Twosies.Interactable;

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
        [SerializeField] Vector2 interactBoxSize;


        [SerializeField] ContactFilter2D groundContactFilter;
        [SerializeField] ContactFilter2D interactContactFilter;

        [SerializeField] private bool jumpFlagTemp;

        [HideInInspector] public PlayerInput input;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public SpriteRenderer spriter;
        [HideInInspector] public Animator animator;

        public delegate void inputDelegate();
        public static event inputDelegate OnJump;
        public static event inputDelegate OnInteract;

        [HideInInspector] internal List<Collider2D> grounders = new();

        [HideInInspector] internal List<IJumpModifier> jumpModifiers = new();

        [HideInInspector] public bool facingRight;
        internal PlayerAttacher AttachedTo => GetAttacher();

        internal InteractableBase targetedInteractable;
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
            input.actions.FindAction("Interact").started += ((InputAction.CallbackContext c) => OnInteract?.Invoke());
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

            ChangeInteractable();
        }

        private void GroundedCheck()
        {
            grounders = GetContactsFromBox(groundedBoxPoint.position, groundedBoxSize, groundContactFilter);

            grounded = (grounders.Count > 0);
        }

        private List<Collider2D> GetContactsFromBox(Vector2 position, Vector2 boxSize, ContactFilter2D contactFilter)
        {
            Collider2D[] tempContacts = new Collider2D[20];

            Physics2D.OverlapBox(position, boxSize, 0, contactFilter, tempContacts);

            List<Collider2D> contacts = new();


            foreach (var coll in tempContacts)
            {
                if (coll == null) continue;

                //if layer mask contains this layer
                if (contactFilter.layerMask == (contactFilter.layerMask | (1 << coll.gameObject.layer)))
                {
                    contacts.Add(coll);
                }
            }

            return contacts;
        }

        internal InteractableBase GetInteractables()
        {
            List<InteractableBase> interactables = new();
            foreach(var coll in GetContactsFromBox(transform.position, interactBoxSize, interactContactFilter))
            {
                if(coll.TryGetComponent(out InteractableBase inter))
                {
                    interactables.Add(inter);
                }
            }


            InteractableBase closest = null;
            float closestDist = 10000;

            foreach(var interactable in interactables)
            {
                float tempDist = Vector2.Distance(transform.position, interactable.transform.position);
                if (tempDist < closestDist)
                {
                    closest = interactable;
                    closestDist = tempDist;
                }
            }


            return closest;
        }

        private void ChangeInteractable()
        {
            InteractableBase newInteractable = GetInteractables();
            if (targetedInteractable != newInteractable)
            {
                if(targetedInteractable != null)
                {
                    targetedInteractable.SetHighlight(false);
                }
                if(newInteractable != null)
                {
                    newInteractable.SetHighlight(true);
                }
                targetedInteractable = newInteractable;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(groundedBoxPoint.position, groundedBoxSize);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, interactBoxSize);
        }

       
    }
}

