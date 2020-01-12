using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platoon : MonoBehaviour
{
    private List<Section> Groups;
    public PlatoonData Data;
    public int radius;

    public void Init()
    {
        Groups = new List<Section>();
        for (int i = 0; i < Data.Sections.Count; i++)
        {
            Vector3 offset = new Vector3((i * Data.Sections[i].spawnradius) - ((Data.Sections.Count - 1) * Data.Sections[i].spawnradius) / 2.0f, 0, 0);
            Section groupspawned = Instantiate(Data.Sections[i], this.transform.position + offset, Quaternion.identity, this.transform);
            groupspawned.Init();
            Groups.Add(groupspawned);
        }
    }
}
