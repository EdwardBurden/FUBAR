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

        private bool Clicked = false;


        public void Clickshow()
        {
            if (!Clicked)
            {
                this.gameObject.SetActive(true);
                Clicked = true;
            }
        }


        public void ClickHide()
        {
            if (Clicked)
            {
                this.gameObject.SetActive(false);
                Clicked = false;
            }
        }



        public void HoverShow()
        {
            if (!Clicked)
                this.gameObject.SetActive(true);
        }

        public void HoverHide()
        {
            if (!Clicked)
                this.gameObject.SetActive(false);
        }

        public void Init(LocalClickObjectData localClickObjectData)
        {
            NameText.text = localClickObjectData.Name;
            this.gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.LookAt((Camera.main.transform.forward * 1000), Vector3.up);


            //transform.rotation = Quaternion.LookRotation(this.transform.position - (Camera.main.transform.forward * -1000), Vector3.up);
        }
    }
}
