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

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
        }

        public static void TransferSoul(InputStateMachine prev, InputStateMachine target)
        {
            prev.OnSoulExit();
            target.OnSoulEnter(prev.soul);
            prev.soul = null;
        }
    }
}
