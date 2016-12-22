using UnityEngine;

public class Key : Action
{
    public Key(GameObject owner) : base(owner)
    {
    }

    public override void Execute(GameObject gameObject)
    {
        gameObject.GetComponent<WaterState>().Watered = true;
        gameObject.GetComponent<ImageSetter>().RemoveSpecialVisual(ImageSetter.SpecialTypes.Key);
        foreach (var selectedBehavior in GameObject.FindObjectsOfType<SelectedBehavior>())
        {
            if (selectedBehavior.IsNameSelected("Lock"))
            {
                (selectedBehavior.SelectedAction as Lock).Unlock();
            }
        }
    }

    public override void UpdateUI(GameObject gameObject, bool preview = false)
    {
        gameObject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Key, preview);
        base.UpdateUI(gameObject);
    }
}
