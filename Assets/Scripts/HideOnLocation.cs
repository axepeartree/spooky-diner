using Commons;
using UnityEngine;
using UnityEngine.UI;

public class HideOnLocation : MonoBehaviour
{
    public Location TargetLocation;

    public void Hide(Location location) => gameObject.SetActive(TargetLocation != location);
}