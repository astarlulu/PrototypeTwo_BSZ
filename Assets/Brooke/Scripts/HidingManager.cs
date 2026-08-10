using System;
using System.Linq;
using UnityEngine;

public class HidingManager : MonoBehaviour
{
    //attach all characters
    [SerializeField] private HideableCharacters[] characters;
    [SerializeField] private KidnapManager kidnapManager;

    //going through the order of the characters and hiding them in the hiding spot 1->3
    public void HideNext(HidingSpot spot)
    {

        var next =
            characters
            .Where(c => !c.IsHidden)
            .Where(c => c != characters[0] || !kidnapManager.baraIsKidnapped)
            .OrderBy(c => c.HideOrder)
            .FirstOrDefault();

        if (next == null)
            return;

        //if (kidnapManager.baraIsKidnapped && next == characters[0])
        //    return;

        //spot.GoToHiding();
        next.Hide(spot.hideLocation);
    }

    //returning the characters in order backwards so 3->1
    public void ReleaseLast(HidingSpot spot)
    {
        var last =
            characters
            .Where(c => c.IsHidden)
            .Where(c => c != characters[0] || !kidnapManager.baraIsKidnapped)
            .OrderByDescending(c => c.HideOrder)
            .FirstOrDefault();

        if (last == null)
            return;

        //if (kidnapManager.baraIsKidnapped && last == characters[0])
        //    return;

        last.UnHide();
    }
}
