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
    public float time = 48.0f; // 制限時間
    float count = 3.0f; // カウントダウン
    public TextMeshProUGUI timertext;
    public TextMeshProUGUI counttext;
    public static int Rpoint;
    public static bool startflag;
    public GameObject generator; // generatorに値をセットする
    
    public string currentMode="normal"; // モード設定用
    int bonusModeCount=0; // カウント用
    float remainingTime; 

    // Start is called before the first frame update
    void Start()
    {
        timertext.enabled = false;
        counttext.enabled = false;
        this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 0.2f,"normal"); // 初期値の設定 float span, float speed, float ratio
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO startflagの削除とexplainSceneの追加
        startflag = ButtonController.startflag;


        Rpoint = ItemController.point;
        Rpointtext.text = Rpoint.ToString();
        Debug.Log(ItemController.stonecount);
        if (startflag)
        {
            timertext.enabled = true;
            counttext.enabled = true;
            this.time -= Time.deltaTime; // 時間管理
            // 3count用
            if (45 <= this.time && this.time < 48)
            {
                this.count -= Time.deltaTime;
                if (this.count < 1.0f)
                {
                    counttext.text = "START";
                }
                else
                {
                    counttext.text = this.count.ToString("F0");
                }
            }
            else if (30 <= this.time && this.time < 45)
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 0.2f,currentMode);
            }
            else if (20 <= this.time && this.time < 30)
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                this.generator.GetComponent<ItemGenerator>().SetParameter(0.8f, -0.04f, 0.4f,currentMode);
            }
            else if (10 <= this.time && this.time < 20)
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                this.generator.GetComponent<ItemGenerator>().SetParameter(0.5f, -0.05f, 0.5f,currentMode);
            }
            else if (0 <= this.time && this.time < 10)
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                this.generator.GetComponent<ItemGenerator>().SetParameter(0.7f, -0.04f, 0.3f,currentMode);
            }
            // 制限時間が0になったらシーン遷移
            // else if (this.time < 0)
            // {
            //     counttext.enabled = false;
            //     timertext.text = this.time.ToString("F2");
            //     Rpoint = StarController.point;
            //     SceneManager.LoadScene("Ending");
            // }

            // モードを追加するためのもの
            int dice=Random.Range(1,100);
            if (this.time<=30&&this.currentMode == "normal" && this.bonusModeCount == 0&&dice<=50)
            {
                this.currentMode = "bonus";
                this.remainingTime = this.time;// 
                this.time = (float)10.0;         

                this.bonusModeCount++;
    
                // 1.3 音声再生
                // this.aud.PlayOneShot(this.bonusSE);
            }
    
            // 3   �{�[�i�X�X�e�[�W�܂��͔��]�X�e�[�W���I�������ہA�ʏ�X�e�[�W�ɖ߂�B
            if ((this.time < 0) && (this.currentMode == "bonus"))
            {
                counttext.enabled = false;
                this.currentMode = "normal";
                this.time = this.remainingTime;
                // this.aud.PlayOneShot(this.normalSE);
            }
    
            // �c�莞�Ԃ��Ȃ��Ȃ�����V�[���I��
            if (this.time < 0 && this.currentMode == "normal")
            {
                counttext.enabled = false;
                timertext.text = this.time.ToString("F2");
                Rpoint = ItemController.point;
                SceneManager.LoadScene("Ending");
                return;
            }
            
        }
    }
}
