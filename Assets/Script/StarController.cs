using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 星の動きに関するスクリプト
public class StarController : MonoBehaviour
{
    public float dropspeed=-0.05f; // 落下速度
    public static int point=0;



    
    // プレイヤーとぶつかった時の判定
     void OnTriggerEnter2D(Collider2D other){
        // 点数用
        if(other.gameObject.tag=="kuma"||other.gameObject.tag=="rocket"){ 
            point++;
        }
        Destroy(gameObject);
        
    }


    // 画面外にいったら削除
    void Update()
    {
        transform.Translate(0,this.dropspeed,0);
        if(transform.position.y<-4.0f){
            Destroy(gameObject);
        }
    }

}
