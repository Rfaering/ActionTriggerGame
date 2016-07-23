﻿using UnityEngine;
using Assets.Scripts.Serialization;
using System.Linq;
using Assets.Scripts.Misc;
using Assets.Scripts.World.Tile;
using Assets.Scripts.Tile;
using Assets.Scripts.Stats;
using Assets.Scripts.Utils;
using Assets.Scripts.Buttons;
using Assets.Scripts.Tile.Behavior;

namespace Assets.Scripts.World
{
    public class LoadLevel : MonoBehaviour
    {
        public string CurrentLevelName = "Level 1";
        private CanvasMenu _menu;

        public void Start()
        {
            LoadCurrentLevel();
            _menu = GlobalGameObjects.Canvas.Get().GetComponent<CanvasMenu>();
        }

        public void LoadCurrentLevel()
        {
            var levelPath = LevelsInfo.GetLevel(CurrentLevelName);

            var createTiles = GetComponent<CreateTiles>();

            if (levelPath != null)
            {
                var levelData = JsonUtility.FromJson<LevelData>(levelPath);

                createTiles.rows = levelData.Rows;
                createTiles.columns = levelData.Columns;

                createTiles.RebuildWorld();
                SetCorrectStateOnTiles(levelData);
            }
            SetCanvasMenu();
            GetComponent<GameStatistics>().StartLevelRecording();
        }

        public void SetCanvasMenu()
        {
            if (_menu)
            {
                _menu.DisableAllButtons();

                if (!GlobalProperties.IsInBuildMode())
                {
                    SetRuntimeButtonLayouts(BehaviorTypes.Actions);
                    SetRuntimeButtonLayouts(BehaviorTypes.Triggers);
                }
                else
                {                    
                    SetDesignModeButtonLayout(BehaviorTypes.Actions);
                    SetDesignModeButtonLayout(BehaviorTypes.Triggers);
                }
            }
        }

        private void SetButtonCount(int count, BehaviorTypes type)
        {
            var createButtons = FindObjectOfType<CreateButtons>();            
            createButtons.BuildObject(type, count);
        }

        private void SetCorrectStateOnTiles(LevelData levelData)
        {
            for (int i = 0; i < levelData.Tiles.Length; i++)
            {
                var tile = levelData.Tiles[i];
                var matchingTile = transform.GetChild(i);
                matchingTile.GetComponent<Visibility>().IsVisible = tile.Visible;
                var selectedBehavior = matchingTile.GetComponent<SelectedBehavior>();

                var triggerList = matchingTile.GetComponent<Behaviors>();
                foreach (var item in triggerList.AllTriggers)
                {
                    var triggerData = tile.Triggers.FirstOrDefault(x => item.GetType().Name == x.Name);
                    if (triggerData != null)
                    {
                        item.Available = triggerData.Available;

                        if (triggerData.Applied)
                        {
                            selectedBehavior.SelectedTrigger = item;
                        }
                    }
                }

                foreach (var item in triggerList.AllActions)
                {
                    var actionData = tile.Actions.FirstOrDefault(x => item.GetType().Name == x.Name);
                    if (actionData != null)
                    {
                        item.Available = actionData.Available;

                        if (actionData.Applied)
                        {
                            selectedBehavior.SelectedAction = item;
                        }
                    }
                }
            }

            GlobalGameObjects.World.Get().GetComponent<Runner>().StopRunning();

            foreach (var animation in GetComponentsInChildren<Visual>().Select(x => x.gameObject.GetComponent<Animation>()))
            {
                animation.Play("StartAnimation");
            }

            FindObjectOfType<SaveLevel>().SaveLevelName = CurrentLevelName;
        }

        private void SetRuntimeButtonLayouts(BehaviorTypes type)
        {
            var behaviors = GetComponentsInChildren<Behaviors>() ?? new Behaviors[0];            

            var actionBehaviors = behaviors
                .SelectMany(x => x.GetBehaviorList(type))
                .Where(x => x.Available)
                .DistinctBy(x => x.Name)
                .ToArray();

            SetButtonCount(actionBehaviors.Count(), type);

            _menu.SetOptions(actionBehaviors, type);
        }

        private void SetDesignModeButtonLayout(BehaviorTypes type)
        {
            var behavior = GetComponentInChildren<Behaviors>();
            var actionBehaviors = behavior.GetBehaviorList(type).ToArray();

            SetButtonCount(actionBehaviors.Count(), type);

            _menu.SetOptions(actionBehaviors, type);
        }

        private T CreateBehaviorData<T>(BehaviorBase behaviorBase) where T : BehaviorData, new()
        {
            return new T()
            {
                Name = behaviorBase.GetType().Name,
                Available = behaviorBase.Available,
                Applied = behaviorBase.Active
            };
        }
    }
}