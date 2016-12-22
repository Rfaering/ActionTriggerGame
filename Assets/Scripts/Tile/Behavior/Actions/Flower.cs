using UnityEngine;

public class Flower : Action
{
    public Flower(GameObject owner) : base(owner)
    {
    }

    public bool Done { get; set; }

    public override void Execute(GameObject gameObject)
    {
        Done = true;
        _owner.GetComponent<WaterState>().Watered = true;

        var next = CFX_SpawnSystem.GetNextObject(CFX_SpawnSystem.instance.objectsToPreload[1]);
        if (next == null)
        {
            return;
        }
        next.transform.position = gameObject.transform.position;
        next.transform.position = new Vector3(next.transform.position.x, next.transform.position.y, -5);
        next.SetActive(true);
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Flower, preview);
        base.UpdateUI(gameobject);
    }
}
