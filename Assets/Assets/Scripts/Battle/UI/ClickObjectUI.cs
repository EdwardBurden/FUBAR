using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FUBAR
{
    public class ClickObjectUI : MonoBehaviour
    {
        [SerializeField]
        private Image BackgroundImage;

        [SerializeField]
        private Text NameText;

        public void SetUI(string name)
        {
            BackgroundImage.gameObject.SetActive(true);
            NameText.text = name;
        }

        public void UnSetUI(string name)
        {
            BackgroundImage.gameObject.SetActive(false);
        }
    }
}
