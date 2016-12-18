using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extentions
{

    public static Vector3 xz( this Vector3 v3 )
    {
        return new Vector3( v3.x, 0, v3.z );
    }
}
