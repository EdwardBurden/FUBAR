using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFormation
{
    public abstract List<Vector3> GetFormationPosition<T>(Vector3 start, List<T> agents, int columns, int space);

    public abstract List<Vector3> GetDragFormationPosition<T>(Vector3 start, Vector3 end, List<T> agents, int columns, int space);

    public int GetColumnsFromDistance(Vector3 start, Vector3 end, int space)
    {
        float endDiffference = Vector3.Distance(end, start);
        return ((int)endDiffference / space) + 1;
    }

    protected Vector2 CalcPosition(int index, int columns, int space)
    {
        float posX = (index % columns) * space;
        float posY = (index / columns) * space;
        return new Vector2(posX, -posY);
    }
}

public enum Formationenum
{
    Square,
    Line,
    Circle,
    Fort


}
