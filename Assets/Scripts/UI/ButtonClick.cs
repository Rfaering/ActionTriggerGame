using Assets.Scripts.Misc;
using Assets.Scripts.Tiles;
using Assets.Scripts.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ButtonClick : MonoBehaviour
    {
        public BehaviorBase BehaviorBase;
        public GameObject GameObject;

        public void ActivateBehavior()
        {            
            var isInBuildMode = Globals.IsInBuildMode();

            if(isInBuildMode && Input.GetKey(KeyCode.LeftControl))
            {
                BehaviorBase.Available = !BehaviorBase.Available;
                SetColorBasedOnAvailibility();
            }
            else
            {
                var listOfSameType = GameObject.GetComponent<Behaviors>()
                .GetBehaviorList(BehaviorBase.BehaviorType);

                foreach (var item in listOfSameType)
                {
                    item.Active = false;
                }

                BehaviorBase.Active = true;
            }            
        }

        private void SetColorBasedOnAvailibility()
        {
            if (!BehaviorBase.Available)
            {
                this.GetComponent<Image>().color = Color.red;
            }
            else
            {
                this.GetComponent<Image>().color = Color.white;
            }
        }

        public void SetBehavior(GameObject gameObject, BehaviorBase behavior )
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = behavior.Icon;
            GameObject = gameObject;
            BehaviorBase = behavior;
            SetColorBasedOnAvailibility();
        }
    }
}
