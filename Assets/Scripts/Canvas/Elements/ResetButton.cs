using UnityEngine;
using Assets.Scripts.World.Tile;

namespace Assets.Scripts.Canvas.Elements
{
    public class ResetButton : MonoBehaviour
    {
        public void Reset()
        {
            foreach (var behaviors in FindObjectsOfType<Behaviors>())
            {
                behaviors.Reset();
            }
        }
    }
}
