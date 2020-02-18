using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{
    public enum Formation
    {
        Square,
        Line
    }
    public static class Formations
    {

        public static int GetColumnsFromDistance(Vector3 start, Vector3 end, int space = Constants.K_DefaultSpace)
        {
            float endDiffference = Vector3.Distance(end, start);
            return ((int)endDiffference / space) + 1;//5 = space
        }

        public static List<Vector3> OrganiseSquareFormationFromCorner<T>(Vector3 start, Vector3 end, List<T> agents, int columns, int space)
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

        public static List<Vector3> OrganiseSquareFormation<T>(Vector3 start, List<T> agents, int columns, int space)
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

        static Vector2 CalcPosition2(int index, int columns, int space) // call this func for all your objects
        {
            float posX = (index % columns) * space;
            float posY = (index / columns) * space;

            return new Vector2(posX, posY);
        }

        static Vector2 CalcPosition(int index, int columns, int space) // call this func for all your objects
        {
            float posX = (index % columns) * space;
            float posY = (index / columns) * space;

            return new Vector2(posX, -posY);
        }
    }
}