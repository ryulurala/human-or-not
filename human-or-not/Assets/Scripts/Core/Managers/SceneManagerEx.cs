﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void Clear()
    {
        CurrentScene.Clear();
    }

    string GetSceneName(Define.Scene type)
    {
        // Reflaction
        return System.Enum.GetName(typeof(Define.Scene), type);
    }

    public void LoadScene(Define.Scene type, bool async = false)
    {
        Manager.Clear();

        string sceneName = GetSceneName(type);
        if (async == false)
            SceneManager.LoadScene(sceneName);
        else
            Manager.OpenCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Scene loading completed!");
        }
    }
}
