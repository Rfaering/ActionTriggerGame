﻿using UnityEngine;

public class FlowerBlue : Action
{
    public FlowerBlue(GameObject owner) : base(owner)
    {
    }

    public bool Done { get; set; }

    public override void Execute(GameObject gameObject)
    {
        Done = true;
        _owner.GetComponent<WaterState>().Watered = true;

        var next = CFX_SpawnSystem.GetNextObject(CFX_SpawnSystem.instance.objectsToPreload[2]);
        if (next == null)
        {
            return;
        }
        next.transform.position = gameObject.transform.position;
        next.transform.position = new Vector3(next.transform.position.x, next.transform.position.y, -5);
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.FlowerBlue, preview);
        base.UpdateUI(gameobject);
    }
}
