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
    float speed=-0.03f;
    public static bool startflag; // スタートボタンが押されたか否か
    public GameObject Star;
    public GameObject Stone;

    public void SetParameter(float span, float speed, float ratio){
        this.span=span;
        this.speed=speed;
        this.ratio=ratio;
    }


    void Update()
    {
        int dice=Random.Range(1,21);
        GameObject item;
        // diceがratio*20より大小で変化させる
        startflag=ButtonController.startflag;
        if(startflag){
            this.delta+=Time.deltaTime;
            this.starttime+=Time.deltaTime;

            if(delta>20.0f&&delta<30.0f){
                span=0.5f;
            }else{
                span=1.0f;
            }
            if(starttime>3.0f){
                if(this.delta>this.span){
                    this.delta=0;
                    float x=Random.Range(-8,8);
                    if(dice<=ratio*20){
                        Instantiate(Stone,new Vector2(x, 7),Quaternion.identity);
                    }else{
                        Instantiate(Star,new Vector2(x, 7),Quaternion.identity);
                    }
                }
            }
        }
    }


}
