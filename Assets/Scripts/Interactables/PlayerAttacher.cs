using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerAttacher : MonoBehaviour, IJumpModifier
{
    Vector3 previousPos;
    [SerializeField] private Vector3 velocity;
    public Vector2 JumpModifierVelocity => velocity;

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
            sm.jumpModifiers.Add(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine sm))
        {
            sm.transform.parent = null;
            sm.jumpModifiers.Remove(this);
        }
    }
}


[System.Flags]
public enum AttachableWith
{
    grounded = 1, //001
    wallCling = 2, //010
    //next should be = 4 (100)
}
