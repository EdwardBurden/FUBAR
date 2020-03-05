using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FUBAR
{
    public class PlacementController : MonoBehaviour
    {
        private GameObject PlacementObject;
        private ClickObject Obj;
        private bool PlacementActive;
        public Transform PreviewTransform;


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            PlacementActive = false;
            if (PlacementObject)
            {
                Destroy(PlacementObject);
                PlacementObject = null;
            }
        }

        public void OnPlacementSelected(ClickObject placement)
        {
            RayCastInfo<TerrainObject> clickable = Helpers.CheckIfObjectIsInFocus<TerrainObject>();
            if (clickable.Focused)
            {
                Obj = placement;
                PlacementObject = Instantiate(Obj.GetPreviewObject(), clickable.HitInfo.point, Quaternion.identity, PreviewTransform);
                PlacementActive = true;
            }
        }

        private void Update()
        {
            if (PlacementActive && PlacementObject)
            {
                HandlePlacementPreview();
                if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    HandlePlacement();
                }
            }
        }

        private void HandlePlacement()
        {
            ArmyConstructor.Instance.AddObject(Obj, PlacementObject.transform);
            Init();
        }

        private void HandlePlacementPreview()
        {
            RayCastInfo<TerrainObject> clickable = Helpers.CheckIfObjectIsInFocus<TerrainObject>();
            if (clickable.Focused)
            {
                PlacementObject.transform.position = clickable.HitInfo.point;
            }
        }
    }
}