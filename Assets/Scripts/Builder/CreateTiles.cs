using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Serialization;
using System.IO;
using System.Linq;
using Assets.Scripts.Tiles;
using Assets.Scripts.Misc;
using Assets;
using UnityEngine.UI;
using System;
using Assets.Scripts.UI.Overlays;

public class CreateTiles : MonoBehaviour
{
    public GameObject obj;

    public int rows = 4;
    public int columns = 6;

    public string SaveLevelName = "testLevel";
    public string LoadLevelName = "";

    public void Start()
    {
        var levelPath = GetLevelPath(LoadLevelName);
        LoadLevel(levelPath);
    }

    public void LoadLevel(string levelPath)
    {
        if (File.Exists(levelPath))
        {
            var levelData = JsonUtility.FromJson<LevelData>(File.ReadAllText(levelPath));
            rows = levelData.Rows;
            columns = levelData.Columns;
            RebuildWorld();
            SetCorrectStateOnTiles(levelData);
        }
    }

    private void SetCorrectStateOnTiles(LevelData levelData)
    {
        for (int i = 0; i < levelData.Tiles.Length; i++)
        {
            var tile = levelData.Tiles[i];
            var matchingTile = transform.GetChild(i);
            matchingTile.GetComponent<Position>().Locked = tile.Locked;
            var triggerList = matchingTile.GetComponent<Behaviors>();
            foreach (var item in triggerList.AllTriggers)
            {
                var triggerData = tile.Triggers.FirstOrDefault(x => item.GetType().Name == x.Name);
                if (triggerData != null)
                {
                    item.Active = triggerData.Applied;
                    item.Available = triggerData.Available;
                }
            }

            foreach (var item in triggerList.AllActions)
            {
                var actionData = tile.Actions.FirstOrDefault(x => item.GetType().Name == x.Name);
                if (actionData != null)
                {
                    item.Active = actionData.Applied;
                    item.Available = actionData.Available;
                }
            }
        }
    }

    public void RebuildWorld()
    {
        DestroyChildrens();
        CreateBoard();
    }

    public void SaveLevelFromComponent()
    {
        SaveLevel(SaveLevelName);
    }

    public void SaveLevelFromOverLay()
    {
        var designPanel = Globals.DesignPanel.Get();
        var saveOverlay = designPanel.Overlay.GetComponent<SaveOverlay>();

        if (!string.IsNullOrEmpty(saveOverlay.GetInputField()))
        {
            SaveLevel(saveOverlay.GetInputField());
            designPanel.CloseActiveOverlay();
        }
        else
        {
            Console.WriteLine("Input field is invalid");
        }
    }

    private void SaveLevel(string level)
    {
        List<TileData> tileData = new List<TileData>();
        foreach (Transform t in this.transform)
        {
            var position = t.gameObject.GetComponent<Position>();
            var triggers = t.gameObject.GetComponent<Behaviors>();
            tileData.Add(new TileData()
            {
                Locked = position.Locked,
                Triggers = triggers
                .GetBehaviorList(BehaviorTypes.Triggers)
                .Where(x => x.Active || x.Available)
                .Select(x => CreateBehaviorData<TriggerData>(x))
                .ToArray(),

                Actions = triggers
                .GetBehaviorList(BehaviorTypes.Actions)
                .Where(x => x.Active || x.Available)
                .Select(x => CreateBehaviorData<ActionData>(x)).ToArray()
            });
        }

        var levelData = new LevelData()
        {
            Columns = columns,
            Rows = rows,
            Tiles = tileData.ToArray()
        };

        var levelPath = GetLevelPath(level);
        var content = JsonUtility.ToJson(levelData);

        File.WriteAllText(levelPath, content);
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

    private string GetLevelPath(string name)
    {
        return Path.Combine(Path.Combine(Application.dataPath, "Levels"), name + ".txt");
    }

    private void CreateBoard()
    {
        Vector3 position = new Vector3(-columns + 1, rows - 1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var gameObject = Instantiate(obj, position + new Vector3(j * 2, -i * 2), Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f))) as GameObject;
                gameObject.name = "Tiles " + (1 + (i * columns + j));
                gameObject.transform.SetParent(this.transform);

                if (j > 0)
                {
                    var leftChild = transform.GetChild(GetIndex(i, j - 1));
                    gameObject.GetComponent<Position>().Left = leftChild.gameObject;
                    leftChild.GetComponent<Position>().Right = gameObject;
                }

                if (i > 0)
                {
                    var topChild = transform.GetChild(GetIndex(i - 1, j));
                    gameObject.GetComponent<Position>().Up = topChild.gameObject;
                    topChild.GetComponent<Position>().Down = gameObject;
                }
            }
        }
    }

    private int GetIndex(int row, int column)
    {
        return (row * columns + column);
    }

    private void DestroyChildrens()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform t in this.transform)
        {
            children.Add(t);
        }

        foreach (Transform t in children)
        {
            DestroyImmediate(t.gameObject);
        }
    }
}


