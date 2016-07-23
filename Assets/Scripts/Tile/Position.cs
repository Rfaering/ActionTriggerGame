using Assets;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class Position : MonoBehaviour
    {
        public GameObject Up;
        public GameObject Down;
        public GameObject Left;
        public GameObject Right;

        public void Start()
        {
        }

        public void Update()
        {

        }

        #region Death
        private bool _death;
        public bool Death
        {
            get { return _death; }
            set
            {
                _death = value;
                UpdateDeath();

            }
        }

        private void UpdateDeath()
        {
            if (!Death)
            {
                gameObject.GetComponent<Animation>().Stop();          
                transform.Find("Foreground").GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);                
                transform.Find("Action").GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);                
            }
            else
            {
                gameObject.GetComponent<Animation>().Play();
            }
        }
        #endregion
    }
}