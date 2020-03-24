using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helpers;

namespace FUBAR
{
    public enum PlacementDataType
    {
        Group,
        Object
    }

    public abstract class BasePlacementData : ScriptableObject
    {
        public float Cost;
        protected PlacementDataType Type;
        public Sprite Icon;
        public string Name;
        protected Vector3 PlacementPreviewCentre;
        public abstract List<GameObject> StartPlacement(RayCastInfo<TerrainObject> rayCastInfo, Transform PreviewTransform);
        public abstract void GeneratePlacementPreview(RayCastInfo<TerrainObject> clickable, List<GameObject> previews);
        public abstract void Place();
    }
}
