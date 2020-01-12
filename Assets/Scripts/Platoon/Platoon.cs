using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platoon : Deployable
{
    private List<Section> Sections;
    public PlatoonData Data;
    public int radius;

    public void Init()
    {
        Sections = new List<Section>();
        for (int i = 0; i < Data.Sections.Count; i++)
        {
            Vector3 offset = new Vector3((i * Data.Sections[i].spawnradius) - ((Data.Sections.Count - 1) * Data.Sections[i].spawnradius) / 2.0f, 0, 0);
            Section groupspawned = Instantiate(Data.Sections[i], this.transform.position + offset, Quaternion.identity, this.transform);
            groupspawned.Init();
            Sections.Add(groupspawned);
        }
    }

    public override Vector3 ClickablesCenterPos()
    {
        if (Sections != null && Sections.Count > 0)
        {
            Vector3 total = Vector3.zero;
            foreach (var item in Sections)
                total += item.ClickablesCenterPos();
            return total / (float)Sections.Count;
        }
        else return this.transform.position;
    }

    protected override void InstatiateChildren(GameObject prefab, int amount)
    {
        throw new System.NotImplementedException();
    }

    public override List<ClickableDeployment> GetAllClickables()
    {
        throw new System.NotImplementedException();
    }
}
