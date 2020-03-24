using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Helpers;

namespace FUBAR
{
    public class PlacementController : MonoBehaviour
    {
        private List<GameObject> PlacementObject;
        private BasePlacementData SelectdObj;
        private Vector3 Center;
        private bool PlacementActive;
        public Transform PreviewTransform;

        private void Awake()
        {
            PlacementObject = new List<GameObject>();
            Init();
        }

        private void Init()
        {
            PlacementActive = false;
            if (PlacementObject.Count > 0)
            {
                foreach (var item in PlacementObject)
                {
                    Destroy(item);
                }
                PlacementObject.Clear();
            }
        }

        public void OnPlacementSelected(BasePlacementData placement)
        {
            Init();
            RayCastInfo<TerrainObject> clickable = Helpers.CheckIfObjectIsInFocus<TerrainObject>();
            if (clickable.Focused)
            {
                SelectdObj = placement;
                PlacementObject = SelectdObj.StartPlacement(clickable, PreviewTransform);
                PlacementActive = true;
            }
        }

        private void Update()
        {
            if (PlacementActive)
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
            SelectdObj.Place();
            Init();
        }

        private void HandlePlacementPreview()
        {
            RayCastInfo<TerrainObject> clickable = Helpers.CheckIfObjectIsInFocus<TerrainObject>();
            if (clickable.Focused)
            {
                SelectdObj.GeneratePlacementPreview(clickable, PlacementObject);
            }
        }
    }
}