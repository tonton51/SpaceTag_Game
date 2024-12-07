using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.MLAgents;
using Unity.MLAgents.Policies;


// ゲーム全体の監督用スクリプト（点数、役割、時間を管理）
public class GameDirector : MonoBehaviour
{
    public TextMeshProUGUI Rpointtext;
    public float time = 48.0f; // 制限時間 85s 48s
    float count = 3.0f; // カウントダウン
    public TextMeshProUGUI timertext;
    public TextMeshProUGUI counttext;
    public static int Rpoint;
    public static bool startflag = false;
    public GameObject generator;
    public static int Stonecount = 0;
    public static int Endpoint;
    public List<GameObject> HP;
    public AudioClip CountSE;
    public AudioClip bonusSE;
    AudioSource aud;


    // ボーナスモード用
    public string currentMode = "normal"; // モード設定用
    int bonusModeCount = 0; // カウント用
    float remainingTime;

    // 学習用
    public Agent[] agents;
    // public static SimpleMultiAgentGroup agentGroup;
    
    public BehaviorParameters kumabehaviorParameters;
    public BehaviorParameters rocketbehaviorParameters;
    private bool autoflag;
    void Start()
    {
        autoflag = StartDirector.autoflag;
        // staticStartDirector
        if (autoflag)
        {
            kumabehaviorParameters.BehaviorType = BehaviorType.Default; // 適切な BehaviorType に変更
            rocketbehaviorParameters.BehaviorType = BehaviorType.Default; // 適切な BehaviorType に変更
        }else{
            kumabehaviorParameters.BehaviorType = BehaviorType.HeuristicOnly; // 適切な BehaviorType に変更
            rocketbehaviorParameters.BehaviorType = BehaviorType.HeuristicOnly; // 適切な BehaviorType に変更
        }

        Reset();
    }
    // Start is called before the first frame update
    // startから変更
    public void Reset()
    {
        Debug.Log("begin");
        Rpoint = 0;
        // AudioSourceを設定
        aud = GetComponent<AudioSource>();
        
        Stonecount = 0;
        
        // HPオブジェクトを初期化
        for (int i = 0; i < 3; i++)
        {
            HP[i].GetComponent<Renderer>().enabled = true; // 表示する場合
        }
        
        timertext.enabled = false;
        counttext.enabled = false;
        
        // 初期設定
        this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 0.2f, "normal");
        Debug.Log(time);

        // 学習用
        time=48.0f;
        // agentGroup.RegisterAgent(agents[1]);
    }

    // Update is called once per frame
    void Update()
    {


        startflag = ButtonController.startflag;

        // HPの非表示設定
        if (Stonecount<=3&&Stonecount > 0)
        {
            HP[Stonecount - 1].GetComponent<Renderer>().enabled = false;
        }
        if (Stonecount >= 3)
        {
            // 学習用
            agents[0].EndEpisode();
            agents[1].EndEpisode();
            // agentGroup.EndGroupEpisode();
            SceneManager.LoadScene("Ending");
            // Reset();
        }
        
        Rpointtext.text = Rpoint.ToString();
        
        // Debug.Log(Stonecount);
        
        // if (startflag)
        // {
            Endpoint = Rpoint;
            timertext.enabled = true;
            counttext.enabled = true;
            this.time -= Time.deltaTime;
            // Debug.Log(time);

            // 82<85
            if (45 <= this.time && this.time < 48)
            {
                this.count -= Time.deltaTime;
                if (this.count <= 0.5f)
                {
                    counttext.text = "START";
                }
                else
                {
                    counttext.text = this.count.ToString("F0");
                }
            }
            // 82 music
            if (this.time < 45)
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                // csvファイルじゃないやつ用、石落下のみに使用
                if (30 <= this.time && this.time < 45)
                {
                    this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 0.2f, currentMode);
                }
                else if (20 <= this.time && this.time < 30)
                {
                    this.generator.GetComponent<ItemGenerator>().SetParameter(0.8f, -0.04f, 0.3f, currentMode);
                }
                else if (10 <= this.time && this.time < 20)
                {
                    this.generator.GetComponent<ItemGenerator>().SetParameter(0.5f, -0.05f, 0.2f, currentMode);
                }
                else if (0 <= this.time && this.time < 10)
                {
                    this.generator.GetComponent<ItemGenerator>().SetParameter(0.7f, -0.04f, 0.3f, currentMode);
                }
            }

            
            // 以下ボーナスステージ実装用
            int dice = Random.Range(1, 100);
            if (this.time <= 30 && this.currentMode == "normal" && this.bonusModeCount == 0 && dice <= 50)
            {
                aud.PlayOneShot(bonusSE);
                this.currentMode = "bonus";
                this.remainingTime = this.time;
                this.time = 10.0f;         
                this.bonusModeCount++;
            }

            if (this.time < 0 && this.currentMode == "bonus")
            {
                counttext.enabled = false;
                this.currentMode = "normal";
                this.time = this.remainingTime;
            }
            // ここまで

            if (this.time < 0 && this.currentMode == "normal")
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                Rpoint = ItemController.point;

                // 学習用
                agents[0].EndEpisode();
                agents[1].EndEpisode();
                // agentGroup.EndGroupEpisode();
                

                SceneManager.LoadScene("Ending");
                // Reset();
                return;
            }
        //}
    }

}
