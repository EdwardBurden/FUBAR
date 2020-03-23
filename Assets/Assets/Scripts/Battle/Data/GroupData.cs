using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    [CreateAssetMenu(menuName = "New/GroupData")]
    public class GroupData : BasePlacementData
    {
        public ClickObject Objects;
        public int ObjectNumber;
        public List<Formationenum> Formations;
        public List<GroupOperation> Operations;
        public GroupFlag GroupFlag;
        public int DefaultSpace;
        public int DefaultColumns;

        public GameObject PlaceAblePreview;
        private void Awake()
        {
            Type = PlacementDataType.Group;
        }
        public override void GeneratePlacementPreview(Helpers.RayCastInfo<TerrainObject> clickable, List<GameObject> previews)
        {
            float widthspace = 0.5f;
            PlacementPreviewCentre = new Vector3(Mathf.FloorToInt(clickable.HitInfo.point.x) + widthspace, (int)clickable.HitInfo.point.y, Mathf.FloorToInt(clickable.HitInfo.point.z) + widthspace);
            previews[0].transform.position = PlacementPreviewCentre;
        }

        public override void Place()
        {
            ArmyConstructor.Instance.Addgroup(this, PlacementPreviewCentre, Quaternion.identity);
        }

        public override List<GameObject> StartPlacement(Helpers.RayCastInfo<TerrainObject> rayCastInfo, Transform PreviewTransform)
        {
            List<GameObject> PlacementObject = new List<GameObject>();
            Vector3 intPos = new Vector3((int)rayCastInfo.HitInfo.point.x, (int)rayCastInfo.HitInfo.point.y, (int)rayCastInfo.HitInfo.point.z);
            PlacementObject.Add(Instantiate(PlaceAblePreview, intPos, Quaternion.identity, PreviewTransform));
            return PlacementObject;
        }
    }
}