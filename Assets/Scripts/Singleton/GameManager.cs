using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private enum Difficulty{
        Easy,
        Normal,
        Hard,
        Extreme
    }
    public static GameManager gmInstance;
    public int score, playerLife;
    [SerializeField] Difficulty difficulty;

    private void Awake(){
        if(gmInstance == null){
            gmInstance = this;
            DontDestroyOnLoad(gameObject);
            InitVariables();
        }
        else{
            Destroy(gameObject);
        }
    }
    
    private void InitVariables(){
        score = 0;
        switch(difficulty){
            case Difficulty.Easy:
                playerLife = 100;
                break;
            case Difficulty.Normal:
                playerLife = 50;
                break;
            case Difficulty.Hard:
                playerLife = 30;
                break;
            case Difficulty.Extreme:
                playerLife = 10;
                break;
            default:
                playerLife = 50;
                Debug.Log("Dificultad no seleccionada. Default: Normal");
                break;
        }
    }
}
