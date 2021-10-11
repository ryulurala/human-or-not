using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static bool IsValid(this GameObject go)
    {
        if (go == null)
            return false;
        else if (go.activeSelf == false)
            return false;
        else
            return true;
    }
}
