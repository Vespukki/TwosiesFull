using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.States.Player;

namespace Twosies
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public PlayerStateMachine playerPrefab;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        public PlayerStateMachine SpawnPlayer(Vector2 position)
        {
            PlayerStateMachine newPlayer = Instantiate(playerPrefab.gameObject, position, Quaternion.identity).GetComponent<PlayerStateMachine>();
            
            return newPlayer;

        }
    }
}
