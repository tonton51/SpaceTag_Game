using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// TODO Rename:ButtonController
public class ButtonController : MonoBehaviour
{
    public GameObject StartButton;
    public static bool startflag=false;

    // local用
    public void StartButtonClick(){
        SceneManager.LoadScene("GameScene");
    }
    public void GameStartButtonClick(){
        startflag=true;
        StartButton.SetActive(false);
    }
}
