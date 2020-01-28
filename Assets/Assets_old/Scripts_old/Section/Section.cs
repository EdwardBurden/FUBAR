using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : Deployable
{
    private List<Group> Groups;
    public SectionData Data;
    public int spawnradius;

    public void Init()
    {
        Groups = new List<Group>();
        for (int i = 0; i < Data.Groups.Count; i++)
        {
            Vector3 offset = new Vector3((i * Data.Groups[i].BaseData.GroupSpawnRadius) - ((Data.Groups.Count - 1) * Data.Groups[i].BaseData.GroupSpawnRadius) / 2.0f, 0, 0);
            Group groupspawned = Instantiate(Data.Groups[i], this.transform.position + offset, Quaternion.identity, this.transform);
            Groups.Add(groupspawned);
        }
    }

    public override Vector3 ClickablesCenterPos()
    {
        if (Groups != null && Groups.Count > 0)
        {
            Vector3 total = Vector3.zero;
            foreach (var item in Groups)
                total += item.ClickablesCenterPos();
            return total / (float)Groups.Count;
        }
        else return this.transform.position;
    }

    protected override void InstatiateChildren(GameObject prefab, int amount)
    {
        throw new System.NotImplementedException();
    }

    public override List<ClickableDeployment> GetAllClickables()
    {
        List<ClickableDeployment> clickables = new List<ClickableDeployment>();
        foreach (Group group in Groups)
        {
            clickables.AddRange(group.GetAllClickables());
        }
        return clickables;
    }
}
