using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;

    public IEnumerable<GameObject> Neighbours
    {
        get
        {
            var result = new List<GameObject>();

            if (Up != null)
            {
                result.Add(Up);
            }
            if (Down != null)
            {
                result.Add(Down);
            }
            if (Left != null)
            {
                result.Add(Left);
            }
            if (Right != null)
            {
                result.Add(Right);
            }

            return result;
        }
    }

    public GameObject VerticalMirror;
    public GameObject HorizontalMirror;
}