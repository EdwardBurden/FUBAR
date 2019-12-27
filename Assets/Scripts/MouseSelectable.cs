using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectable : MonoBehaviour
{
    public Transform SpawnPoint;
    public List<Operation> LocalOperations;
    public BaseInfo BaseInfo;
    public int HP;
    public string SelectableName;
    public GameObjectUnityEvent GameObjectUnityEvent;

    public virtual void Start()
    {
        InitData();
    }

    public void InitData() {
        if (BaseInfo)
        {
            LocalOperations = new List<Operation>(BaseInfo.Operations);
            HP = BaseInfo.HP;
            SelectableName = BaseInfo.Name;
        }

    }


    public virtual void OnMouseUpAsButton()
    {
        GameObjectUnityEvent.Invoke(this.gameObject);
    }

}
