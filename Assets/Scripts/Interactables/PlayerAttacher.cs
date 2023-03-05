using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerAttacher : MonoBehaviour
{
    Vector3 previousPos;
    [HideInInspector] public Vector3 velocity;

    public AttachableWith attachable;

    private void Awake()
    {
        previousPos = transform.position;
    }

    private void FixedUpdate()
    {
        velocity = (transform.position - previousPos) / Time.fixedDeltaTime;
        previousPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine sm))
        {
            sm.transform.parent = transform;

            sm.GetComponent<Rigidbody2D>().velocity = new Vector2(0, sm.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine sm))
        {
            sm.transform.parent = null;
            Debug.Log("unattach");
        }
    }
}


[System.Flags]
public enum AttachableWith
{
    grounded = 1, //01
    wallCling = 2, //10
    //next should be = 4 (100)
}
