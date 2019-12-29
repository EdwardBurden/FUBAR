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
    private struct RayCastInfo
    {
        public bool Focused;
        public RaycastHit HitInfo;
        public RayCastInfo(bool focus, RaycastHit info)
        {
            Focused = focus;
            HitInfo = info;
        }
    }


    public static SelectionController instance;

    public SelectionType CurrentSelection;

    public Unit UnitSelected { get { return SingleUnit; } }
    private Unit SingleUnit;
    private Deployable SingleSquad;
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
            SingleUnit = gameObject.GetComponent<Unit>();
            CurrentSelection = SelectionType.SingleUnit;
        }
    }

    public void SingleSquadSelected(GameObject gameObject)
    {
        //either selected all units in squad or clicked select squad button on unit
        if (gameObject && gameObject.GetComponent<Deployable>())
        {
            SingleSquad = gameObject.GetComponent<Deployable>();
            CurrentSelection = SelectionType.SingleSquad;
        }
    }

    public void OnTerrainClick(GameObject gameObject)
    {
        RayCastInfo info = IsTerrainFocusedOnClick();
        if (info.Focused)
        {
            switch (CurrentSelection)
            {
                case SelectionType.SingleUnit:
                    UnitSelected.MoveToTarget(info.HitInfo.point);
                    break;
                case SelectionType.SingleSquad:
                    SingleSquad.MoveToTarget(info.HitInfo.point);
                    break;
                case SelectionType.MultipleUnits:
                    break;
                case SelectionType.MultipleSquads:
                    break;
                default:
                    break;
            }

        }
    }

    private RayCastInfo IsTerrainFocusedOnClick()
    {
        bool terrainfocused = false;
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                if (raycasts[i].collider.gameObject.GetComponent<Terrain>())
                {
                    terrainfocused = true;
                    hit = raycasts[i];
                }
                else
                    terrainfocused = false;
            }
        }
        return new RayCastInfo(terrainfocused, hit);
    }
}
