using UnityEngine;

public class BlackHole : Action
{
    public bool IsBlackholeActive { get; set; }

    public BlackHole(GameObject owner) : base(owner)
    {
    }

    public override void Execute(GameObject gameObject)
    {
        IsBlackholeActive = true;
        gameObject.GetComponent<WaterState>().Watered = true;
    }

    public override void Reset()
    {
        IsBlackholeActive = false;
        base.Reset();
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.BlackHole, preview);
        base.UpdateUI(gameobject);
    }
}
