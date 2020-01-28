using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Section")]
public class SectionData : ScriptableObject
{
    public List<Group> Groups;
    public string SectionName;
    public int radius;
}
