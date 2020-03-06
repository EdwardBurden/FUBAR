using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FUBAR
{
    public class PlacemntUIManager : MonoBehaviour
    {
        [SerializeField]
        private Button ButtonPrefab;

        void Start()
        {
            foreach (GroupData item in AssetLoader.Groups)
            {
                Instantiate(ButtonPrefab, this.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}