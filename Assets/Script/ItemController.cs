using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float dropspeed=-0.05f; // 落下速度
    public static int point=0;
    public static int stonecount=0;


    
    // プレイヤーとぶつかった時の判定
     void OnTriggerEnter2D(Collider2D other){
        // 点数用
        if(tag=="star"){
            if(other.gameObject.tag=="kuma"||other.gameObject.tag=="rocket"){ 
                GameDirector.Rpoint++;
            }
        }else{
            GameDirector.Stonecount++;
        }
        Destroy(gameObject);
        
    }


    // 画面外にいったら削除
    void Update()
    {
        transform.Translate(0,this.dropspeed/5.0f,0);
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }
}
