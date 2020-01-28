using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Group : Deployable
{

    private List<ClickableDeployment> ClickableDeployments;
    public GroupData BaseData;
    public Vector3 SpawnPointOffset;
    public int SpawnRadius;

    public override Vector3 ClickablesCenterPos()
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
        SpawnRadius = BaseData.GroupSpawnRadius;
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

    protected override void InstatiateChildren(GameObject gameobjectprefab, int amount)
    {
        ClickableDeployment clickable = gameobjectprefab.GetComponent<ClickableDeployment>();
        for (int i = 0; i < amount; i++)
        {
            Vector3 offset = new Vector3((i * clickable.SpawnRadius) - ((amount - 1) * clickable.SpawnRadius) / 2.0f, 0, 0);
            ClickableDeployment newclickable = Instantiate(clickable, this.transform.position + offset, Quaternion.identity, this.transform);
            newclickable.Init(this);
            ClickableDeployments.Add(newclickable);
        }
    }

    public override List<ClickableDeployment> GetAllClickables()
    {
        return ClickableDeployments;
    }
}
