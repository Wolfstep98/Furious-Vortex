using System;
using UnityEngine;

public static class Calculus 
{
    #region Fields & Properties

    #endregion

    #region Methods
    /// <summary>
    /// Check if the <paramref name="point"/> is on the left side or the right side avec the line <paramref name="b"/>-<paramref name="a"/>.
    /// </summary>
    /// <param name="a">The starting point of the line.</param>
    /// <param name="b">The end point of the line.</param>
    /// <param name="point">The point to check side of.</param>
    /// <returns></returns>
    public static int PointSideRelativeToALine(Vector3 a, Vector3 b, Vector3 point)
    {
        int result = 0;

        float determinant = ((point.x - a.x) * (b.y - a.y)) - ((point.y - b.y) * (b.x - a.x));

        if (determinant > 0)
            result = 1;
        else if (determinant < 0)
            result = -1;
        else
            result = 0;

        return result;
    }

    /// <summary>
    /// Campute a perpendicular vector of <paramref name="vector"/>.
    /// </summary>
    /// <param name="vector">The vector to compute perpendicular one.</param>
    /// <returns>The perpendicular vector.</returns>
    public static Vector2 PerpendicularVector2(Vector2 vector, Side side = Side.Left)
    {
        Vector2 result = Vector2.zero;
        switch (side)
        {
            case Side.Left:
                result = new Vector2(-vector.y, vector.x);
                break;
            case Side.Right:
                result = new Vector2(vector.y, -vector.x);
                break;
            default:
                Debug.Log("[Parameter Error] - Perpendicular vector direction can anly be on the left/right side of the vector.");
                break;
        }
        return result;
    }
	#endregion
}
