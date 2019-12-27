using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button Button;
    public GameObject SquadInfoPanel;
    public GameObject BuildingInfoPanel;

    private void Start()
    {
        SquadInfoPanel.SetActive(false);
        BuildingInfoPanel.SetActive(false);
    }

    public void BuildingSelected(GameObject gameObject)
    {
        Building selectedbuilding = gameObject.GetComponent<Building>();
        if (selectedbuilding)
        {
            BuildingInfoPanel.GetComponentInChildren<Text>().text = "Building name : " + gameObject.GetComponent<Building>().BuildInfo.BuildingName;
            foreach (var item in selectedbuilding.BuildInfo.BuildOperations)
            {
                Button button = Instantiate(Button, BuildingInfoPanel.transform.GetChild(1));
                button.onClick.AddListener(() => item.Activate());
                button.GetComponentInChildren<Text>().text = item.Title;
            }
        }
        SquadInfoPanel.SetActive(false);
        BuildingInfoPanel.SetActive(true);
    }

    public void UnitSelected(GameObject gameObject)
    {
        SquadInfoPanel.SetActive(true);
        BuildingInfoPanel.SetActive(false);
    }
}
