using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareFormation : BaseFormation
{
    public override List<Vector3> GetDragFormationPosition<T>(Vector3 start, Vector3 end, List<T> agents, int columns, int space)
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < agents.Count; i++)
        {

            Vector3 pos = CalcPosition(i, columns, space);
            Vector3 posss = new Vector3(pos.x, 0, pos.y);
            float angle = Vector3.SignedAngle(Vector3.right, (end - start).normalized, Vector3.up);
            Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * posss;
            positions.Add(rotatedVector + start);
        }
        return positions;
    }
 
    public override List<Vector3> GetFormationPosition<T>(Vector3 start, List<T> agents, int columns, int space)
    {
        int rows = agents.Count / columns;
        float offsetx = rows > 0 ? ((columns * space) - space) / 2 : ((agents.Count * space) - space) / 2;
        float offsetz = rows > 0 ? ((rows * space) - space) / 2 : 0;

        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < agents.Count; i++)
        {
            Vector3 pos = CalcPosition(i, columns, space);
            float angle = Vector3.SignedAngle(Vector3.right, (Camera.main.gameObject.transform.right).normalized, Vector3.up);
            Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(pos.x - offsetx, 0, pos.y + offsetz);
            positions.Add(new Vector3(start.x + rotatedVector.x, 0, start.z + rotatedVector.z));
        }
        return positions;
    }
}
