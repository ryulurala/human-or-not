using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper
{
    [System.Serializable]
    class Wrapper<T>
    {
        public T[] items;
    }

    public T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);

        return wrapper.items;
    }

    public string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;

        return JsonUtility.ToJson(wrapper);
    }

    public string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;

        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
}
