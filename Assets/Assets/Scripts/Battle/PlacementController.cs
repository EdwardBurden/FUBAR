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
        private ClickObject SelectdObj;
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

        public void OnPlacementSelected(ClickObject placement)
        {
            RayCastInfo<TerrainObject> clickable = Helpers.CheckIfObjectIsInFocus<TerrainObject>();
            if (clickable.Focused)
            {
                Vector3 intPos = new Vector3((int)clickable.HitInfo.point.x, (int)clickable.HitInfo.point.y, (int)clickable.HitInfo.point.z);
                SelectdObj = placement;
                Building building = SelectdObj.GetComponent<Building>();
                if (building)
                {

                    float widthspace = 0.5f;
                    for (int i = 0; i < building.Length; i++)
                    {
                        for (int j = 0; j < building.Width; j++)
                        {
                            Vector3 pos = new Vector3(intPos.x - (building.Length / 2.0f) + (widthspace + i), intPos.y, intPos.z - (building.Width / 2.0f) + (widthspace + j));
                            PlacementObject.Add(Instantiate(SelectdObj.GetPreviewObject(), pos, Quaternion.identity, PreviewTransform));
                        }
                    }
                }
                PlacementObject.Add(Instantiate(SelectdObj.GetPreviewObject(), intPos + Vector3.up, Quaternion.identity, PreviewTransform));
                PlacementActive = true;
            }
        }

        private void Update()
        {
            if (PlacementActive)
            {
                HandlePlacementPreview();

                if (Input.GetKeyDown(KeyCode.R))
                {

                }

                if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    HandlePlacement();
                }
            }
        }

        private void HandlePlacement()
        {
            ArmyConstructor.Instance.AddObject(SelectdObj, Center, Quaternion.identity);
            Init();
        }

        private void HandlePlacementPreview()
        {
            RayCastInfo<TerrainObject> clickable = Helpers.CheckIfObjectIsInFocus<TerrainObject>();
            if (clickable.Focused)
            {
                float widthspace = 0.5f;
                Vector3 intPos = new Vector3(Mathf.FloorToInt(clickable.HitInfo.point.x) + widthspace, (int)clickable.HitInfo.point.y, Mathf.FloorToInt(clickable.HitInfo.point.z) + widthspace);
                Building building = SelectdObj.GetComponent<Building>();
                if (building)
                {
                    float offset = building.Length % 2 == 0 ? 0 : 0.5f;
                    float startX = offset + intPos.x - (building.Length * 0.5f);
                    float startZ = offset + intPos.z - (building.Length * 0.5f);
                    int count = 0;
                    for (int i = 0; i < building.Length; i++)
                    {
                        for (int j = 0; j < building.Width; j++)
                        {
                            //  int X = intPos.x
                            Vector3 pos = new Vector3(startX + i, intPos.y, startZ + j);
                            PlacementObject[count].transform.position = pos;
                            count++;
                        }
                    }
                }
                PlacementObject[PlacementObject.Count - 1].transform.position = intPos + Vector3.up;
                Center = intPos;
            }
        }
    }
}