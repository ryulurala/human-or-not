using System;
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
    // public static bool IsMobile { get; } = true;
#endif

    public static T GetEnumValue<T>(string str) where T : System.Enum
    {
        string[] enums = Enum.GetNames(typeof(T));

        T result = (T)Enum.ToObject(typeof(T), 0);

        for (int i = 0; i < enums.Length; i++)
        {
            if (str == enums[i])
                result = (T)Enum.ToObject(typeof(T), i);
        }

        return result;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        // Unity GameObject는 Transform Component가 무조건 존재.
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            // 재귀 [X], 직속 자식만
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            // 재귀 O, 자식의 자식도
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        // 모두 해당 [X]
        return null;
    }
}
