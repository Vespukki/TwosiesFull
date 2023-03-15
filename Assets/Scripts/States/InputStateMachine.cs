using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.Player.Movement;
using Twosies.Physics;
using Twosies.Interactable;
using Twosies.Player;
using Twosies.Interactable.Possessable;

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

        [SerializeField] Transform groundedBoxPoint;
        [SerializeField] Vector2 groundedBoxSize;
        [SerializeField] Vector2 interactBoxSize;

        [SerializeField] ContactFilter2D groundContactFilter;
        [SerializeField] ContactFilter2D interactContactFilter;

        [HideInInspector] internal List<Collider2D> grounders = new();

        [HideInInspector] internal List<IJumpModifier> jumpModifiers = new();

        [HideInInspector] public bool facingRight;
        internal PlayerAttacher AttachedTo => GetAttacher();

        internal InteractableBase targetedInteractable;

        private bool groundable = true;
        private bool canInteract = true;

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
            if(soul != null)
            {
                OnSoulEnter(soul);
            }
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

        public virtual void OnSoulExit()
        {
            ChangeState(null);
        }

        private void OnDrawGizmos()
        {
            if(groundedBoxPoint != null && groundedBoxSize.x != 0 && groundedBoxSize.y != 0)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawWireCube(groundedBoxPoint.position, groundedBoxSize);
            }
            if(interactBoxSize.x != 0 && interactBoxSize.y != 0)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position, interactBoxSize);
            }
        }
    }
}
