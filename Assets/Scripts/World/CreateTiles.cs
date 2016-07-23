using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.World.Tile;
using Assets.Scripts.Tile;

namespace Assets.Scripts.World
{
    public class CreateTiles : MonoBehaviour
    {
        public GameObject obj;

        public int rows = 4;
        public int columns = 6;

        public void RebuildWorld()
        {
            DestroyChildrens();
            CreateBoard();
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
                    gameObject.SetActive(true);
                    gameObject.GetComponent<Behaviors>().Initialize();
                    gameObject.GetComponent<Visibility>().IsVisible = true;
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
}