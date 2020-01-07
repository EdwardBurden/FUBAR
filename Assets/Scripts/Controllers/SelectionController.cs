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

    public GameObjectUnityEvent OnClickEvent;
    public GameObjectUnityEvent OnAddedToSelectionEvent;
    public GameObjectUnityEvent OnDeselectEvent;
    public GameObjectUnityEvent OnAllDeselectedEvent;
    public GameObjectUnityEvent OnHoverEvent;
    public GameObjectUnityEvent OffHoverEvent;

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
                    ClickableOffHover(HoveredClickable);
                ClickableOnHover(clickable.FocusObject);
                HoveredClickable = clickable.FocusObject;
            }
        }
        else
        {
            if (HoveredClickable)
                ClickableOffHover(HoveredClickable);
            HoveredClickable = null;
        }
    }

    private void CheckSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectionImageStartPos = Input.mousePosition;
            SelectionMouseStartPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SelectSquareImage.gameObject.SetActive(false);
                if (Mathf.Abs(SelectionImageStartPos.x - SelectionImageEndPos.x) > 30 && Mathf.Abs(SelectionImageStartPos.y - SelectionImageEndPos.y) > 30)
                    SelectObjectsFromBox();
                else
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        if (HoveredClickable && !Selected.Contains(HoveredClickable))
                            ClickableSelectionAdded(HoveredClickable);
                        else
                            ClickableDeSelected(HoveredClickable);
                    }
                    else
                    {
                        if (HoveredClickable != null)
                            ClickableSelected(HoveredClickable);
                        else
                        {
                            RayCastInfo<TerrainClickable> clickable = CheckIfObjectIsInFocus<TerrainClickable>();
                            if (clickable.Focused)
                            {
                                ResetSelection();
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            SelectionImageEndPos = Input.mousePosition;
            SelectionMouseEndPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if ((Mathf.Abs(SelectionImageStartPos.x - SelectionImageEndPos.x) > 30 && Mathf.Abs(SelectionImageStartPos.y - SelectionImageEndPos.y) > 30))
            {
                if (!SelectSquareImage.gameObject.activeInHierarchy)
                    SelectSquareImage.gameObject.SetActive(true);
                SetSelectionImageSize();
                HoverObjectsFromBox();
            }


        }
    }

    private void SelectObjectsFromBox()
    {
        ResetSelection();
        Rect selectionrect = new Rect(SelectionMouseStartPos.x, SelectionMouseStartPos.y, SelectionMouseEndPos.x - SelectionMouseStartPos.x, SelectionMouseEndPos.y - SelectionMouseStartPos.y);
        List<ClickableDeployment> Searchable = FindObjectsOfType<ClickableDeployment>().ToList();
        List<ClickableDeployment> found = new List<ClickableDeployment>();
        foreach (ClickableDeployment item in Searchable)
        {
            if (selectionrect.Contains(Camera.main.WorldToViewportPoint(item.gameObject.transform.position), true))
                found.Add(item);
        }

        if (found != null && found.Count > 0)
        {
            if (found.Count > 1)
            {
                foreach (var founds in found)
                    ClickableSelectionAdded(founds);
            }
            else
                ClickableSelected(found.First());
        }
    }

    private void HoverObjectsFromBox()
    {
        Rect selectionrect = new Rect(SelectionMouseStartPos.x, SelectionMouseStartPos.y, SelectionMouseEndPos.x - SelectionMouseStartPos.x, SelectionMouseEndPos.y - SelectionMouseStartPos.y);
        List<ClickableDeployment> Searchable = FindObjectsOfType<ClickableDeployment>().ToList();
        foreach (ClickableDeployment item in Searchable)
        {
            if (selectionrect.Contains(Camera.main.WorldToViewportPoint(item.gameObject.transform.position), true))
                ClickableOnHover(item);
            else
                ClickableOffHover(item);
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

    public void ClickableSelected(ClickableDeployment clickable)
    {
        ResetSelection();
        clickable.TriggerOffHover();
        Selected.Add(clickable);
        clickable.TriggerOnClick();
        CurrentSelection = SelectionType.SingleUnit;
        OnClickEvent.Invoke(clickable.gameObject);
    }
    public void ClickableSelectionAdded(ClickableDeployment clickable)
    {
        clickable.TriggerOffHover();
        Selected.Add(clickable);
        clickable.TriggerOnAddedToSelection();
        CurrentSelection = SelectionType.MultipleUnits;
        OnAddedToSelectionEvent.Invoke(clickable.gameObject);
    }
    public void ClickableDeSelected(ClickableDeployment clickable)
    {
        Selected.Remove(clickable);
        clickable.TriggerDeselect();
        if (Selected.Count == 0)
        {
            ResetSelection();
        }
        OnDeselectEvent.Invoke(clickable.gameObject);
    }

    public void ClickableOnHover(ClickableDeployment clickable)
    {
        if (!Selected.Contains(clickable))
        {
            clickable.TriggerOnHover();
            OffHoverEvent.Invoke(clickable.gameObject);
        }
    }


    public void ClickableOffHover(ClickableDeployment clickable)
    {
        clickable.TriggerOffHover();
        OnHoverEvent.Invoke(clickable.gameObject);
    }


    public void DeplyableSelected(GameObject gameObject)
    {
        //clicked select squad button on unit
        if (gameObject && gameObject.GetComponent<Deployable>())
        {
            ResetSelection();
            SelectedDeployable = gameObject.GetComponent<Deployable>();
            CurrentSelection = SelectionType.SingleSquad;

            foreach (var item in SelectedDeployable.ClickableDeployments)
            {
                Selected.Add(item);
                item.TriggerOnAddedToSelection();
                OnAddedToSelectionEvent.Invoke(item.gameObject);
            }
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
        {
            foreach (var item in Selected)
            {
                if (item)
                {
                    item.TriggerDeselect();
                }
            }
        }
        Selected.Clear();
        CurrentSelection = SelectionType.None;
        OnAllDeselectedEvent.Invoke(null);
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
