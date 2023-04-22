using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.Utils.Scenes;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Twosies.States;
namespace Twosies.Interactable.Doors
{
    [CreateAssetMenu]
    public class DoorObject : ScriptableObject
    {
        [SerializeField] private SceneField _scene;

        public string SceneName => _scene; // set by door class

        [SerializeField] private Vector2 _position;
        public Vector2 Position => _position; //Set by Door class

        public void Exit(InputStateMachine player)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));
            player.transform.position = Position;
        }

        public async Task LoadScene(string sceneName)
        {
            var progress = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!progress.isDone)
            {
                await Task.Yield();
            }
        }

        
    }
}
