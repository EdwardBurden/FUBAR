
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public enum ClickObjectType
    {
        Moveable,
        Destructable,
        Attachable
    }

    public class LocalClickObjectData
    {
        public string Name;
    }


    public abstract class ClickObject : MonoBehaviour
    {
        public bool Friendly;
        public LocalClickObjectData Data;
        [SerializeField]
        private ClickObjectUI UIComponent;
        [SerializeField]
        private GameObject PreviewObject;
        public ClickObjectType ClickType;

        public virtual void Init(LocalClickObjectData data)
        {
            Data = data;
            //     Name = "Unit" + Random.Range(0, 100);
        }

        public GameObject GetPreviewObject()
        {
            return PreviewObject;
        }

        public virtual void OnClick()
        {
            if (UIComponent) UIComponent.SetUI("Click");
        }

        internal virtual void OffHover()
        {
            if (UIComponent) UIComponent.UnSetUI("Hover");
        }

        internal virtual void OnHover()
        {
            if (UIComponent) UIComponent.SetUI("Hover");
        }

        internal virtual void OffClick()
        {
            if (UIComponent) UIComponent.UnSetUI("Click");
        }
    }
}