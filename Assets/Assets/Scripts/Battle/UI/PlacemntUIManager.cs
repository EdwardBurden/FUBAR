using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FUBAR
{
    public class PlacemntUIManager : MonoBehaviour
    {
        [SerializeField]
        private PlaceableItemUI ButtonPrefab;
        [SerializeField]
        private Transform Objects;
        [SerializeField]
        private Transform Groups;


        void Start()
        {
            foreach (BasePlacementData item in AssetLoader.Placeables)
            {
                PlaceableItemUI b = null;
                if (item is GroupData)
                {
                    b = Instantiate(ButtonPrefab, Groups.transform);
                }
                else
                    b = Instantiate(ButtonPrefab, Objects.transform);
                b.Init(item);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}