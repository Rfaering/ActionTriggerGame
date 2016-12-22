using UnityEngine;

public class Lock : Action
{
    public bool Locked { get; set; }

    public Lock(GameObject owner) : base(owner)
    {
        Locked = true;
    }

    public void Unlock()
    {
        Locked = false;
        _owner.GetComponent<Animation>().Play("Unlock");
        if (_owner.GetComponent<Behaviors>().Active)
        {
            Execute(_owner);
        }
    }

    public override void Execute(GameObject gameObject)
    {
        if (Locked == false)
        {
            gameObject.GetComponent<WaterState>().Watered = true;
        }
    }

    public override void UpdateUI(GameObject gameObject, bool preview = false)
    {
        gameObject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Lock, preview);
        base.UpdateUI(gameObject);
    }

    public override void Reset()
    {
        Locked = true;
        base.Reset();
    }
}
