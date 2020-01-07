using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeployableFlag : MonoBehaviour
{
    public GameObject DeployableRef;
    public GameObjectUnityEvent OnClick;

    private void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnClick.Invoke(DeployableRef);
        }
    }
}
