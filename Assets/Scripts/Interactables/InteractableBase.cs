using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.States.Player;
using TMPro;

namespace Twosies.Interactable
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] TextMeshPro hightlightText;

        public virtual void Interact(PlayerStateMachine player)
        {

        }

        public virtual void SetHighlight(bool value)
        {
            hightlightText.gameObject.SetActive(value);
        }
    }
}