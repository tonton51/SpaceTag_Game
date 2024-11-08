using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// ゲーム全体の監督用スクリプト（点数、役割、時間を管理）
public class GameDirector : MonoBehaviour
{
    public TextMeshProUGUI Rpointtext;
    public float time = 85.0f; // 制限時間
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
    public AudioSource BGM;
    AudioSource aud;
    private bool bgmflag = false;
    
    public string currentMode = "normal"; // モード設定用
    int bonusModeCount = 0; // カウント用
    float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        Rpoint = 0;
        
        // メインのサウンドとBGM用のAudioSourceを設定
        aud = GetComponent<AudioSource>();
        
        Debug.Log("Start");
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
    }

    // Update is called once per frame
    void Update()
    {
        startflag = ButtonController.startflag;

        // HPの非表示設定
        if (Stonecount > 0)
        {
            HP[Stonecount - 1].GetComponent<Renderer>().enabled = false;
        }
        if (Stonecount == 3)
        {
            SceneManager.LoadScene("Ending");
        }
        
        Rpointtext.text = Rpoint.ToString();
        
        Debug.Log(Stonecount);
        
        // if (startflag)
        // {
            Endpoint = Rpoint;
            timertext.enabled = true;
            counttext.enabled = true;
            this.time -= Time.deltaTime;
            // Debug.Log(time);

            if (82 <= this.time && this.time < 85)
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

            if (this.time < 82)
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");

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

            // int dice = Random.Range(1, 100);
            // if (this.time <= 30 && this.currentMode == "normal" && this.bonusModeCount == 0 && dice <= 50)
            // {
            //     aud.PlayOneShot(bonusSE);
            //     this.currentMode = "bonus";
            //     this.remainingTime = this.time;
            //     this.time = 10.0f;         
            //     this.bonusModeCount++;
            // }

            // if (this.time < 0 && this.currentMode == "bonus")
            // {
            //     counttext.enabled = false;
            //     this.currentMode = "normal";
            //     this.time = this.remainingTime;
            // }

            if (this.time < 0 && this.currentMode == "normal")
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                Rpoint = ItemController.point;
                SceneManager.LoadScene("Ending");
                return;
            }
        //}
    }

}
