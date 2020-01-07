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
    private Deployable SelectedDeployable;

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

    private void FillDeployableInfoPanel(Deployable selected)
    {
        DeployableInfoPanel.GetComponentInChildren<Text>().text = "Building name : " + selected.DeploymentName;
        foreach (var item in selected.LocalOperations)
        {
            if (item.StateFlag == selected.CurrentState)
            {
                Button button = Instantiate(Button, DeployableInfoPanel.transform.GetChild(1));
                button.onClick.AddListener(() => item.Activate(selected));
                button.GetComponentInChildren<Text>().text = item.Title;
            }
        }
    }

    private void FillClickableDeploymentInfoPanel(ClickableDeployment clickableDeployment)
    {
        UnitInfoPanel.GetComponentInChildren<Text>().text = "Unit name : " + clickableDeployment.LocalName;
        UnitInfoPanel.GetComponentInChildren<Button>().onClick.AddListener(() => SquadSelectedFromUnit.Invoke(clickableDeployment.DeployableRef.gameObject));
        UnitInfoPanel.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = clickableDeployment.DeployableRef.DeploymentName;
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
        Deployable squad = gameObject.GetComponent<Deployable>();
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
