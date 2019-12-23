using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SquadInfo" , menuName ="Squad/SquadInfo")]
public class SquadInfo : ScriptableObject
{
    public List<Unit> Units;
    public string SquadName = "";

}
