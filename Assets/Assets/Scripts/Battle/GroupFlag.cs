using System;
using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.UI;

public class GroupFlag : MonoBehaviour
{
    public Button FlagButton;
    Group Group;
    public float Height;
    public GroupEvent GroupSelectedEvent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(this.transform.position - Camera.main.transform.position, Vector3.up);
        Vector3 sumPos = Vector3.zero;
        foreach (ClickObject item in Group.GetObjects())
        {
            sumPos += item.transform.position;
        }
        sumPos = sumPos / (Group.GetObjects().Count);
        transform.position = new Vector3(sumPos.x, Height, sumPos.z);
    }

    public void OnCLick()
    {
        if (Group != null)
            GroupSelectedEvent.Raise(Group);
    }

    internal void Init(Group group)
    {
        Group = group;
        FlagButton.image.sprite = Group.Data.Icon;
    }
}
