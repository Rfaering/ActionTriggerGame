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
        _activeCoroutine = null;

        GlobalGameObjects.Canvas.Get().GetComponentInChildren<ProgressButton>(true).SetProgress(false);

        ResetVisualState();
    }

    IEnumerator BeginRuntimeMode()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            var overlayManager = GlobalGameObjects.OverlayManager.Get().GetComponent<OverlayManager>();
            if (!overlayManager.IsOverlayOpen)
            {
                // Only run win condition if the game is not in build mode
                if (!GlobalProperties.IsInBuildMode())
                {
                    if (IsWinConditionMet())
                    {                        
                        _infoManager.ShowWinForCurrentLevel();
                        GetComponent<GameStatistics>().StopLevelRecording();
                    }
                    else if (IsLoosingConditionMet())
                    {
                        _infoManager.ShowInfoForLevel("Level 007");
                    }
                }

                RunSingleRound();
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
        var activeWinCondition = components
            .Select(x => x.GetAction<Flower>())
            .Where(x => x.Active)
            .ToArray();

        if (activeWinCondition.Any(x => x.Done))
        {
            return activeWinCondition.All(x => x.Done);
        }

        return false;
    }

    private bool IsLoosingConditionMet()
    {
        var components = GetComponentsInChildren<Behaviors>();

        // Check win conditions
        var activeWinCondition = components
            .Select(x => x.GetAction<Flower>())
            .Where(x => x.Active)
            .ToArray();

        if (activeWinCondition.Any(x => x.Done))
        {
            return !activeWinCondition.All(x => x.Done);
        }

        return false;
    }

    private void ResetVisualState()
    {
        var behaviors = GetComponentsInChildren<Behaviors>();
        foreach (var item in behaviors)
        {
            item.ResetUI();
            item.gameObject.GetComponent<Animation>().Play("Reset", PlayMode.StopAll);
        }
    }
}
