using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameInit : MonoBehaviour
{
    public Button map1;
    public Button map2;
    void Start()
    {
        map1.onClick.AddListener(() =>
        {
            LoadSceneAsync("map1",()=>
            {
                World.Ins.Init("map1");
            }
            );
        });
        map2.onClick.AddListener(() =>
        {
            LoadSceneAsync("map2", () =>
            {
                World.Ins.Init("map2");
            }
            );
        });
     
    }
    public void LoadSceneAsync(string sceneName, Action call)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.completed += (_ao) =>
        {
            call?.Invoke();
        };
    }
}
