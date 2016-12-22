using UnityEngine;

public abstract class Action : BehaviorBase
{
    public Action(GameObject owner) : base(owner)
    {
    }

    public abstract void Execute(GameObject gameObject);

    public override BehaviorTypes BehaviorType
    {
        get
        {
            return BehaviorTypes.Actions;
        }
    }

    public override void ClearUI(GameObject gameObject)
    {
        gameObject.GetComponent<ImageSetter>().ClearImages(BehaviorTypes.Actions);
    }

    public override void UpdateUI(GameObject gameObject, bool preview = false)
    {
    }
}
