using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.Interactable
{
    public class Button : MonoBehaviour
    {
        private List<Collider2D> pressers = new();

        /// <summary>
        /// make sure target derives from IToggleable
        /// </summary>
        [SerializeField] GameObject targetGameObject;
        private IToggleable target;

        [SerializeField] bool activatedOnPress;

        private bool _pressed;
        private bool Pressed { get { return _pressed; } set { _pressed = value; target.Toggle(value == activatedOnPress); } }

        private void Awake()
        {
            if(targetGameObject.TryGetComponent(out IToggleable toggler))
            {
                target = toggler;
            }
            else
            {
                Debug.LogError(this.name + "'s target is not toggleable!!");
            }
        }

        private void Start()
        {
            target.Toggle(!activatedOnPress);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                pressers.Add(collision);

                if(Pressed != true)
                {
                    Pressed = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                pressers.Remove(collision);

                if(pressers.Count <= 0)
                {
                    if (Pressed != false)
                    {
                        Pressed = false;
                    }
                }
            }
        }
    }
}
