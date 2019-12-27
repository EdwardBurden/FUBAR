using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Button Button;
    public GameObject SquadInfoPanel;
    public GameObject BuildingInfoPanel;

    private Building LastSelectedBuilding;

    private void Start()
    {
        instance = this;
        SquadInfoPanel.SetActive(false);
        BuildingInfoPanel.SetActive(false);
    }

    public void RefreshBuildPanel()
    {
        ClearBuildPanel();
        FillBuildInfoPanel(LastSelectedBuilding);
    }

    public void BuildingSelected(GameObject gameObject)
    {
        Building selectedbuilding = gameObject.GetComponent<Building>();
        if (selectedbuilding)
        {
            LastSelectedBuilding = selectedbuilding;
            ClearBuildPanel();
            FillBuildInfoPanel(LastSelectedBuilding);
        }
        SquadInfoPanel.SetActive(false);
        BuildingInfoPanel.SetActive(true);
    }

    private void ClearBuildPanel()
    {
        BuildingInfoPanel.GetComponentInChildren<Text>().text = "";
        foreach (Transform item in BuildingInfoPanel.transform.GetChild(1))
        {
            Destroy(item.gameObject);
        }
    }

    private void FillBuildInfoPanel(Building selected)
    {
        BuildingInfoPanel.GetComponentInChildren<Text>().text = "Building name : " + selected.BuildingName;
        foreach (var item in selected.LocalOperations)
        {
            Button button = Instantiate(Button, BuildingInfoPanel.transform.GetChild(1));
            button.onClick.AddListener(() => item.Activate());
            button.GetComponentInChildren<Text>().text = item.Title;
        }
    }

    public void UnitSelected(GameObject gameObject)
    {
        SquadInfoPanel.SetActive(true);
        BuildingInfoPanel.SetActive(false);
    }
}
