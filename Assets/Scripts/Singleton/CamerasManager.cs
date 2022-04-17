using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;
    int camCounter = 0;

    private void Awake(){   
        int countCM = FindObjectsOfType<AudioManager>().Length;
        if(countCM > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        SetActiveCamera(camCounter);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            camCounter++;
            if(camCounter >= cameras.Length){
                camCounter = 0;
            }
            SetActiveCamera(camCounter);
        }
    }

    private void SetActiveCamera(int camNumber){
        foreach(GameObject camera in cameras){
            camera.SetActive(false);
        }
        cameras[camNumber].SetActive(true);
    }
}
