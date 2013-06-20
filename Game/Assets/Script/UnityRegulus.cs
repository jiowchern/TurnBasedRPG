using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class UnityRegulus
{
    internal static UnityEngine.Vector3 Vector3(Regulus.Types.Vector2 vector2)
    {
        return new UnityEngine.Vector3(vector2.X, 0, vector2.Y);
    }
}