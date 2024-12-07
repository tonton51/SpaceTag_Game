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
    public static int Gamecount=0;
    public static bool splayerflag;
    public TextMeshProUGUI DebugEndtext;
    public static int LoopCount;
    public TMP_InputField inputField;
    public static Text Looptext;

    void Start()
    {
        Gamecount++;
        if(autoflag){
            DebugEndtext.enabled=true;
            DebugEndtext.text="Count"+Gamecount.ToString();
        }else if(!autoflag&&Gamecount<=LoopCount){
            DebugEndtext.enabled=false;
        }else if(Gamecount>LoopCount){
            DebugEndtext.enabled=true;
            DebugEndtext.text="DebugEnd";
        }
    }
    public void InputButtonClick(){
        LoopCount=int.Parse(inputField.text);
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

    public void SinglePlayerStartButtonClick(){
        SceneManager.LoadScene("GameScene");
        splayerflag=true;
        Debug.Log("push");
    }
    public void MultiPlayerStartButtonClick(){
        SceneManager.LoadScene("GameScene");
        splayerflag=false;
        Debug.Log("push");
    }


}
