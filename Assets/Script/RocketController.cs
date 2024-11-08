using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    string pushingKey;
    public float moveSpeed;
    Rigidbody2D rb;
    public AudioClip starSE;
    public AudioClip stoneSE;
    AudioSource aud;
    float count=3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aud=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.count -= Time.deltaTime;
    
        if(count<=0){
            float moveHRstick = Input.GetAxis("PS5HorizontalR");
            float moveVRstick = Input.GetAxis("PS5VerticalR");
            // float moveHRstick=Input.GetAxis("Stick2Horizontal");
            // float moveVRstick = Input.GetAxis("Stick2Vertical");
            rb.AddForce(new Vector3(moveHRstick * moveSpeed, moveVRstick * moveSpeed, 0));
        }
    }


    void OnTriggerEnter2D(Collider2D other){
        // 点数用
        if(other.gameObject.tag=="star"){ 
            aud.PlayOneShot(starSE);
        }else if(other.gameObject.tag=="stone"){
            aud.PlayOneShot(stoneSE);
        }
    }
}
