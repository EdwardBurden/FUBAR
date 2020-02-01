using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : Deployable
{
    private List<Platoon> Platoons;
    public CompanyData Data;
    private Group CommandGroup;
    private Group DeputyCommandGroup;

    public void Init()
    {
        Platoons = new List<Platoon>();
        for (int i = 0; i < Data.Platoons.Count; i++)
        {
            Vector3 offset = new Vector3((i * Data.Platoons[i].radius) - ((Data.Platoons.Count - 1) * Data.Platoons[i].radius) / 2.0f, 0, 0);
            Platoon groupspawned = Instantiate(Data.Platoons[i], this.transform.position + offset, Quaternion.identity, this.transform);
            groupspawned.Init();
            Platoons.Add(groupspawned);
        }
        CommandGroup = Instantiate(Data.CommandGroup, this.transform.position, Quaternion.identity, this.transform);
        DeputyCommandGroup = Instantiate(Data.DeputyCommandGroup, this.transform.position, Quaternion.identity, this.transform);
    }

    public override Vector3 ClickablesCenterPos()
    {
        if (Platoons != null && Platoons.Count > 0)
        {
            Vector3 total = Vector3.zero;
            foreach (var item in Platoons)
            {
                total += item.ClickablesCenterPos();
            }
            total += CommandGroup.ClickablesCenterPos();
            total += DeputyCommandGroup.ClickablesCenterPos();
            return total / ((float)Platoons.Count + 2.0f);
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
        foreach (Platoon group in Platoons)
        {
            clickables.AddRange(group.GetAllClickables());
        }
        clickables.AddRange(CommandGroup.GetAllClickables());
        clickables.AddRange(DeputyCommandGroup.GetAllClickables());
        return clickables;
    }
}
