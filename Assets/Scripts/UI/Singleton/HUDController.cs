using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject inGameHUD;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text difficultyText;
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
        InGameHUD();
    }

    private void MainMenu(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(SceneManager.GetActiveScene().name == "Level1"){
                NextLevel();
            }
        }
        difficultyText.text = "Difficulty: " + GameManager.gmInstance.difficulty;
    }

    private void InGameHUD(){
        lifeText.text = GameManager.gmInstance.playerLife.ToString();
        scoreText.text = GameManager.gmInstance.score.ToString();
    }

    public void DifficultyUp(){
        if(GameManager.gmInstance.difficulty != GameManager.Difficulty.Extreme){
            GameManager.gmInstance.difficulty++;
        }
    }

    public void DifficultyDown(){
        if(GameManager.gmInstance.difficulty != GameManager.Difficulty.Easy){
            GameManager.gmInstance.difficulty--;
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
            inGameHUD.SetActive(false);
            panel.SetActive(true);
        }
        if(SceneManager.GetActiveScene().name == "Main"){
            SceneManager.LoadScene("Level1");
            GameManager.gmInstance.ResetStats();
            inGameHUD.SetActive(true);
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
