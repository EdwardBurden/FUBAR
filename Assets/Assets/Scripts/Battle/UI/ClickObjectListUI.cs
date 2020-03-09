using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FUBAR
{
    public class ClickObjectListUI : MonoBehaviour
    {
        public ClickObjectEvent click;
        public Button prefab;
        private List<ClickObject> clicks;

        public void Init()
        {
            clicks = new List<ClickObject>();
            this.gameObject.SetActive(false);
        }
        public void NewSelecetd(ClickObject clickObject)
        {
            clicks.Clear();
            clicks.Add(clickObject);
            UpdateList();
            if (!this.gameObject.activeInHierarchy)
                this.gameObject.SetActive(true);
        }

        internal void Hide()
        {
            clicks.Clear();
            UpdateList();
            if (this.gameObject.activeInHierarchy)
                this.gameObject.SetActive(false);
        }

        public void OnSelecetd(List<ClickObject> clickObject)
        {
            clicks.AddRange(clickObject);
            UpdateList();
            if (!this.gameObject.activeInHierarchy)
                this.gameObject.SetActive(true);
        }

        public void OnDeselecetd(ClickObject clickObject)
        {
            clicks.Remove(clickObject);
            UpdateList();
            if (clicks.Count == 0)
                this.gameObject.SetActive(false);
        }

        private void UpdateList()
        {
            foreach (Transform item in this.transform)
            {
                Destroy(item.gameObject);
            }

            foreach (ClickObject item in clicks)
            {
                Button b = Instantiate(prefab, this.transform);
                b.GetComponentInChildren<Text>().text = item.Data.Name;
                b.onClick.AddListener(() => click.Raise(item));
            }

        }
    }
}
