using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObjectUnityEvent SquadSelectedFromUnit;
    public Button Button;
    public GameObject SquadInfoPanel;
    public GameObject BuildingInfoPanel;
    public GameObject UnitInfoPanel;

    private Building LastSelectedBuilding;
    private Unit LastSelectedUnit;
    private Squad LastSelectedSquad;

    private void Start()
    {
        instance = this;
        SquadInfoPanel.SetActive(false);
        BuildingInfoPanel.SetActive(false);
        UnitInfoPanel.SetActive(false);
    }

    public void Refreshpanels()
    {
        RefreshBuildPanel();
        RefreshSquadPanel();
    }

    public void RefreshBuildPanel()
    {
        ClearBuildPanel();
        FillBuildInfoPanel(LastSelectedBuilding);
    }

    private void ClearBuildPanel()
    {
        BuildingInfoPanel.GetComponentInChildren<Text>().text = "";
        foreach (Transform item in BuildingInfoPanel.transform.GetChild(1))
        {
            Destroy(item.gameObject);
        }
    }

    public void RefreshSquadPanel()
    {
        ClearSquadPanel();
        FillSquadInfoPanel(LastSelectedSquad);
    }

    private void ClearSquadPanel()
    {
        SquadInfoPanel.GetComponentInChildren<Text>().text = "";
        foreach (Transform item in SquadInfoPanel.transform.GetChild(1))
        {
            Destroy(item.gameObject);
        }
    }

    private void FillBuildInfoPanel(Building selected)
    {
        BuildingInfoPanel.GetComponentInChildren<Text>().text = "Building name : " + selected.SelectableName;
        foreach (var item in selected.LocalOperations)
        {
            Button button = Instantiate(Button, BuildingInfoPanel.transform.GetChild(1));
            button.onClick.AddListener(() => item.Activate(selected));
            button.GetComponentInChildren<Text>().text = item.Title;
        }
    }

    private void FillUnitInfoPanel(Unit unit)
    {
        UnitInfoPanel.GetComponentInChildren<Text>().text = "Unit name : " + unit.unitname;
        UnitInfoPanel.GetComponentInChildren<Button>().onClick.AddListener(() => SquadSelectedFromUnit.Invoke(unit.SquadRef.gameObject));
    }

    private void FillSquadInfoPanel(Squad squad)
    {
        SquadInfoPanel.GetComponentInChildren<Text>().text = "Squad name : " + squad.SelectableName;
        foreach (var item in squad.LocalOperations)
        {
            Button button = Instantiate(Button, SquadInfoPanel.transform.GetChild(1));
            button.onClick.AddListener(() => item.Activate(squad));
            button.GetComponentInChildren<Text>().text = item.Title;
        }
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
        UnitInfoPanel.SetActive(false);
    }

    public void UnitSelected(GameObject gameObject)
    {
        Unit selectedunit = gameObject.GetComponent<Unit>();
        if (selectedunit)
        {
            LastSelectedUnit = selectedunit;
            FillUnitInfoPanel(LastSelectedUnit);
        }
        UnitInfoPanel.SetActive(true);
        SquadInfoPanel.SetActive(false);
        BuildingInfoPanel.SetActive(false);
    }

    public void SquadSelected(GameObject gameObject)
    {
        Squad squad = gameObject.GetComponent<Squad>();
        if (squad)
        {
            LastSelectedSquad = squad;
            ClearSquadPanel();
            FillSquadInfoPanel(LastSelectedSquad);
        }
        UnitInfoPanel.SetActive(false);
        SquadInfoPanel.SetActive(true);
        BuildingInfoPanel.SetActive(false);
    }
}
