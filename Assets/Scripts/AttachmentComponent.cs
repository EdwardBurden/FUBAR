using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentComponent : MonoBehaviour
{
    public List<Transform> AttachmentPoints;


    public void Attach(MoveableClickable clickableDeployment)
    {
        if (AttachmentPoints.Count > 0)
        {
            clickableDeployment.Attachment = this;
            AttachmentPoints.RemoveAt(0);
            AttachmentPoints[0];
        }

    }

    public void Detach(ClickableDeployment clickableDeployment)
    {


    }
}
