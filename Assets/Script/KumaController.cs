using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO playerControllerに合わせる
public class KumaController : MonoBehaviour
{
    string pushingKey;
    public float moveSpeed;
    Rigidbody2D rb;
    public AudioClip starSE;
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aud=GetComponent<AudioSource>();
    
    }

    // Update is called once per frame
    void Update()
    {
        float moveHLstick = Input.GetAxis("PS5HorizontalL");
        float moveVLstick = Input.GetAxis("PS5VerticalL");
        // float moveHLstick=Input.GetAxis("Stick1Horizontal");
        // float moveVLstick = Input.GetAxis("Stick1Vertical");
        rb.AddForce(new Vector3(moveHLstick * moveSpeed, moveVLstick * moveSpeed, 0));

    }

    void OnTriggerEnter2D(Collider2D other){
        // 点数用
        if(other.gameObject.tag=="star"){ 
            aud.PlayOneShot(starSE);
        }        
    }
}
