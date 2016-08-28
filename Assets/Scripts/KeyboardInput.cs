using Assets.Scripts.Tile.Behavior;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class KeyboardInput : MonoBehaviour
    {
        private Runner _runner;

        void Start()
        {
            _runner = FindObjectOfType<Runner>();
        }

        public void Update()
        {
            if (!GlobalProperties.IsOverlayPanelOpen)
            {
                var runner = FindObjectOfType<Runner>();

                if (Input.GetKeyDown(KeyCode.C))
                {
                    foreach (var behaviors in FindObjectsOfType<Behaviors>())
                    {
                        behaviors.Reset();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene(0);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (_runner.IsBoardStateGoingToWin())
                        {
                            return;
                        }
                        CreateRandomLevel();
                    }
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    runner.NumberOfMovesToWinningState();
                }
            }
        }

        private static void CreateRandomLevel()
        {
            foreach (var behaviors in FindObjectsOfType<RandomBehavior>())
            {
                behaviors.SelectRandomAvailibleTriggerAndAction();
            }
        }
    }
}
