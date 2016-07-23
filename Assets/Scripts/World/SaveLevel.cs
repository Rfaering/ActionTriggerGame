using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Serialization;
using System.IO;
using System.Linq;
using Assets.Scripts.Misc;
using Assets.Scripts.World.Tile;
using Assets.Scripts.Tile;
using Assets.Scripts.Canvas.Elements;

namespace Assets.Scripts.World
{
    public class SaveLevel : MonoBehaviour
    {
        public string SaveLevelName = "testLevel";

        public void SaveCurrentLevel()
        {
            var createTiles = GetComponent<CreateTiles>();

            List<TileData> tileData = new List<TileData>();
            foreach (Transform t in this.transform)
            {
                var visibility = t.gameObject.GetComponent<Visibility>();
                var triggers = t.gameObject.GetComponent<Behaviors>();
                tileData.Add(new TileData()
                {
                    Visible = visibility.IsVisible,
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
                Columns = createTiles.columns,
                Rows = createTiles.rows,
                Tiles = tileData.ToArray()
            };


            var content = JsonUtility.ToJson(levelData);

            using (FileStream fs = new FileStream("Assets/Resources/Levels/" + SaveLevelName + ".txt", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(content);
                }
            }

            FindObjectOfType<LevelsDropdown>().PopulateOptions();
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