using UnityEngine;

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
