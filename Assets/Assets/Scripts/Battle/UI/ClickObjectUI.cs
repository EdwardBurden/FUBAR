using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FUBAR
{
    public class ClickObjectUI : MonoBehaviour
    {

        [SerializeField]
        private Text NameText;

        public void Show()
        {
            this.gameObject.SetActive(true);

        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Init(LocalClickObjectData localClickObjectData)
        {
            NameText.text = localClickObjectData.Name;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(this.transform.position - Camera.main.transform.position, Vector3.up);
        }
    }
}
