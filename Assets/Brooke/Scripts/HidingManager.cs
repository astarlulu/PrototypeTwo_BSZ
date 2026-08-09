using System.Linq;
using UnityEngine;

public class HidingManager : MonoBehaviour
{
    //attach all characters
    [SerializeField] private HideableCharacters[] characters;

    //going through the order of the characters and hiding them in the hiding spot 1->3
    public void HideNext(HidingSpot spot)
    {
        var next =
            characters
            .Where(c => !c.IsHidden)
            .OrderBy(c => c.HideOrder)
            .FirstOrDefault();

        if (next == null)
            return;

        //spot.GoToHiding();
        next.Hide(spot.hideLocation);
    }

    //returning the characters in order backwards so 3->1
    public void ReleaseLast(HidingSpot spot)
    {
        var last =
            characters
            .Where(c => c.IsHidden)
            .OrderByDescending(c => c.HideOrder)
            .FirstOrDefault();

        if (last == null)
            return;

        last.UnHide();
    }
}
