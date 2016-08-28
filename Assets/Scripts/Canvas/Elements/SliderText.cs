using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public void UpdateText()
    {
        GetComponent<Text>().text = GetComponentInParent<Slider>().value.ToString();
    }
}
