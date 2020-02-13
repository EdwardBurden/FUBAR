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

        public static List<Vector3> OrganiseRelaxedFormation<T>(Vector3 start, List<T> agents, int columns, int space)
        {
            int rows = agents.Count / columns;
            float offsetx = ((columns * space) - space) / 2;
            float offsetz = ((rows * space) - space) / 2;
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < agents.Count; i++)
            {
                Vector3 pos = CalcPosition(i, columns, space);
                positions.Add(new Vector3(start.x + pos.x - offsetx, 0, start.z + pos.y - offsetz));
            }
            return positions;
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
            float offsetx = ((columns * space) - space) / 2;
            float offsetz = ((rows * space) - space) / 2;
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < agents.Count; i++)
            {
                Vector3 pos = CalcPosition2(i, columns, space);
                positions.Add(new Vector3(start.x + pos.x - offsetx, 0, start.z + pos.y - offsetz));
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