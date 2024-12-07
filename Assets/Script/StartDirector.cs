using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using UnityEngine.UI;


// ゲーム全体の監督用スクリプト（点数、役割、時間を管理）
public class StartDirector : MonoBehaviour
{
    public BehaviorParameters behaviorParameters;
    public Toggle toggle;
    public static bool autoflag;

    void Start()
    {
        if (behaviorParameters != null)
        {
            behaviorParameters.BehaviorType = BehaviorType.HeuristicOnly; // 適切な BehaviorType に変更
        }
    }
    
    void Update()
    {
        if(autoflag){
            behaviorParameters.BehaviorType = BehaviorType.Default;
            Debug.Log("Default");
        }else{
            behaviorParameters.BehaviorType = BehaviorType.HeuristicOnly;
            Debug.Log("HeuristicOnly");
        }
    }


    public  void OnToggleChanged(){
        if(toggle.isOn){
            autoflag=true;
        }else{
            autoflag=false;
        }
        AutoDebug(autoflag);

    }


    public void AutoDebug(bool autoflag){
        if(autoflag){
            behaviorParameters.BehaviorType = BehaviorType.Default;
            Debug.Log("Default");
        }else{
            behaviorParameters.BehaviorType = BehaviorType.HeuristicOnly;
            Debug.Log("HeuristicOnly");
        }
    }

    public void StartButtonClick(){
        SceneManager.LoadScene("GameScene");
        Debug.Log("push");
    }

}
