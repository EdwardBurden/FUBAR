using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainspawner : MonoBehaviour
{

    public GameObject Terrain;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(Terrain);
            this.gameObject.AddComponent<Rigidbody>();
        }
    }
}
