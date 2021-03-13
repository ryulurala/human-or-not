using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Util
{

#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")] private static extern bool isMobile();
    public static bool IsMobile { get; } = isMobile();
#else
    public static bool IsMobile { get; } = false;
#endif

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

}
