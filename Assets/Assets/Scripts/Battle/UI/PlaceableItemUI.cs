using System;
using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableItemUI : MonoBehaviour
{
    [SerializeField]
    private PlaceableEvent PlacementEvent;
    [SerializeField]
    private Button button;

    public void Init(BasePlacementData item)
    {
        button.onClick.RemoveAllListeners();
        button.GetComponentInChildren<Text>().text = item.Name;
        button.onClick.AddListener(delegate { PlacementEvent.Raise(item); });
    }
}
