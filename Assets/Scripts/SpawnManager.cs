using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private struct RayCastInfo
    {
        public bool Focused;
        public RaycastHit HitInfo;
        public RayCastInfo(bool focus, RaycastHit info)
        {
            Focused = focus;
            HitInfo = info;
        }
    }


    public GameObject PrefabSquad;
    public bool UnitSelected;

    public void OnSelect()
    {
        UnitSelected = true;
    }

    public void OnTerrainClick()
    {
        RayCastInfo info = IsTerrainFocusedOnClick();
        if (info.Focused && UnitSelected)
        {
            Instantiate(PrefabSquad, info.HitInfo.point, Quaternion.identity);
        }
    }

    private RayCastInfo IsTerrainFocusedOnClick()
    {
        bool terrainfocused = false;
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                if (raycasts[i].collider.gameObject.GetComponent<Terrain>())
                {
                    terrainfocused = true;
                    hit = raycasts[i];
                }
                else
                    terrainfocused = false;
            }
        }
        return new RayCastInfo(terrainfocused, hit);
    }


    public void OnDeselect()
    {
        UnitSelected = false;
    }
}
