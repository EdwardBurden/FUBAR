using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectionType
{
    SingleUnit,
    SingleSquad,
    MultipleUnits,
    MultipleSquads
}

public class SelectionController : MonoBehaviour
{
    public static SelectionController instance;

    public SelectionType CurrentSelection;

    public GameObject UnitSelected { get { return SingleUnit; } }
    private GameObject SingleUnit;
    private GameObject SingleSquad;
    private void Start()
    {
        instance = this;
    }


    public bool IsSelectionFollowable()
    {
        return (CurrentSelection == SelectionType.SingleUnit && SingleUnit != null);
    }

    public void SingleUnitSelected(GameObject gameObject)
    {
        if (gameObject && gameObject.GetComponent<Unit>())
        {
            SingleUnit = gameObject;
            CurrentSelection = SelectionType.SingleUnit;
        }
    }

    public void SingleSquadSelected(GameObject gameObject)
    {
        //either selected all units in squad or clicked select squad button on unit
        if (gameObject && gameObject.GetComponent<Squad>())
        {
            SingleSquad = gameObject;
            CurrentSelection = SelectionType.SingleSquad;
        }

    }
}
