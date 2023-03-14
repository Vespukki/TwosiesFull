using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.States.Player;

namespace Twosies.Physics
{
public class Pusher : MonoBehaviour
{
    [SerializeField] float force;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine sm))
        {
            sm.GetComponent<Rigidbody2D>().AddForce(force * transform.up, ForceMode2D.Impulse);
        }
    }
}
}