using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum DeployableFlagState
{
    Group,
    Section,
    Platoon,
    Company
}

public class ChangeFlag : MonoBehaviour
{
    public Button Group;
    public Button Section;
    public Button Platoon;
    public Button Company;

    public DeployableFlagState State;

    public void ChangeState(int newint)
    {
        DeployableFlagState newstate = (DeployableFlagState)newint;
        List<Flag> flags = (FindObjectsOfTypeAll(typeof(Flag)) as Flag[]).ToList();
        foreach (Flag item in flags)
        {
            item.gameObject.SetActive(false);
            switch (newstate)
            {
                case DeployableFlagState.Group:
                    if (item.DeployableRef.GetComponent<Group>())
                        item.gameObject.SetActive(true);
                    break;
                case DeployableFlagState.Section:
                    if (item.DeployableRef.GetComponent<Section>())
                        item.gameObject.SetActive(true);
                    break;
                case DeployableFlagState.Platoon:
                    if (item.DeployableRef.GetComponent<Platoon>())
                        item.gameObject.SetActive(true);
                    break;
                case DeployableFlagState.Company:
                    if (item.DeployableRef.GetComponent<Company>())
                        item.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }


    }
}
