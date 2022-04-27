using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum Difficulty{
        Easy,
        Normal,
        Hard,
        Extreme
    }
    public static GameManager gmInstance;
    public int score, playerLife;
    [SerializeField] public Difficulty difficulty;
    

    private void Awake(){
        if(gmInstance == null){
            gmInstance = this;
            DontDestroyOnLoad(gameObject);
            ResetStats();
        }
        else{
            Destroy(gameObject);
        }
    }
    
    public void ResetStats(){
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
