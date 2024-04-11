using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.Player.Movement;
using Twosies.Physics;
using Twosies.Interactable;
using Twosies.Player;

namespace Twosies.States
{
    public abstract class InputStateMachine : StateMachine
    {
        public delegate void inputDelegate();
        public static event inputDelegate OnJump;
        public static event inputDelegate OnInteract;

        [HideInInspector] public PlayerInput input;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public SpriteRenderer spriter;
        [HideInInspector] public Animator animator;

        public PlayerStats stats;
        [HideInInspector] public bool grounded;
        [HideInInspector] public bool leftWalled;
        [HideInInspector] public bool rightWalled;


        [SerializeField] private Vector2 _groundedBoxCenter;
        [SerializeField] private Vector2 _leftWallBoxCenter;
        [SerializeField] private Vector2 _rightWallBoxCenter;
        Vector2 groundedBoxPoint { get { return (Vector2)transform.position + _groundedBoxCenter; } }
        Vector2 leftWallBoxPoint { get { return (Vector2)transform.position + _leftWallBoxCenter; } }
        Vector2 rightWallBoxPoint { get { return (Vector2)transform.position + _rightWallBoxCenter; } }


        [SerializeField] Vector2 groundedBoxSize;
        [SerializeField] Vector2 leftWallBoxSize;
        [SerializeField] Vector2 rightWallBoxSize;
        [SerializeField] Vector2 interactBoxSize;

        [SerializeField] ContactFilter2D groundContactFilter;
        [SerializeField] ContactFilter2D interactContactFilter;

        [HideInInspector] internal List<Collider2D> grounders = new();
        [HideInInspector] internal List<Collider2D> leftWallers = new();
        [HideInInspector] internal List<Collider2D> rightWallers = new();

        [HideInInspector] internal List<IJumpModifier> jumpModifiers = new();

        [HideInInspector] public bool facingRight;
        internal PlayerAttacher AttachedTo => GetAttacher();

        internal InteractableBase targetedInteractable;

        [System.NonSerialized] public bool groundable = true;
        [System.NonSerialized] public bool canInteract = false;

        public PlayerSoul soul;

        protected override void Awake()
        {
            base.Awake(); 
            
            body = GetComponent<Rigidbody2D>();
            spriter = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private PlayerAttacher GetAttacher()
        {
            if (transform.parent == null) return null;

            if (transform.parent.TryGetComponent<PlayerAttacher>(out PlayerAttacher attacher))
            {
                return attacher;
            }
            return null;
        }

        protected override void Update()
        {
            base.Update();

            if(groundable)
            {
                GroundedCheck();
            }

            WallCheck();

            spriter.flipX = facingRight;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (canInteract)
            {
                {
                    ChangeInteractable();
                }
            }

            if(soul == null)
            {
                SoullessFixedUpdate();
            }
        }

        internal InteractableBase GetInteractables()
        {
            List<InteractableBase> interactables = new();
            foreach (var coll in GetContactsFromBox(transform.position, interactBoxSize, interactContactFilter))
            {
                if (coll.TryGetComponent(out InteractableBase inter))
                {
                    interactables.Add(inter);
                }
            }


            InteractableBase closest = null;
            float closestDist = 10000;

            foreach (var interactable in interactables)
            {
                if (!interactable.CanHighlight()) continue;

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
                if (targetedInteractable != null)
                {
                    targetedInteractable.SetHighlight(false);
                }
                if (newInteractable != null)
                {
                    newInteractable.SetHighlight(true);
                }
                targetedInteractable = newInteractable;
            }
        }

        protected override void Start()
        {
            base.Start();
            SetGravity();
            if(soul != null)
            {
                OnSoulEnter(soul);
            }
        }

        private void SetGravity()
        {
            body.gravityScale = (2 * stats.JumpHeight) / (stats.JumpTime * stats.JumpTime);
        }

        private void GroundedCheck()
        {
            grounders = GetContactsFromBox(groundedBoxPoint, groundedBoxSize, groundContactFilter);
            grounded = (grounders.Count > 0);
        }

        private void WallCheck()
        {
            leftWallers = GetContactsFromBox(leftWallBoxPoint, leftWallBoxSize, groundContactFilter);
            leftWalled = (leftWallers.Count > 0);

            rightWallers = GetContactsFromBox(rightWallBoxPoint, rightWallBoxSize, groundContactFilter);
            rightWalled = (rightWallers.Count > 0);
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

        public virtual void OnSoulEnter(PlayerSoul newSoul)
        {
            soul = newSoul;

            if (soul != null)
            {
                input = soul.GetComponent<PlayerInput>();
                input.actions.FindAction("Jump").started += ((InputAction.CallbackContext c) => OnJump?.Invoke());
                input.actions.FindAction("Interact").started += ((InputAction.CallbackContext c) => OnInteract?.Invoke());
            }
        }
        /// <summary>
        /// called every fixed frame when soul == null;
        /// </summary>
        public virtual void SoullessFixedUpdate()
        {

        }

        public virtual void OnSoulExit()
        {
            ChangeState(null);

            if(groundable && grounded)
            {
                body.velocity *= Vector2.up;
            }
        }

        private void OnDrawGizmos()
        {
            if(groundedBoxSize.x != 0 && groundedBoxSize.y != 0)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawWireCube(groundedBoxPoint, groundedBoxSize);
            }
            if(leftWallBoxSize.x != 0 && leftWallBoxSize.y != 0)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawWireCube(leftWallBoxPoint, leftWallBoxSize);
            }
            if(rightWallBoxSize.x != 0 && rightWallBoxSize.y != 0)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawWireCube(rightWallBoxPoint, rightWallBoxSize);
            }
            if(interactBoxSize.x != 0 && interactBoxSize.y != 0)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position, interactBoxSize);
            }
        }
    }
}
