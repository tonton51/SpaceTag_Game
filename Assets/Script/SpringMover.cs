using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringMover : MonoBehaviour
{
    public Transform kuma;   // オブジェクト1
    public Transform rocket;   // オブジェクト2
    public Transform spring;    // ばねの画像オブジェクト（スプライト）
 
    private Vector3 initialScale;   // 元のばねのスケール
 
    void Start()
    {
        // 初期のスケールを保存しておく
        initialScale = spring.localScale;
    }
 
    void Update()
    {
        // オブジェクト1とオブジェクト2の間の距離を計算
        float distance = Vector3.Distance(kuma.position, rocket.position);
 
        // スプライトをオブジェクト1とオブジェクト2の間に配置する
        spring.position = (kuma.position + rocket.position) / 2;
 
        // ばねの画像をオブジェクトに向けて回転させる
        Vector3 direction = kuma.position - rocket.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spring.rotation = Quaternion.Euler(0, 0, angle);
 
        // 距離に応じてスケールを調整
        spring.localScale = new Vector3(initialScale.x*(distance*0.5f), initialScale.y, initialScale.z);
    }
}
