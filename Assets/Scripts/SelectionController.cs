using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SelectionType
{
    None,
    SingleUnit,
    SingleSquad,
    MultipleUnits
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
    [SerializeField]
    private RectTransform SelectSquareImage;

    private Vector3 SelectionImageStartPos;
    private Vector3 SelectionImageEndPos;

    private Vector3 SelectionMouseStartPos;
    private Vector3 SelectionMouseEndPos;
    public SelectionType CurrentSelection;

    public List<ClickableDeployment> Selected;
    public Deployable SelectedDeployable;

    private bool SelectionInProgress;

    private void Start()
    {
        SelectionInProgress = false;
        instance = this;
        CurrentSelection = SelectionType.None;
        Selected = new List<ClickableDeployment>();
        SelectSquareImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        SelectionInProgress = false;
        if (Input.GetMouseButtonDown(0))
        {
            SelectionImageStartPos = Input.mousePosition;
            SelectionMouseStartPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectSquareImage.gameObject.SetActive(false);
            SelectionMouseEndPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (SelectionMouseStartPos != SelectionMouseEndPos)
                SelectObjectsFromBox();
        }

        if (Input.GetMouseButton(0))
        {
            if (!SelectSquareImage.gameObject.activeInHierarchy)
                SelectSquareImage.gameObject.SetActive(true);

            SelectionImageEndPos = Input.mousePosition;
            SetSelectionImageSize();

        }
    }

    private void SelectObjectsFromBox()
    {
        SelectionInProgress = true;
        Selected.Clear();
        CurrentSelection = SelectionType.MultipleUnits;
        Rect selectionrect = new Rect(SelectionMouseStartPos.x, SelectionMouseStartPos.y, SelectionMouseEndPos.x - SelectionMouseStartPos.x, SelectionMouseEndPos.y - SelectionMouseStartPos.y);
        List<ClickableDeployment> Searchable = FindObjectsOfType<ClickableDeployment>().ToList();
        foreach (ClickableDeployment item in Searchable)
        {
            if (selectionrect.Contains(Camera.main.WorldToViewportPoint(item.gameObject.transform.position), true))
            {
                Selected.Add(item);
            }
        }
    }

    private void SetSelectionImageSize()
    {
        Vector3 center = SelectionImageStartPos + SelectionImageEndPos / 2f;
        float sizex = Mathf.Abs(SelectionImageStartPos.x - SelectionImageEndPos.x);
        float sizey = Mathf.Abs(SelectionImageStartPos.y - SelectionImageEndPos.y);
        SelectSquareImage.sizeDelta = new Vector2(sizex, sizey);
        SelectSquareImage.position = (SelectionImageStartPos + SelectionImageEndPos) / 2.0f;

    }

    public bool IsSelectionFollowable()
    {
        return (CurrentSelection == SelectionType.SingleUnit && Selected.Count == 1);
    }

    public void SingleUnitSelected(GameObject gameObject)
    {
        if (gameObject && gameObject.GetComponent<Unit>())
        {
            Selected.Clear();
            Selected.Add(gameObject.GetComponent<Unit>());
            CurrentSelection = SelectionType.SingleUnit;
        }
    }

    public void SingleSquadSelected(GameObject gameObject)
    {
        //either selected all units in squad or clicked select squad button on unit
        if (gameObject && gameObject.GetComponent<Deployable>())
        {
            Selected.Clear();
            SelectedDeployable = gameObject.GetComponent<Deployable>();
            CurrentSelection = SelectionType.SingleSquad;
        }
    }

    public void OnTerrainClick(GameObject gameObject)
    {
        if (!SelectionInProgress) { 
        RayCastInfo info = IsTerrainFocusedOnClick();
            if (info.Focused)
            {
                switch (CurrentSelection)
                {
                    case SelectionType.SingleUnit:
                        ((Unit)Selected[0]).MoveToTarget(info.HitInfo.point);
                        break;
                    case SelectionType.SingleSquad:
                        SelectedDeployable.MoveToTarget(info.HitInfo.point);
                        break;
                    case SelectionType.MultipleUnits:
                        foreach (ClickableDeployment item in Selected)
                        {
                            if (item is Unit)
                                ((Unit)item).MoveToTarget(info.HitInfo.point);
                        }
                        break;
                    case SelectionType.None:
                        break;
                    default:
                        break;
                }
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
