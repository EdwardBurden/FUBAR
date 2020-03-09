using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helpers;

namespace FUBAR
{
    [CreateAssetMenu(menuName = "New/BuildingPlacementData")]
    public class BuildingPlaceData : BasePlacementData
    {
        private void Awake()
        {
            Type = PlacementDataType.Object;
        }

        public int Length;
        public int Width;
        public ClickObject Building;
        public override  void GeneratePlacementPreview(RayCastInfo<TerrainObject> clickable, List<GameObject> previews)
        {
            float widthspace = 0.5f;
            PlacementPreviewCentre = new Vector3(Mathf.FloorToInt(clickable.HitInfo.point.x) + widthspace, (int)clickable.HitInfo.point.y, Mathf.FloorToInt(clickable.HitInfo.point.z) + widthspace);
            float offsetx = Length % 2 == 0 ? 0 : 0.5f;
            float offsetz = Width % 2 == 0 ? 0 : 0.5f;
            float startX = offsetx + PlacementPreviewCentre.x - (Length * 0.5f);
            float startZ = offsetz + PlacementPreviewCentre.z - (Width * 0.5f);
            int count = 0;
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Vector3 pos = new Vector3(startX + i, PlacementPreviewCentre.y, startZ + j);
                    previews[count].transform.position = pos;
                    count++;
                }
            }
        }

        public override void Place()
        {
            ArmyConstructor.Instance.AddObject(Building, PlacementPreviewCentre, Quaternion.identity);
        }

        public override List<GameObject> StartPlacement(RayCastInfo<TerrainObject> rayCastInfo, Transform PreviewTransform)
        {
            List<GameObject> PlacementObject = new List<GameObject>();
            Vector3 intPos = new Vector3((int)rayCastInfo.HitInfo.point.x, (int)rayCastInfo.HitInfo.point.y, (int)rayCastInfo.HitInfo.point.z);
            float widthspace = 0.5f;
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Vector3 pos = new Vector3(intPos.x - (Length / 2.0f) + (widthspace + i), intPos.y, intPos.z - (Width / 2.0f) + (widthspace + j));
                    PlacementObject.Add(Instantiate(Building.GetPreviewObject(), pos, Quaternion.identity, PreviewTransform));
                }
            }
            return PlacementObject;
        }
    }
}
