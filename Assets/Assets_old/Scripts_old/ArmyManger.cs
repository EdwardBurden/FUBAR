using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManger : MonoBehaviour
{
    public Company CompanyPrefab;
    private Company Company;

    private void Start()
    {
        //CompanyControl = new Company(CompanyDataPrefab);
        Company = Instantiate(CompanyPrefab, this.transform);
        Company.Init();
    }


}
