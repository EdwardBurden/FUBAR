using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flag : MonoBehaviour
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

    private void Update()
    {
        Deployable dep = DeployableRef.GetComponent<Deployable>();
        this.transform.position = new Vector3(dep.ClickablesCenterPos().x, this.transform.position.y, dep.ClickablesCenterPos().z);
    }

}

