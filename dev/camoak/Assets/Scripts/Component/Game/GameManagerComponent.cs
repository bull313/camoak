using UnityEngine;

namespace Camoak.Component.Game
{
    public class GameManagerComponent : MonoBehaviour
    {
        private GameComponent Game { get; set; }

        public void Start()
        {
            Game = GetComponent<GameComponent>();
            Game.Init();
        }

        public void Update() => Game.Play();
    }
}
