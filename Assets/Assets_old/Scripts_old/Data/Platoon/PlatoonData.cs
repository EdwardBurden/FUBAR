using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Platoon")]
public class PlatoonData : ScriptableObject
{
    public List<Section> Sections;
    public string PlatoonName;
}
