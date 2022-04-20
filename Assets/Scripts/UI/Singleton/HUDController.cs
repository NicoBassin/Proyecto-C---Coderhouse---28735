using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject instructions;
    public static HUDController HUDMInstance;

    public event Action OnSceneChange;
    private float delayTime = 0.1f;

    private void Awake(){
        if (HUDMInstance == null)
        {
            HUDMInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update(){
        MainMenu();
    }

    private void MainMenu(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(SceneManager.GetActiveScene().name == "Level1"){
                NextLevel();
            }
        }
    }

    public void NextLevel(){
        NextScene();
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange(){
        yield return new WaitForSeconds(delayTime);
        OnSceneChange?.Invoke();
    }

    private void NextScene(){
        if(SceneManager.GetActiveScene().name == "Level1"){
            SceneManager.LoadScene("Main");
            panel.SetActive(true);
        }
        if(SceneManager.GetActiveScene().name == "Main"){
            SceneManager.LoadScene("Level1");
            panel.SetActive(false);
        }
    }

    public void ShowInstructions(){
        panel.SetActive(false);
        instructions.SetActive(true);
    }

    public void ExitInstructions(){
        panel.SetActive(true);
        instructions.SetActive(false);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
