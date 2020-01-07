using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SelectionType
{
    None,
    SingleUnit,
    SingleSquad,
    MultipleUnits
}

public struct RayCastInfo<T> where T : MonoBehaviour
{
    public bool Focused;
    public T FocusObject;
    public RaycastHit HitInfo;
    public RayCastInfo(bool focus, RaycastHit info, T focusobject)
    {
        Focused = focus;
        HitInfo = info;
        FocusObject = focusobject;
    }
}

public class SelectionController : MonoBehaviour
{

    public static SelectionController instance;
    [SerializeField]
    private RectTransform SelectSquareImage;

    private Vector3 SelectionImageStartPos;
    private Vector3 SelectionImageEndPos;
    private Vector3 SelectionMouseStartPos;
    private Vector3 SelectionMouseEndPos;

    public SelectionType CurrentSelection;

    public ClickableDeployment HoveredClickable;
    public List<ClickableDeployment> Selected;
    public Deployable SelectedDeployable;

    private void Start()
    {
        instance = this;
        CurrentSelection = SelectionType.None;
        Selected = new List<ClickableDeployment>();
        SelectSquareImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckHoverObject();
        CheckSelection();
        if (Input.GetMouseButtonUp(1))
            CheckForMovement();
    }

    private void CheckHoverObject()
    {
        RayCastInfo<ClickableDeployment> clickable = CheckIfObjectIsInFocus<ClickableDeployment>();
        if (clickable.Focused)
        {
            if (clickable.FocusObject != HoveredClickable)
            {
                if (HoveredClickable)
                    HoveredClickable.TriggerOffHover();
                clickable.FocusObject.TriggerOnHover();
                HoveredClickable = clickable.FocusObject;
            }
        }
        else HoveredClickable = null;
    }

    private void CheckSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectionImageStartPos = Input.mousePosition;
            SelectionMouseStartPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (HoveredClickable && !Selected.Contains(HoveredClickable))
                    HoveredClickable.TriggerOnAddedToSelection();
                else
                    HoveredClickable.TriggerDeselect();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SelectSquareImage.gameObject.SetActive(false);
                SelectionMouseEndPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (Mathf.Abs(SelectionImageStartPos.x - SelectionImageEndPos.x) > 30 && Mathf.Abs(SelectionImageStartPos.y - SelectionImageEndPos.y) > 30)
                    SelectObjectsFromBox();
                else
                {
                    if (HoveredClickable != null)
                    {
                        ResetSelection();
                        HoveredClickable.TriggerOnClick();
                    }
                    else
                        SelectNone();
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            SelectionImageEndPos = Input.mousePosition;
            if ((Mathf.Abs(SelectionImageStartPos.x - SelectionImageEndPos.x) > 30 && Mathf.Abs(SelectionImageStartPos.y - SelectionImageEndPos.y) > 30) || HoveredClickable == null)
            {
                if (!SelectSquareImage.gameObject.activeInHierarchy)
                    SelectSquareImage.gameObject.SetActive(true);
                SetSelectionImageSize();
            }


        }
    }


    private void SelectObjectsFromBox()
    {
        ResetSelection();
        Rect selectionrect = new Rect(SelectionMouseStartPos.x, SelectionMouseStartPos.y, SelectionMouseEndPos.x - SelectionMouseStartPos.x, SelectionMouseEndPos.y - SelectionMouseStartPos.y);
        List<ClickableDeployment> Searchable = FindObjectsOfType<ClickableDeployment>().ToList();
        foreach (ClickableDeployment item in Searchable)
        {
            if (selectionrect.Contains(Camera.main.WorldToViewportPoint(item.gameObject.transform.position), true))
                item.TriggerOnAddedToSelection();
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

    public void ClickableSelected(GameObject gameObject)
    {
        if (gameObject && gameObject.GetComponent<ClickableDeployment>())
        {
            Selected.Clear();
            Selected.Add(gameObject.GetComponent<DefaultUnit>());
            CurrentSelection = SelectionType.SingleUnit;
            //update ui
            //etc
        }
    }

    private void SelectNone()
    {
        CurrentSelection = SelectionType.None;
        UIController.instance.NukeIt();
    }

    public void ClickableSelectionAdded(GameObject gameObject)
    {
        if (gameObject && gameObject.GetComponent<ClickableDeployment>())
        {
            Selected.Add(gameObject.GetComponent<DefaultUnit>());
            CurrentSelection = SelectionType.MultipleUnits;
            //update ui
            //etc
        }
    }

    public void ClickableDeSelected(GameObject gameObject)
    {
        if (gameObject && gameObject.GetComponent<ClickableDeployment>())
        {
            Selected.Remove(gameObject.GetComponent<DefaultUnit>());
            if (Selected.Count == 0)
                SelectNone();
            //update ui
            //etc
        }
    }

    public void DeplyableSelected(GameObject gameObject)
    {
        //clicked select squad button on unit
        if (gameObject && gameObject.GetComponent<Deployable>())
        {
            ResetSelection();
            SelectedDeployable = gameObject.GetComponent<Deployable>();
            CurrentSelection = SelectionType.SingleSquad;
        }
    }

    public void CheckForMovement()
    {
        RayCastInfo<TerrainClickable> info = CheckIfObjectIsInFocus<TerrainClickable>();
        if (info.Focused)
        {
            switch (CurrentSelection)
            {
                case SelectionType.SingleUnit:
                    ((DefaultUnit)Selected[0]).MoveToTarget(info.HitInfo.point);
                    break;
                case SelectionType.SingleSquad:
                    SelectedDeployable.MoveToTarget(info.HitInfo.point);
                    break;
                case SelectionType.MultipleUnits:
                    foreach (ClickableDeployment item in Selected)
                    {
                        if (item is DefaultUnit)
                            ((DefaultUnit)item).MoveToTarget(info.HitInfo.point);
                    }
                    break;
                case SelectionType.None:
                    break;
                default:
                    break;
            }
        }
    }

    private void ResetSelection()
    {
        if (Selected != null && Selected.Count > 0)
            for (int i = 0; i < Selected.Count; i++)
            {
                if (Selected[i])
                    Selected[i].TriggerDeselect();
            }

        Selected.Clear();
        SelectNone();
    }

    private RayCastInfo<T> CheckIfObjectIsInFocus<T>() where T : MonoBehaviour
    {
        RaycastHit hit = new RaycastHit();
        bool inSight = false;
        T foundObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                T foundobject = raycasts[i].collider.gameObject.GetComponent<T>();
                if (foundobject)
                {
                    inSight = true;
                    hit = raycasts[i];
                    foundObject = foundobject;
                }
                else
                    inSight = false;
            }
        }
        return new RayCastInfo<T>(inSight, hit, foundObject);
    }
}
