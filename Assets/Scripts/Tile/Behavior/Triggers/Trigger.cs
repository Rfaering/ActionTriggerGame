using UnityEngine;

public abstract class Trigger : BehaviorBase
{
    public abstract bool Check();
    public bool Locked;

    public Trigger(GameObject owner) : base(owner)
    {
    }

    public override BehaviorTypes BehaviorType
    {
        get
        {
            return BehaviorTypes.Triggers;
        }
    }

    public override void ClearUI(GameObject gameobject)
    {
        gameobject.GetComponent<ImageSetter>().ClearImages(BehaviorTypes.Triggers);
    }


    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        //gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Full", Icon);
    }
}
