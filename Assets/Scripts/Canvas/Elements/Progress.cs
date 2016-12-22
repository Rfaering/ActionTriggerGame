using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    public void SetProgress(bool inProgress)
    {
        this.GetComponent<Image>().color = inProgress ? new Color(0.5f, 1.0f, 0.5f) : new Color(1.0f, 0.5f, 0.5f);
    }
}
