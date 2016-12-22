using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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
            transform.Find("Foreground").GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
        }
        else
        {
            gameObject.GetComponent<Animation>().Blend("KillAnimation");
        }
    }
    #endregion
}
