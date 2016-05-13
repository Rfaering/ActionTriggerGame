using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using Assets.Scripts.Tiles;
using Assets.Scripts.Misc;
using Assets.Scripts.UI;

public class Runner : MonoBehaviour {
    private Coroutine _activeCoroutine;

    private BuilderMode _runtimeMode;
    public BuilderMode RuntimeMode
    {
        get { return _runtimeMode; }
        set
        {
            SetNewRuntimeMode(value);
            _runtimeMode = value;
        }
    }    
    
    // Use this for initialization
    void Start () {
        StopRunning();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if( _activeCoroutine != null)
            {
                StopRunning();
            }
            else
            {
                StartRunning();
            }
        }

        if( Input.GetKeyDown( KeyCode.Q ) )
        {
            RuntimeMode = RuntimeMode == BuilderMode.DesignMode ? BuilderMode.Running : BuilderMode.DesignMode;
        }

        if ( Input.GetKeyDown(KeyCode.S) )
        {
            RuntimeMode = RuntimeMode == BuilderMode.DesignMode ? BuilderMode.Running : BuilderMode.DesignMode;
        }
    }

    private void StartRunning()
    {
        SetProgressColor(new Color(0.5f, 1.0f, 0.5f));
        _activeCoroutine = StartCoroutine(SingleRound());
    }

    private void StopRunning()
    {
        if(_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }        
        _activeCoroutine = null;
        SetProgressColor(new Color(1.0f, 0.5f, 0.5f));
        var components = GetComponentsInChildren<Position>();

        foreach (var item in components)
        {
            item.Death = false;
        }

    }

    private void SetProgressColor(Color color)
    {
        GameObject.Find("Progress").GetComponent<Image>().color = color;
    }

    private void SetNewRuntimeMode(BuilderMode value)
    {
        if (value == BuilderMode.DesignMode)
        {
            GameObject.Find("BuildMode").GetComponent<Text>().text = "Design Mode";
        }
        if (value == BuilderMode.Running)
        {
            GameObject.Find("BuildMode").GetComponent<Text>().text = "Debug Mode";            
        }

        UnSelectAll();
    }

    private void UnSelectAll()
    {
        Selection[] selections = GetComponentsInChildren<Selection>();
        foreach (var item in selections)
        {
            item.Selected = false;
        }

        GameObject.Find("Canvas")
            .GetComponent<CanvasMenu>()
            .DisableAllButtons();
    }

    IEnumerator SingleRound()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            var components = GetComponentsInChildren<Behaviors>();
            foreach (var item in components) {
                item.UpdateTrigger();
            }

            foreach (var item in components) {
                item.UpdateActions();                
            }
        }
    }
}
