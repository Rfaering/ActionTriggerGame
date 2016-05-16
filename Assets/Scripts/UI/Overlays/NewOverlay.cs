using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Overlay
{
    public class NewOverlay : MonoBehaviour
    {
        private const int DefaultValue = 4;

        public int GetRows()
        {
            return GetValue("Rows");
        }

        public int GetColumns()
        {
            return GetValue("Columns");
        }

        private int GetValue(string childName)
        {
            try
            {
                return int.Parse(transform.Find(childName).GetComponent<InputField>().text);

            }
            catch
            {
                return DefaultValue;
            }
        }
    }
}
