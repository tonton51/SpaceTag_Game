using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アイテムを生成するためのスクリプト
public class ItemGenerator : MonoBehaviour
{
    float span=1.0f; // アイテム生成間隔 phton:3.0f,local:1.0f
    float delta=0; // 時間計測用
    float starttime=0; // カウントダウンまでアイテムを生成しない用
    float ratio=0.2f; // 隕石生成する確率
    float speed=-0.05f;
    public static bool startflag; // スタートボタンが押されたか否か
    public GameObject Star;
    public GameObject Stone;
    string currentMode="normal"; // モード設定用

    public void SetParameter(float span, float speed, float ratio,string mode){
        this.span=span;
        this.speed=speed;
        this.ratio=ratio;
        this.currentMode=mode;
    }


    void Update()
    {
        int dice=Random.Range(1,21);

        // diceがratio*20より大小で変化させる
        startflag=ButtonController.startflag;
        // if(startflag){
            this.delta+=Time.deltaTime;
            this.starttime+=Time.deltaTime;

            if(delta>20.0f&&delta<30.0f){
                span=0.5f;
            }else{
                span=1.0f;
            }
            if(starttime>3.0f){
                // TODO rotationを変更してもいいかも？
                if(this.delta>this.span){
                    GameObject item=null;
                    this.delta=0;
                    float x=Random.Range(-8,8);
                    if(currentMode=="normal"){
                        if(dice<=ratio*20){
                            item=Instantiate(Stone) as GameObject;
                        }
                        // コメントアウトしてた部分
                        else{
                             item=Instantiate(Star) as GameObject;
                         }
                    }
                    // // else ifコメントアウトした部分
                    else if(currentMode=="bonus"){
                         item=Instantiate(Star) as GameObject;
                    }
                    if (item != null)  // itemが初期化された場合のみ位置を設定
                    {
                        item.transform.position = new Vector3(x, 7, 0);
                        item.GetComponent<ItemController>().dropspeed=this.speed;
                    }
                }
                
            }
            
       // }
        
    }


}
