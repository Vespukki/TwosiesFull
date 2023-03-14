using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Twosies.States.Player;

namespace Twosies.Meters
{
    public class StateReader : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] PlayerStateMachine playerSM;

        private void FixedUpdate()
        {
            text.SetText(playerSM.currentState?.ToString());
        }
    }
}
