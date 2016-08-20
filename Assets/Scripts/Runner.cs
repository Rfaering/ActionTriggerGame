using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Scripts.Misc;
using Assets.Scripts.Actions;
using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.World.Tile;
using Assets.Scripts.Tile;
using Assets.Scripts.Stats;
using Assets.Scripts.World;
using Assets.Scripts.Levels;
using Assets.Scripts.Tile.Behavior;
using Assets.Scripts.Tile.Behavior.Triggers.Directions;

public class Runner : MonoBehaviour
{
    private Coroutine _activeCoroutine;
    private BuildMode _buildMode;
    private LevelInfoManager _infoManager;
    // Use this for initialization
    void Start()
    {
        StopRunning();
        StartCoroutine(LateStart(0.1f));
        _buildMode = GetComponent<BuildMode>();
        _infoManager = FindObjectOfType<LevelInfoManager>();
    }


    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        var level = GlobalGameObjects.World.Get().GetComponent<LoadLevel>();
        level.CurrentLevelName = "Level 001";
        level.LoadCurrentLevel();


#if UNITY_ANDROID
        _buildMode.RuntimeMode = BuilderMode.Running;
#endif

#if UNITY_IOS
        _buildMode.RuntimeMode = BuilderMode.Running;
#endif

#if UNITY_STANDALONE_WIN
        _buildMode.RuntimeMode = BuilderMode.DesignMode;
#endif        
    }

    void Update()
    {
        if (!GlobalProperties.IsOverlayPanelOpen())
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
        GlobalGameObjects.Canvas.Get().GetComponentInChildren<ProgressButton>(true).SetProgress(true);
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

        GlobalGameObjects.Canvas.Get().GetComponentInChildren<ProgressButton>(true).SetProgress(false);

        ResetVisualState();
    }

    IEnumerator BeginRuntimeMode()
    {
        int number = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            var overlayManager = GlobalGameObjects.OverlayManager.Get().GetComponent<OverlayManager>();
            if (!overlayManager.IsOverlayOpen)
            {
                // Only run win condition if the game is not in build mode                
                if (!GlobalProperties.IsInBuildMode() && IsLoosingConditionMet())
                {
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
                    RunSingleRound();
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


    private void RunSingleRound()
    {
        var components = GetComponentsInChildren<Behaviors>();
        foreach (var item in components)
        {
            item.UpdateTrigger();
        }

        foreach (var item in components)
        {
            var bridge = item.GetComponent<SelectedBehavior>().SelectedTrigger as BridgeUpDown;
            if (bridge != null && bridge.IsGoingToExecuteWaterFlow)
            {
                bridge.ExecuteUnderWaterFlow();
            }

            if (!item.GetComponent<WaterState>().Watered)
            {
                item.UpdateActions();
            }
        }
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

        if (normalFlowers.Any(x => x.Done))
        {
            if (!normalFlowers.All(x => x.Done))
            {
                foreach (var item in normalFlowers)
                {
                    item._owner.GetComponent<Animation>().Blend("Highlight");
                }

                return true;
            }
        }

        if (blueFlowers.Any(x => x.Done))
        {
            if (!blueFlowers.All(x => x.Done))
            {
                foreach (var item in blueFlowers)
                {
                    item._owner.GetComponent<Animation>().Blend("Highlight");
                }

                return true;
            }
        }

        return false;
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
