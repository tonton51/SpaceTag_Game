using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using UnityEngine.UI;
using TMPro;

// 結果表示用スクリプト
public class ResultDirector : MonoBehaviour
{
    public TextMeshProUGUI Rocketresult;
    public TextMeshProUGUI Kumaresult;
    private bool autoflag;
    public static int Gamecount=0;
    public TextMeshProUGUI ClearText;
    
    public BehaviorParameters behaviorParameters;

    // Start is called before the first frame update
    void Start()
    {
        Kumaresult.text="Point"+GameDirector.Endpoint.ToString();
        Rocketresult.text="Stone"+GameDirector.Stonecount.ToString();
        if(GameDirector.Stonecount<3){
            ClearText.text="Clear!";
        }else{
            ClearText.text="Game Over";
        }

        autoflag = StartDirector.autoflag;
        Gamecount++;
        if(Gamecount==3){
            StartDirector.autoflag=false;
            behaviorParameters.BehaviorType = BehaviorType.HeuristicOnly;
        }
        if(autoflag){
            behaviorParameters.BehaviorType = BehaviorType.Default;
            Debug.Log("Default");
        }else{
            behaviorParameters.BehaviorType = BehaviorType.HeuristicOnly;
            Debug.Log("HeuristicOnly");
        }

        // Kumaresult.text="Kuma"+GameDirector.Rpoint[0].ToString();
        // Rocketresult.text="Rocket"+GameDirector.Rpoint[1].ToString();
    }

    public void RetryButtonClick(){
        SceneManager.LoadScene("StartScene");
    }
}
