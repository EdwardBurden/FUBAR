
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public class LocalClickObjectData
    {
        public string Name;
    }


    public abstract class ClickObject : MonoBehaviour
    {
        public LocalClickObjectData Data;
        [SerializeField]
        protected ClickObjectUI UIComponent;
        [SerializeField]
        protected GameObject PreviewObjectPrefab;

        public virtual void Init(LocalClickObjectData data)
        {
            Data = data;
            UIComponent.Init(data);
        }

        public GameObject GetPreviewObject()
        {
            return PreviewObjectPrefab;
        }

        public virtual void OnClick()
        {
            if (UIComponent) UIComponent.Clickshow();
        }

        internal virtual void OffHover()
        {
            if (UIComponent) UIComponent.HoverHide();
        }

        internal virtual void OnHover()
        {
            if (UIComponent) UIComponent.HoverShow();
        }

        internal virtual void OffClick()
        {
            if (UIComponent) UIComponent.ClickHide();
        }
    }
}