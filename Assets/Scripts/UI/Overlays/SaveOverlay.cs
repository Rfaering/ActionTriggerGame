using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Overlays
{
    public class SaveOverlay : MonoBehaviour
    {
        public string GetInputField()
        {
            return transform.Find("LevelName").GetComponent<InputField>().text;
        }      
    }
}
