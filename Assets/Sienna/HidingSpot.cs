using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] public HidingSpotType hidingSpotType;
    public enum HidingSpotType
    {
        Top,
        Middle,
        Bottom
    }

    public bool isHiding;
}
