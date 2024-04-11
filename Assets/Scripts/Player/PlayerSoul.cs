using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.States;
using Twosies.Utils;

namespace Twosies.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerSoul : MonoBehaviour
    {
        private SpriteRenderer spriter;
        private TrailRenderer trail;
        [HideInInspector] public PlayerInput input;
        public static PlayerSoul instance;
        [SerializeField] private float soulTransferTime;

        //private Vector2 initialSoulPosition;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(this.gameObject);
            }

            input = GetComponent<PlayerInput>();
            spriter = GetComponent<SpriteRenderer>();
            trail = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            //float t = Vector2.Distance(transform.position, transform.parent.position) / Vector2.Distance(transform.position, transform.parent.position);
            Vector2 newPos = Vector2.MoveTowards(transform.position, transform.parent.position, soulTransferTime);

            if (newPos == (Vector2)transform.parent.position)
            {
                SetVisual(false);
            }
            else
            {
                transform.position = newPos;
            }
        }

        private void SetVisual(bool value)
        {
            spriter.enabled = value;
            trail.emitting = value;
        }

        public static void TransferSoul(InputStateMachine prev, InputStateMachine target)
        {
            PlayerSoul soul = prev.soul;
            soul.SetVisual(true);

            //soul.initialSoulPosition = prev.transform.position;

            soul.transform.parent = target.transform;
            prev.OnSoulExit();
            target.OnSoulEnter(soul);
            prev.soul = null;
        }
    }
}
