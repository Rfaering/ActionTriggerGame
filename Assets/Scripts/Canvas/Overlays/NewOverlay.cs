﻿using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas.Overlays
{
    public class NewOverlay : MonoBehaviour
    {
        private const int DefaultValue = 4;

        public int GetRows()
        {
            return GetValue("Rows");
        }

        public int GetColumns()
        {
            return GetValue("Columns");
        }

        public void CreateWorld()
        {
            var createTiles = GlobalGameObjects.World.Get().GetComponent<CreateTiles>();
            createTiles.rows = GetRows();
            createTiles.columns = GetColumns();
            createTiles.RebuildWorld();

            createTiles.GetComponent<LoadLevel>().CurrentLevelName = "New Level";
            createTiles.GetComponent<SaveLevel>().SaveLevelName = "New Level";

            FindObjectOfType<SaveLevel>().SaveLevelName = "";


            GetComponentInParent<OverlayManager>().CloseActiveOverlay();
        }

        private int GetValue(string childName)
        {
            try
            {
                return int.Parse(transform.Find(childName).GetComponent<InputField>().text);

            }
            catch
            {
                return DefaultValue;
            }
        }
    }
}
