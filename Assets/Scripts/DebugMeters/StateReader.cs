using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using States;

namespace DebugMeter
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
