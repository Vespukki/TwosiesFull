using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twosies.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Twosies.Utils;
using Twosies.States.Player;

namespace Twosies.Interactable.Doors
{
    public class Door : InteractableBase
    {
        public DoorObject destination;

        public override void Interact(PlayerStateMachine player)
        {
            base.Interact(player);

            Enter(player);
        }

        async Task UnloadScene(string sceneName)
        {

            var progress = SceneManager.UnloadSceneAsync(sceneName);

            while (!progress.isDone)
            {
                await Task.Yield();
            }
        }

        public async void Enter(PlayerStateMachine player)
        {
            player.ChangeState(new PlayerDoorState(player));
            await Easing.ScreenFadeOut();
            await destination.LoadScene(destination.SceneName);
            destination.Exit(player);
            await UnloadScene(gameObject.scene.name);
            player.ChangeState(new PlayerWalkingState(player));
            await Easing.ScreenFadeIn();

        }



    }
}
