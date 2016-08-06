using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class WaterState : MonoBehaviour
    {

        #region Death
        private bool _watered;
        public bool Watered
        {
            get { return _watered; }
            set
            {
                _watered = value;
                UpdateDeath();

            }
        }

        private void UpdateDeath()
        {
            if (!Watered)
            {
                gameObject.GetComponent<Animation>().Stop();
                transform.Find("Foreground").GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
            }
            else
            {
                gameObject.GetComponent<Animation>().Play();
            }
        }
        #endregion
    }
}
