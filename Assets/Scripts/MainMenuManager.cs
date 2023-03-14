using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    
    public Button btnContinue;
    public Animator credsAnim;

    void Start()
    {
        btnContinue.interactable = PlayerPrefs.HasKey("progress");
    }

    void Update()
    {  
        Time.timeScale = 1f;
    }

    public void Credits()
    {
        credsAnim.SetTrigger("creds");
    }

    public void Main()
    {
        credsAnim.SetTrigger("main");
    }

    public void NewGame()
    {
        FindObjectOfType<ProgressManager>().ClearAllStats();
        FindObjectOfType<LevelManager>().LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
