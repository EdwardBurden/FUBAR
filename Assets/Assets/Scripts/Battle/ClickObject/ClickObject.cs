
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

    public abstract class ClickObject : MonoBehaviour
    {
        [SerializeField]
        private Health HealthComponent;

        [SerializeField]
        private ClickObjectUI UIComponent;

        [SerializeField]
        private GameObject PreviewObject;

        private Group GroupReferance;

        public ClickObjectType ClickType;

        public string Name = "";

        public virtual void Init(Group group)
        {
            GroupReferance = group;
            Name = "Unit" + Random.Range(0, 100);
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
            // if (UIComponent) UIComponent.UnSetUI("Hover");
        }

        internal virtual void OnHover()
        {
            // if (UIComponent) UIComponent.SetUI("Hover");
        }

        internal virtual void OffClick()
        {
            if (UIComponent) UIComponent.UnSetUI("Click");
        }
    }
}