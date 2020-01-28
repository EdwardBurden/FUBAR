using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Deployable : MonoBehaviour
{
    public DeployableFlagState DeployableType;
    public abstract Vector3 ClickablesCenterPos();
    protected abstract void InstatiateChildren(GameObject prefab, int amount);

    public abstract List<ClickableDeployment> GetAllClickables();
}
