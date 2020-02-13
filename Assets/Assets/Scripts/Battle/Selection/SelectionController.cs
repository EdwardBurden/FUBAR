using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FUBAR
{
    public class SelectionController : MonoBehaviour
    {
        public ClickObjectListEvent ClickObjectSelectedEvent;
        public ClickObjectEvent ClickObjectDeselectedEvent;
        public ClickObjectEvent NewClickObjectSelectionEvent;
        public ClickObjectEvent NoneSelected;

        [SerializeField]
        private RectTransform SelectSquareImage;

        [SerializeField]
        private Transform PreviewTransform;

        [SerializeField]
        private ArmyConstructor Army;

        private Vector3 SelectionImageStartPos;
        private Vector3 SelectionImageEndPos;
        private Vector3 SelectionMouseStartPos;
        private Vector3 SelectionMouseEndPos;

        public ClickObject HoverObject;

        private ObjectSelectionManager OSManager;
        private GroupSelectionManager GSManager;

        private SelectionState State;

        public void PreviewMovementOrder(PreviewOrder order)
        {
            if (State != null)
                State.GeneratePreview(order);
        }

        public void BeginPreviewOrder(PreviewOrder order)
        {

            if (State != null)
                State.BeginPreview(order);
        }
        public void EndPreviewOrder(PreviewOrder order)
        {

            if (State != null)
                State.EndPreview(order);
        }


        public void Move(Order order)
        {
            if (State != null)
                State.Move((MoveOrder)order);
        }

        public void Attack(Order order)
        {
            if (State != null)
                State.Attack((AttackOrder)order);
        }

        public void Attch(Order order)
        {
            if (State != null)
                State.Attach((AttachOrder)order);
        }

        public void OnGroupSelectionChange(Group group)
        {
            OSManager.ResetSelection();
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (group != null && !GSManager.GetGroup().Contains(group))
                    GSManager.SelectionAdded(group);
                else
                    GSManager.SelectionRemoved(group);
            }
            else
                GSManager.GroupSelected(group);
            ChangeState(new GroupState(GSManager));
        }

        public void OnObjectSelected(ClickObject obj)//called from ui event clicks
        {
            GSManager.ResetSelection();
            OSManager.NewSelection(obj);
            ChangeState(new ObjectState(OSManager));
        }

        private void ChangeState(SelectionState state)
        {
            if (State == null || state == null || State.GetType() != state.GetType())
            {
                if (State != null)
                    State.StateLost();
                State = state;
                if (State != null)
                    State.Init();
            }
        }

        public void Init()
        {
            OSManager = new ObjectSelectionManager();
            GSManager = new GroupSelectionManager();
            SelectSquareImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            CheckHoverObject();
            CheckSelection();
        }

        private void CheckHoverObject()
        {
            RayCastInfo<ClickObject> clickable = Helpers.CheckIfObjectIsInFocus<ClickObject>();
            if (clickable.Focused)
            {
                if (clickable.FocusObject != HoverObject)
                {
                    if (HoverObject)
                        HoverObject.OffHover();
                    HoverObject = clickable.FocusObject;
                    HoverObject.OnHover();
                }
            }
            else
            {
                if (HoverObject)
                    HoverObject.OffHover();
                HoverObject = null;
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
                RayCastInfo<TerrainObject> clickable = Helpers.CheckIfObjectIsInFocus<TerrainObject>();
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    SelectSquareImage.gameObject.SetActive(false);
                    if (Mathf.Abs(SelectionImageStartPos.x - SelectionImageEndPos.x) > 30 && Mathf.Abs(SelectionImageStartPos.y - SelectionImageEndPos.y) > 30)
                        SelectObjectsFromBox();
                    else
                    {
                        if (Input.GetKey(KeyCode.LeftControl) && !clickable.Focused)
                        {
                            if (HoverObject && !OSManager.GetSelectedObjects().Contains(HoverObject))
                            {
                                OSManager.SelectionAdded(HoverObject);
                                List<ClickObject> added = new List<ClickObject>();
                                added.Add(HoverObject);
                                ClickObjectSelectedEvent.Raise(added);
                            }
                            else
                            {
                                OSManager.SelectionRemoved(HoverObject);
                                ClickObjectDeselectedEvent.Raise(HoverObject);
                            }
                            ChangeState(new ObjectState(OSManager));
                        }
                        else
                        {
                            if (HoverObject != null && !clickable.Focused)
                            {
                                OSManager.NewSelection(HoverObject);
                                NewClickObjectSelectionEvent.Raise(HoverObject);
                                ChangeState(new ObjectState(OSManager));
                            }
                            else
                            {
                                if (clickable.Focused)
                                {
                                    OSManager.ResetSelection();
                                    NoneSelected.Raise(null);
                                    ChangeState(null);
                                }
                            }
                        }
                    }
                }
            }

            if (Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0))
            {
                SelectionImageEndPos = Input.mousePosition;
                SelectionMouseEndPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if ((Mathf.Abs(SelectionImageStartPos.x - SelectionImageEndPos.x) > 30 && Mathf.Abs(SelectionImageStartPos.y - SelectionImageEndPos.y) > 30) || HoverObject == null)
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
            OSManager.ResetSelection();
            NoneSelected.Raise(null);
            ChangeState(null);
            Rect selectionrect = new Rect(SelectionMouseStartPos.x, SelectionMouseStartPos.y, SelectionMouseEndPos.x - SelectionMouseStartPos.x, SelectionMouseEndPos.y - SelectionMouseStartPos.y);
            List<ClickObject> Searchable = Army.Objects();
            List<ClickObject> found = new List<ClickObject>();
            foreach (ClickObject item in Searchable)
            {
                if (selectionrect.Contains(Camera.main.WorldToViewportPoint(item.gameObject.transform.position), true))
                    found.Add(item);
            }
            if (found != null && found.Count > 0)
            {
                foreach (var founds in found)
                {
                    founds.OffHover();
                    OSManager.SelectionAdded(founds);

                }
                ClickObjectSelectedEvent.Raise(found); //change to pass list at end
                ChangeState(new ObjectState(OSManager));
            }
        }

        private void HoverObjectsFromBox()
        {
            Rect selectionrect = new Rect(SelectionMouseStartPos.x, SelectionMouseStartPos.y, SelectionMouseEndPos.x - SelectionMouseStartPos.x, SelectionMouseEndPos.y - SelectionMouseStartPos.y);
            List<ClickObject> Searchable = Army.Objects();
            foreach (ClickObject item in Searchable)
            {
                if (selectionrect.Contains(Camera.main.WorldToViewportPoint(item.gameObject.transform.position), true))
                    item.OnHover();
                else
                    item.OffHover();
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
    }
}