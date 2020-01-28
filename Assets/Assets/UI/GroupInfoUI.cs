using System;
using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.UI;

public class GroupInfoUI : MonoBehaviour
{
    public ClickObjectEvent click;
    public Button buttonprefab;
    public Button OperationButtons;
    public Transform ClickList;
    public Transform Operations;
    internal void Init()
    {
        //throw new NotImplementedException();
    }

    internal void GroupSelected(FUBAR.Group group)
    {
        foreach (Transform item in ClickList)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in Operations)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in group.Operations)
        {
            Button b = Instantiate(OperationButtons, Operations);
          //  b.GetComponentInChildren<Text>().text = item.Name;
        }
        foreach (var item in group.GetObjects())
        {
            Button b = Instantiate(buttonprefab, ClickList);
            b.GetComponentInChildren<Text>().text = item.Name;
            b.onClick.AddListener(() => click.Raise(item));
        }

    }
}
