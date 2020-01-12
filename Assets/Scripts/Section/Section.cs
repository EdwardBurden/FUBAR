using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
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
}
