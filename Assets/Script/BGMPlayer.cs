using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    void Start()
    {
        //bgmStartを3秒後に呼び出す処理
        Invoke("BGMPlay", 3.0f);
    }

    public void BGMPlay()
    {
        //AidopSourceコンポーネントを格納する
        AudioSource bgm = this.GetComponent<AudioSource>();
        //再生する
        bgm.Play();
        //playOnAwakeをオンにする
        bgm.playOnAwake = true;
    }
}
