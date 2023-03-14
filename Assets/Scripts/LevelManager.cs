using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private int buildIndexHere;


    public void LoadScene(int buildIndex) {
        buildIndexHere = buildIndex;
        FindObjectOfType<LoaderAnimator>().LoadLoader();
        Invoke("LoadSceneNow", 0.1f);
    }

    void LoadSceneNow()
    {
        var scene = SceneManager.LoadSceneAsync(buildIndexHere);
        scene.allowSceneActivation = false;
        scene.allowSceneActivation = true;
    }

    public void QuitGame()
    {
        FindObjectOfType<PauseManager>().Resume();
        LoadScene(0);
    }

}
