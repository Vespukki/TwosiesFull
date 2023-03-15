using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.States;

namespace Twosies.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerSoul : MonoBehaviour
    {
        [HideInInspector] public PlayerInput input;
        public static PlayerSoul instance;

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
        }

        public static void TransferSoul(InputStateMachine prev, InputStateMachine target)
        {
            prev.soul.transform.parent = target.transform;
            prev.OnSoulExit();
            target.OnSoulEnter(prev.soul);
            prev.soul = null;
        }
    }
}
