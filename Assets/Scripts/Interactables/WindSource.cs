using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.Interactables
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WindSource : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayers;
        private List<Rigidbody2D> affectedList = new();

        [SerializeField] private float force;
        [SerializeField] private float drag;

        private BoxCollider2D coll;

        private void Awake()
        {
            coll = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (ValidLayer(collision.gameObject.layer))
            {
                if ( collision.TryGetComponent(out Rigidbody2D body) && !affectedList.Contains(body))
                {
                    affectedList.Add(body);
                }
            }
        }

        private void FixedUpdate()
        {
            foreach(var body in affectedList)
            {
                float forceMult = Mathf.InverseLerp(coll.bounds.center.y + coll.bounds.extents.y, coll.bounds.center.y - coll.bounds.extents.y,
                    body.transform.position.y);

                Vector2 appliedForce = force * forceMult * transform.up;
                body.AddForce(appliedForce);

                Vector2 a = (body.velocity * transform.up).normalized;
                Vector2 b = (a + (Vector2)transform.up);

                bool goingSameWayAsAir = (b == Vector2.zero);
                if (goingSameWayAsAir)
                {
                    body.velocity = new Vector2(body.velocity.x, body.velocity.y * (1 - drag));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (ValidLayer(collision.gameObject.layer))
            {
                if (collision.TryGetComponent(out Rigidbody2D body) && affectedList.Contains(body))
                {
                    affectedList.Remove(body);
                }
            }
        }

        private bool ValidLayer(int layer)
        {
            return interactableLayers == (interactableLayers | (1 << layer));
        }

    }
}