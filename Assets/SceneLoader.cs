using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int _currentSceneIndex;

    void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        var sceneToLoad = (_currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1) ? 0 : (_currentSceneIndex + 1);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadPreviousScene()
    {
        var sceneToLoad = (_currentSceneIndex == 0) ? SceneManager.sceneCountInBuildSettings - 1 : (_currentSceneIndex - 1);
        SceneManager.LoadScene(sceneToLoad);
    }

    
}
