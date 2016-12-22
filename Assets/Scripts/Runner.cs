using UnityEngine;
using System.Collections;
using System.Linq;

public class Runner : MonoBehaviour
{
    private Coroutine _activeCoroutine;
    private LevelInfoManager _infoManager;
    // Use this for initialization
    void Start()
    {
        StopRunning();
        _infoManager = FindObjectOfType<LevelInfoManager>();
    }

    void Update()
    {
        if (!GlobalProperties.IsOverlayPanelOpen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_activeCoroutine != null)
                {
                    StopRunning();
                }
                else
                {
                    StartRunning();
                }
            }

        }
    }

    public bool IsRunning()
    {
        return _activeCoroutine != null;
    }

    public void ToggleRunning()
    {
        if (_activeCoroutine == null)
        {
            StartRunning();
        }
        else
        {
            StopRunning();
        }
    }

    private void StartRunning()
    {
        FindObjectOfType<Canvas>().GetComponentInChildren<ProgressButton>(true).SetProgress(true);
        _activeCoroutine = StartCoroutine(BeginRuntimeMode());
    }

    public void StopRunning()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }
        FindObjectOfType<Number>().HideNumber();
        _activeCoroutine = null;

        FindObjectOfType<CanvasMenu>().GetComponentInChildren<ProgressButton>(true).SetProgress(false);

        ResetVisualState();
    }

    IEnumerator BeginRuntimeMode()
    {
        int number = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            var overlayManager = FindObjectOfType<OverlayManager>();
            if (!overlayManager.IsOverlayOpen)
            {
                // Only run win condition if the game is not in build mode                
                if (!GlobalProperties.IsInBuildMode() && IsLoosingConditionMet())
                {
                    PlayLooseAnimation();
                    yield return new WaitForSeconds(1.5f);
                    StopRunning();
                }
                else if (!GlobalProperties.IsInBuildMode() && IsWinConditionMet())
                {
                    _infoManager.ShowWinForCurrentLevel();
                    LevelEvents.SendCompletedEvent(FindObjectOfType<LoadLevel>().CurrentLevelName);
                }
                else
                {
                    number++;
                    FindObjectOfType<Number>().SetRoundNumber(number);
                    var hasProgressed = RunSingleRound();
                    if (!hasProgressed && !GlobalProperties.IsInBuildMode())
                    {
                        PlayLooseAnimation();
                        yield return new WaitForSeconds(1.5f);
                        StopRunning();
                    }
                }
            }
        }
    }

    public bool IsBoardStateGoingToWin()
    {
        return NumberOfMovesToWinningState() != -1;
    }

    public int NumberOfMovesToWinningState()
    {
        for (int i = 0; i < 100; i++)
        {
            bool winCondition = IsWinConditionMet();
            if (winCondition)
            {
                ResetVisualState();
                return i;
            }

            RunSingleRound();
        }

        ResetVisualState();
        return -1;
    }


    private bool RunSingleRound()
    {
        var hasUpdated = false;
        var tiles = GetComponentsInChildren<Behaviors>();

        foreach (var item in tiles)
        {
            hasUpdated |= item.UpdateTrigger();
        }

        var blackHoles = GetActions<BlackHole>(tiles);

        if (blackHoles.Any(x => x.IsBlackholeActive))
        {
            foreach (var item in blackHoles.Where(x => !x._owner.GetComponent<Behaviors>().Active))
            {
                item._owner.GetComponent<Behaviors>().Active = true;
                hasUpdated = true;
            }
        }

        foreach (var item in tiles)
        {
            var bridge = item.GetComponent<SelectedBehavior>().SelectedTrigger as BridgeUpDown;
            if (bridge != null && bridge.IsGoingToExecuteWaterFlow)
            {
                bridge.ExecuteUnderWaterFlow();
                hasUpdated = true;
            }

            if (!item.GetComponent<WaterState>().Watered)
            {
                hasUpdated |= item.UpdateActions();
            }
        }

        var bacterias = GetActions<Bacteria>(tiles);
        foreach (var item in bacterias)
        {
            item.Move();
        }

        return hasUpdated;
    }

    private static System.Collections.Generic.IEnumerable<T> GetActions<T>(Behaviors[] components) where T : class
    {
        string nameOfType = typeof(T).Name;

        return components
            .Select(x => x.GetComponent<SelectedBehavior>())
            .Where(x => x.IsNameSelected(nameOfType))
            .Select(x => x.GetSelectedBehavior(nameOfType) as T);
    }

    private bool IsWinConditionMet()
    {
        var components = GetComponentsInChildren<Behaviors>();

        // Check win conditions
        var redFlower = components
            .Select(x => x.GetAction<Flower>())
            .Where(x => x.Active)
            .ToArray();

        var blueFlowers = components
            .Select(x => x.GetAction<FlowerBlue>())
            .Where(x => x.Active)
            .ToArray();

        return redFlower.All(x => x.Done) && blueFlowers.All(x => x.Done);
    }

    private void PlayLooseAnimation()
    {
        var components = GetComponentsInChildren<Behaviors>();

        var normalFlowers = components
            .Select(x => x.GetAction<Flower>())
            .Where(x => x.Active)
            .ToArray();

        var blueFlowers = components
            .Select(x => x.GetAction<FlowerBlue>())
            .Where(x => x.Active)
            .ToArray();

        foreach (var item in normalFlowers)
        {
            if (!item.Done)
            {
                item._owner.GetComponent<Animation>().Blend("Highlight");
            }
        }

        foreach (var item in blueFlowers)
        {
            if (!item.Done)
            {
                item._owner.GetComponent<Animation>().Blend("Highlight");
            }
        }
    }

    private bool IsLoosingConditionMet()
    {
        var components = GetComponentsInChildren<Behaviors>();

        // Check win conditions
        var normalFlowers = components
            .Select(x => x.GetAction<Flower>())
            .Where(x => x.Active)
            .ToArray();

        var blueFlowers = components
            .Select(x => x.GetAction<FlowerBlue>())
            .Where(x => x.Active)
            .ToArray();

        bool result = false;

        if (normalFlowers.Any(x => x.Done))
        {
            if (!normalFlowers.All(x => x.Done))
            {
                result = true;
            }
        }

        if (blueFlowers.Any(x => x.Done))
        {
            if (!blueFlowers.All(x => x.Done))
            {
                result = true;
            }
        }

        return result;
    }

    private void ResetVisualState()
    {
        foreach (var item in FindObjectsOfType<CFX_AutoDestructShuriken>())
        {
            item.gameObject.SetActive(false);
        }

        var behaviors = GetComponentsInChildren<Behaviors>();
        foreach (var item in behaviors)
        {
            item.ResetUI();
            item.gameObject.GetComponent<Animation>().Play("Reset", PlayMode.StopAll);
        }
    }
}
