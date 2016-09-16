using UnityEngine;

public class Hint : MonoBehaviour
{
    public void PlayHint(string hint)
    {
        gameObject.SetActive(true);
        GetComponent<Animation>().Play(hint);
    }
}
