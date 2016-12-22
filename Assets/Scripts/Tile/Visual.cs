using UnityEngine;
using UnityEngine.UI;

public class Visual : MonoBehaviour
{
    private Runner _runner;
    private ConnectTiles _connectTiles;

    public bool Editable { get; set; }
    public bool InChain;
    public bool NextToChain;

    void Start()
    {
        _runner = FindObjectOfType<Runner>();
        _connectTiles = GetComponent<ConnectTiles>();
    }

    void Update()
    {
        if (Globals.InputMode == InputMode.Connect)
        {
            if (InChain)
            {
                bool willEdit = _connectTiles.ChainWillBeAdded;
                if (willEdit)
                {
                    transform.Find("Background").GetComponent<Image>().color = new Color(0.6f, 1.0f, 0.6f);
                }
                else
                {
                    transform.Find("Background").GetComponent<Image>().color = new Color(1.0f, 0.6f, 0.6f);
                }
            }
            else
            {
                transform.Find("Background").GetComponent<Image>().color = new Color(0.30f, 0.2f, 0.0f);
            }

            return;
        }

        if (_runner.IsRunning())
        {
            transform.Find("Background").GetComponent<Image>().color = new Color(0.30f, 0.2f, 0.0f);
            return;
        }

        if (Globals.InputMode == InputMode.DragAndDrop && DraggableButton.itemBeingDragged == null)
        {
            transform.Find("Background").GetComponent<Image>().color = new Color(0.30f, 0.2f, 0.0f);
            return;
        }

        if (Editable)
        {
            transform.Find("Background").GetComponent<Image>().color = new Color(0.0f, 0.4f, 0.0f);
        }
        else
        {
            transform.Find("Background").GetComponent<Image>().color = Color.red;
        }

    }
}
