using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helpers;

namespace FUBAR
{
    [CreateAssetMenu(menuName = "New/UnitPlacementData")]
    public class UnitPlacementData : BasePlacementData
    {
        private void Awake()
        {
            Type = PlacementDataType.Object;
        }
        public ClickObject Unit;
        public override void GeneratePlacementPreview(RayCastInfo<TerrainObject> clickable, List<GameObject> previews)
        {
            float widthspace = 0.5f;
            PlacementPreviewCentre = new Vector3(Mathf.FloorToInt(clickable.HitInfo.point.x) + widthspace, (int)clickable.HitInfo.point.y, Mathf.FloorToInt(clickable.HitInfo.point.z) + widthspace);
            previews[0].transform.position = PlacementPreviewCentre;
        }

        public override void Place()
        {
            ArmyConstructor.Instance.AddObject(Unit, PlacementPreviewCentre, Quaternion.identity);
        }

        public override List<GameObject> StartPlacement(RayCastInfo<TerrainObject> rayCastInfo, Transform PreviewTransform)
        {
            List<GameObject> PlacementObject = new List<GameObject>();
            Vector3 intPos = new Vector3((int)rayCastInfo.HitInfo.point.x, (int)rayCastInfo.HitInfo.point.y, (int)rayCastInfo.HitInfo.point.z);
            PlacementObject.Add(Instantiate(Unit.GetPreviewObject(), intPos, Quaternion.identity, PreviewTransform));
            return PlacementObject;
        }
    }
}
