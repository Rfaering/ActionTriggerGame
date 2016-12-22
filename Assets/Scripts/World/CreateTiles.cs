using UnityEngine;
using System.Collections.Generic;

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
        RectTransform reactTransform = this.gameObject.transform as RectTransform;
        reactTransform.sizeDelta = new Vector2(90 * columns, 90 * rows);
        Vector3 position = new Vector3(0 - (((columns - 1) / 2.0f) * 1.7f), (((rows - 1) / 2.0f) * 1.7f));

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var gameObject = Instantiate(obj) as GameObject;
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

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (i < ((rows + 1) / 2))
                {
                    var unit = transform.GetChild(GetIndex(i, j));
                    var mirror = transform.GetChild(GetIndex(rows - 1 - i, j));
                    unit.GetComponent<Position>().VerticalMirror = mirror.gameObject;
                    mirror.GetComponent<Position>().VerticalMirror = unit.gameObject;
                }

                if (j < ((columns + 1) / 2))
                {
                    var unit = transform.GetChild(GetIndex(i, j));
                    var mirror = transform.GetChild(GetIndex(i, columns - 1 - j));
                    unit.GetComponent<Position>().HorizontalMirror = mirror.gameObject;
                    mirror.GetComponent<Position>().HorizontalMirror = unit.gameObject;
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