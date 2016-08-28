using UnityEngine;

public class GroupImage : MonoBehaviour
{
    public int CurrentGroup = 1;
    public void SetGroup(int group)
    {
        transform.Find(CurrentGroup.ToString()).gameObject.SetActive(false);
        CurrentGroup = group;
        transform.Find(CurrentGroup.ToString()).gameObject.SetActive(true);
    }
}
