using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public void Start()
    {
        HideNumber();
    }

    public void SetRoundNumber(int number)
    {
        GetComponent<Text>().text = number.ToString();
    }

    public void HideNumber()
    {
        GetComponent<Text>().text = string.Empty;
    }
}
