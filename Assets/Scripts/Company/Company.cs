using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    private List<Platoon> Groups;
    public CompanyData Data;

    public void Init()
    {
        Groups = new List<Platoon>();
        for (int i = 0; i < Data.Platoons.Count; i++)
        {
            Vector3 offset = new Vector3((i * Data.Platoons[i].radius) - ((Data.Platoons.Count - 1) * Data.Platoons[i].radius) / 2.0f, 0, 0);
            Platoon groupspawned = Instantiate(Data.Platoons[i], this.transform.position + offset, Quaternion.identity, this.transform);
            groupspawned.Init();
            Groups.Add(groupspawned);
        }
        Instantiate(Data.CommandGroup, this.transform.position, Quaternion.identity, this.transform);
        Instantiate(Data.DeputyCommandGroup, this.transform.position, Quaternion.identity, this.transform);
    }

}
