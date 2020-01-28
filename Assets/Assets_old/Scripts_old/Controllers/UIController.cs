using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObjectUnityEvent SquadSelectedFromUnit;
    public Button Button;
    public GameObject DeployableInfoPanel;
    public GameObject UnitInfoPanel;

    private ClickableDeployment SelectedClickableDeployment;
    private Group SelectedDeployable;

    private void Start()
    {
        instance = this;
        DeployableInfoPanel.SetActive(false);
        UnitInfoPanel.SetActive(false);
    }

    public void NukeIt()
    {
        SelectedDeployable = null;
        SelectedClickableDeployment = null;
        RefreshDeployablePanel();
        RefreshUnitPanel();
    }

    public void Refreshpanels()
    {
        RefreshDeployablePanel();
        RefreshUnitPanel();
    }

    public void RefreshDeployablePanel()
    {
        ClearDeployablePanel();
        if (SelectedDeployable)
            FillDeployableInfoPanel(SelectedDeployable);
    }

    private void ClearDeployablePanel()
    {
        DeployableInfoPanel.GetComponentInChildren<Text>().text = "";
        foreach (Transform item in DeployableInfoPanel.transform.GetChild(1))
        {
            Destroy(item.gameObject);
        }
    }

    public void RefreshUnitPanel()
    {
        ClearUnitPanel();
        if (SelectedClickableDeployment)
            FillClickableDeploymentInfoPanel(SelectedClickableDeployment);
    }

    private void ClearUnitPanel()
    {
        UnitInfoPanel.GetComponentInChildren<Text>().text = "";
        UnitInfoPanel.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "";
    }

    private void FillDeployableInfoPanel(Group selected)
    {
        SwitchableGroup group = selected.GetComponent<SwitchableGroup>();
        if (group)
        {
            DeployableInfoPanel.GetComponentInChildren<Text>().text = "Building name : " + group.LocalData.DeployableName;
            foreach (var item in group.LocalData.Operations)
            {
                if (item.StateFlag == group.LocalData.CurrentState)
                {
                    Button button = Instantiate(Button, DeployableInfoPanel.transform.GetChild(1));
                    button.onClick.AddListener(() => item.Activate(selected));
                    button.GetComponentInChildren<Text>().text = item.Title;
                }
            }
        }
        else
        {
            StaticGroup staticgroup = selected.GetComponent<StaticGroup>();
            if (staticgroup)
            {
                DeployableInfoPanel.GetComponentInChildren<Text>().text = "Building name : " + staticgroup.LocalData.DeployableName;
                foreach (var item in staticgroup.LocalData.Operations)
                {
                    Button button = Instantiate(Button, DeployableInfoPanel.transform.GetChild(1));
                    button.onClick.AddListener(() => item.Activate(selected));
                    button.GetComponentInChildren<Text>().text = item.Title;
                }
            }
        }
    }

    private void FillClickableDeploymentInfoPanel(ClickableDeployment clickableDeployment)
    {
        UnitInfoPanel.GetComponentInChildren<Text>().text = "Unit name : " + clickableDeployment.LocalName;
        UnitInfoPanel.GetComponentInChildren<Button>().onClick.AddListener(() => SquadSelectedFromUnit.Invoke(clickableDeployment.DeployableRef.gameObject));
        SwitchableGroup group = clickableDeployment.DeployableRef.GetComponent<SwitchableGroup>();
        if (group)
        {
            UnitInfoPanel.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = group.LocalData.DeployableName;
        }
        else
        {
            StaticGroup staticgroup = clickableDeployment.DeployableRef.GetComponent<StaticGroup>();
            if (staticgroup)
            {
                UnitInfoPanel.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = staticgroup.LocalData.DeployableName;
            }
        }
    }

    public void OnClickableDeplymentSelected(GameObject gameObject)
    {
        ClickableDeployment selectedunit = gameObject.GetComponent<ClickableDeployment>();
        if (selectedunit)
        {
            SelectedClickableDeployment = selectedunit;
            ClearUnitPanel();
            FillClickableDeploymentInfoPanel(SelectedClickableDeployment);
        }
        UnitInfoPanel.SetActive(true);
        DeployableInfoPanel.SetActive(false);
    }

    public void OnDeployableSelected(GameObject gameObject)
    {
        Group squad = gameObject.GetComponent<Group>();
        if (squad)
        {
            SelectedDeployable = squad;
            ClearDeployablePanel();
            FillDeployableInfoPanel(SelectedDeployable);
        }
        UnitInfoPanel.SetActive(false);
        DeployableInfoPanel.SetActive(true);
    }
}
