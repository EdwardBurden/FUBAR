using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Deployable : MonoBehaviour
{
    public GameObject flag;

    public virtual void Update()
    {
        flag.transform.position = new Vector3(ClickablesCenterPos().x, flag.transform.position.y, ClickablesCenterPos().z);
    }

    public List<ClickableDeployment> ClickableDeployments;
    public DeployableData BaseData;
    public Vector3 SpawnPointOffset;

    protected Vector3 ClickablesCenterPos()
    {
        if (ClickableDeployments != null && ClickableDeployments.Count > 0)
        {
            Vector3 total = Vector3.zero;
            foreach (var item in ClickableDeployments)
                total += item.gameObject.transform.position;
            return total / (float)ClickableDeployments.Count;
        }
        else return this.transform.position;
    }
    public Vector3 SpawnPoint
    {
        get
        {
            return transform.localPosition + SpawnPointOffset;
        }
    }

    public virtual void Start()
    {
        ClickableDeployments = new List<ClickableDeployment>();
        InitData();
        InitClickables();
    }
    protected abstract void InitClickables();
    protected abstract void InitData();

    protected void ClearClickables()
    {
        foreach (var item in ClickableDeployments)
        {
            Destroy(item.gameObject);
        }
        ClickableDeployments.Clear();
    }

    protected void InstatiateClickables(ClickableDeployment prefab, int amount)
    {
        if (BaseData != null)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector3 offset = new Vector3((i * prefab.SpawnRadius) - ((amount - 1) * prefab.SpawnRadius) / 2.0f, 0, 0);
                ClickableDeployment newclickable = Instantiate(prefab, this.transform.position + offset, Quaternion.identity, this.transform);
                newclickable.Init(this);
                ClickableDeployments.Add(newclickable);
            }
        }
    }
}
