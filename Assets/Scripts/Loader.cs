using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainManuScene,
        LoadingScene,
        GameScene,
    }

    private static Scene targetSceneName;
    
    // when this method is called, the loading scene was loaded first.
    // In the loading scene, the loader callback will be called to load the actual scene
    // the call back function is used so that loading scene will be shown on the screen before
    // the sceneManager loads the targetScene
    public static void LoadScene(Scene scene)
    {
        targetSceneName = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetSceneName.ToString());
        ResetStaticDataManager.ResetStaticData();
    }
    
    public static void ResetStaticData()
    {
        targetSceneName = Scene.MainManuScene;
    }
}
