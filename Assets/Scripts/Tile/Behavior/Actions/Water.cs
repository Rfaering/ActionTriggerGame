using UnityEngine;

public class Water : Action
{
    public Water(GameObject owner) : base(owner)
    {
    }

    public override void Execute(GameObject gameObject)
    {
        gameObject.GetComponent<WaterState>().Watered = true;

        var next = CFX_SpawnSystem.GetNextObject(CFX_SpawnSystem.instance.objectsToPreload[0]);
        if (next == null)
        {
            return;
        }
        next.transform.position = gameObject.transform.position;
        next.SetActive(true);
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Water, preview);
        base.UpdateUI(gameobject);
    }
}
