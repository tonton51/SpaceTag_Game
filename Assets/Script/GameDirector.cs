using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// ゲーム全体の監督用スクリプト（点数、役割、時間を管理）
public class GameDirector : MonoBehaviour
{
    // public TextMeshProUGUI[] pointtext;
    public TextMeshProUGUI Rpointtext;
    public float delta=63.0f; // 制限時時間
    float count=3.0f; // カウントダウン
    public TextMeshProUGUI timertext;
    public TextMeshProUGUI counttext;
    public static int Rpoint;
    public static bool startflag;
    // Start is called before the first frame update
    void Start()
    {
            timertext.enabled=false;
            counttext.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        startflag=ButtonController.startflag;
        
        Rpoint=StarController.point;
        Rpointtext.text=Rpoint.ToString();
        if(startflag){
            timertext.enabled=true;
            counttext.enabled=true;
            // 時間管理
            this.delta-=Time.deltaTime;
            if(this.delta<=60.0f){
                counttext.enabled=false;
                timertext.text=this.delta.ToString("F2");
                // 制限時間が0になったらシーン遷移
                if(this.delta<0){
                    Rpoint=StarController.point;
                     SceneManager.LoadScene("Ending");
                }
            }else{
                this.count-=Time.deltaTime;
                if(this.count<1.0f){
                    counttext.text="START";
                }else{
                    counttext.text=this.count.ToString("F0");
                }
                
            }
        }
    }
}
