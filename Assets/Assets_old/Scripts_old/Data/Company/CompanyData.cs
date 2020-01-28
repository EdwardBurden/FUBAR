using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Company")]
public class CompanyData : ScriptableObject
{
    public List<Platoon> Platoons;
    public Group CommandGroup;
    public Group DeputyCommandGroup;
    public string CompanyName;
}
