using Assets.Scripts.Tile.Behavior;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class Visual : MonoBehaviour
    {
        private Position _position;
        private Runner _runner;
        private SelectedBehavior _selection;

        public bool Editable { get; set; }

        void Start()
        {
            _position = GetComponent<Position>();
            _selection = GetComponent<SelectedBehavior>();
            _runner = GlobalGameObjects.World.Get().GetComponent<Runner>();
        }

        void Update()
        {
            if (!_position.Death)
            {
                if (Editable && !_runner.IsRunning() && !_selection.HasBothSelected())
                {
                    transform.Find("Background").GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.4f, 0.0f);
                }
                else
                {
                    transform.Find("Background").GetComponent<SpriteRenderer>().color = Color.red;
                }
            }

        }
    }
}
