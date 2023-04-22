using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.States.Player;
using UnityEngine.SceneManagement;
using Twosies.Utils.Scenes;

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
            Scene oldActiveScene = SceneManager.GetActiveScene();

            SceneManager.SetActiveScene(gameObject.scene);
            PlayerStateMachine newPlayer = Instantiate(playerPrefab.gameObject, position, Quaternion.identity).GetComponent<PlayerStateMachine>();
            SceneManager.SetActiveScene(oldActiveScene);
            return newPlayer;

        }
    }
}
