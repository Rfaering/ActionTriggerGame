using UnityEngine;

public class Position : MonoBehaviour {
    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;

    public void Start()
    {
    }

    public void Update()
    {
        if( GetComponent<Selection>().Selected && Input.GetKeyDown( KeyCode.L ) )
        {
            Locked = !Locked;
        }   
    }

    #region Death
    private bool _death;
    public bool Death
    {
        get { return _death; }
        set
        {
            _death = value;
            UpdateDeathColor();

        }
    }

    private void UpdateDeathColor()
    {
        if (_death)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.7f);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
    #endregion

    #region Locked
    private bool _locked;
    public bool Locked
    {
        get { return _locked; }
        set
        {
            _locked = value;
            UpdateLockedVisibility();

        }
    }

    private void UpdateLockedVisibility()
    {
        if (_locked)
        {
            gameObject.transform.Find("Foreground")
                .Find("Locked")
                .gameObject
                .SetActive(true);
        }
        else
        {
            gameObject.transform.Find("Foreground")
                .Find("Locked")
                .gameObject
                .SetActive(false);
        }
    }
    #endregion
}

