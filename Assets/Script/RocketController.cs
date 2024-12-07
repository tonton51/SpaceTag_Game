// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RocketController : MonoBehaviour
// {
//     string pushingKey;
//     public float moveSpeed;
//     Rigidbody2D rb;
//     public AudioClip starSE;
//     public AudioClip stoneSE;
//     AudioSource aud;
//     float count=3.0f;
//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         aud=GetComponent<AudioSource>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         this.count -= Time.deltaTime;
    
//         if(count<=0){
//             float moveHRstick = Input.GetAxis("PS5HorizontalR");
//             float moveVRstick = Input.GetAxis("PS5VerticalR");
//             // float moveHRstick=Input.GetAxis("Stick2Horizontal");
//             // float moveVRstick = Input.GetAxis("Stick2Vertical");
//             rb.AddForce(new Vector3(moveHRstick * moveSpeed, moveVRstick * moveSpeed, 0));
//         }
//     }


//     void OnTriggerEnter2D(Collider2D other){
//         // 点数用
//         if(other.gameObject.tag=="star"){ 
//             aud.PlayOneShot(starSE);
//         }else if(other.gameObject.tag=="stone"){
//             aud.PlayOneShot(stoneSE);
//         }
//     }
// }


// 元のやつ
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// // TODO playerControllerに合わせる
// public class KumaController : MonoBehaviour
// {
//     string pushingKey;
//     public float moveSpeed;
//     Rigidbody2D rb;
//     public AudioClip starSE;
//     public AudioClip stoneSE;
//     AudioSource aud;
//     float count=3.0f;
//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         aud=GetComponent<AudioSource>();
    
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         this.count -= Time.deltaTime;
    
//         if(count<=0){
//             float moveHLstick = Input.GetAxis("PS5HorizontalL");
//             float moveVLstick = Input.GetAxis("PS5VerticalL");
//             // float moveHLstick=Input.GetAxis("Stick1Horizontal");
//             // float moveVLstick = Input.GetAxis("Stick1Vertical");
//             rb.AddForce(new Vector3(moveHLstick * moveSpeed, moveVLstick * moveSpeed, 0));
//         }
//     }

//     void OnTriggerEnter2D(Collider2D other){
//         // 点数用
//         if(other.gameObject.tag=="star"){ 
//             aud.PlayOneShot(starSE);
//         }else if(other.gameObject.tag=="stone"){
//             aud.PlayOneShot(stoneSE);
//         }  
//     }
// }



// using UnityEngine;
// using Unity.MLAgents;
// using Unity.MLAgents.Sensors;
// using Unity.MLAgents.Actuators;
// using System.Collections.Generic;

// public class RocketAgent : Agent
// {
//     Rigidbody2D rBody;
//     AudioSource aud;
//     public AudioClip starSE;
//     public AudioClip stoneSE;

//     public int starCount = 0;
//     public List<GameObject> stars = new List<GameObject>(); // 星を管理するリスト

//     public override void Initialize()
//     {
//         rBody = GetComponent<Rigidbody2D>();
//         aud = GetComponent<AudioSource>();

//     }

//     public override void CollectObservations(VectorSensor sensor)
//     {
//         // 自分の位置
//         sensor.AddObservation(this.transform.localPosition.x);
//         sensor.AddObservation(this.transform.localPosition.y);

//         // 最も近い星の位置を観察
//         GameObject nearestStar = GetNearestStar();
//         if (nearestStar != null)
//         {
//             sensor.AddObservation(nearestStar.transform.localPosition.x);
//             sensor.AddObservation(nearestStar.transform.localPosition.y);
//         }
//         else
//         {
//             sensor.AddObservation(0f);
//             sensor.AddObservation(0f);
//         }

//         // 最も近い石の位置を観察
//         GameObject nearestStone = GetNearestStone();
//         if (nearestStone != null)
//         {
//             sensor.AddObservation(nearestStone.transform.localPosition.x);
//             sensor.AddObservation(nearestStone.transform.localPosition.y);
//         }
//         else
//         {
//             sensor.AddObservation(0f);
//             sensor.AddObservation(0f);
//         }
//     }

//     public override void OnActionReceived(ActionBuffers actionBuffers)
//     {
//         // エージェントの動き
//         Vector3 controlSignal = Vector3.zero;
//         controlSignal.x = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
//         controlSignal.y = Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
//         rBody.AddForce(new Vector3(controlSignal.x * 5, controlSignal.y * 5, 0));

//         // 最も近い星に近づくと報酬
//         GameObject nearestStar = GetNearestStar();
//         if (nearestStar != null)
//         {
//             float distanceToStar = Vector3.Distance(this.transform.position, nearestStar.transform.position);
//             // GameDirector.agentGroup.AddGroupReward(-0.01f * distanceToStar); // 距離に応じたペナルティ
//             AddReward(-0.01f*distanceToStar);
//         }

//         // 最も近い石からの距離に応じたペナルティ
//         GameObject nearestStone = GetNearestStone();
//         if (nearestStone != null)
//         {
//             float distanceToStone = Vector3.Distance(this.transform.position, nearestStone.transform.position);
//             if (distanceToStone < 3.0f) // 石が近い場合にペナルティを追加
//             {
//                 // GameDirector.agentGroup.AddGroupReward(-0.1f / distanceToStone);
//                 AddReward(-0.1f/distanceToStone);
//             }
//         }
//     }

//     public override void Heuristic(in ActionBuffers actionBuffers)
//     {
//         var actionsOut = actionBuffers.ContinuousActions;
//         actionsOut[0] = Input.GetAxis("PS5HorizontalR");
//         actionsOut[1] = Input.GetAxis("PS5VerticalR");
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.gameObject.tag == "star")
//         {
//             // 星を取得した際の報酬
//             // GameDirector.agentGroup.AddGroupReward(2.0f);
//             AddReward(5.0f);

//             // 星の音声再生
//             aud.PlayOneShot(starSE);

//             // 星カウントの増加とリストから削除
//             starCount++;
//             stars.Remove(other.gameObject);
//             Destroy(other.gameObject);
//         }
//         else if (other.gameObject.tag == "stone")
//         {
//             // 石に接触した場合のペナルティ
//             // GameDirector.agentGroup.AddGroupReward(-5.0f);
//             AddReward(-1.0f);

//             // 石の音声再生
//             aud.PlayOneShot(stoneSE);
//         }
//     }

//     // 星を管理するためのメソッド
//     public void AddStar(GameObject star)
//     {
//         if (!stars.Contains(star))
//         {
//             stars.Add(star);
//         }
//     }

//     // 最も近い星を取得
//     private GameObject GetNearestStar()
//     {
//         GameObject nearestStar = null;
//         float minDistance = float.MaxValue;

//         foreach (GameObject star in stars)
//         {
//             if (star == null) continue; // 星が削除されている場合を無視

//             float distance = Vector3.Distance(this.transform.position, star.transform.position);
//             if (distance < minDistance)
//             {
//                 minDistance = distance;
//                 nearestStar = star;
//             }
//         }

//         return nearestStar;
//     }

//     // 最も近い石を取得
//     private GameObject GetNearestStone()
//     {
//         GameObject nearestStone = null;
//         float minDistance = float.MaxValue;

//         foreach (GameObject stone in GameObject.FindGameObjectsWithTag("stone"))
//         {
//             if (stone == null) continue; // 石が削除されている場合を無視

//             float distance = Vector3.Distance(this.transform.position, stone.transform.position);
//             if (distance < minDistance)
//             {
//                 minDistance = distance;
//                 nearestStone = stone;
//             }
//         }

//         return nearestStone;
//     }
// }


using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class RocketAgent : Agent
{
    Rigidbody2D rBody;
    AudioSource aud;
    public AudioClip starSE;

    public int starCount = 0;
    public List<GameObject> stars = new List<GameObject>(); // 星を管理するリスト

    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 自分の位置
        sensor.AddObservation(this.transform.localPosition.x);
        sensor.AddObservation(this.transform.localPosition.y);

        // 最も近い星の位置を観察
        GameObject nearestStar = GetNearestStar();
        if (nearestStar != null)
        {
            sensor.AddObservation(nearestStar.transform.localPosition.x);
            sensor.AddObservation(nearestStar.transform.localPosition.y);
        }
        else
        {
            // 星が存在しない場合、デフォルト値を設定
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
        controlSignal.y = Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);


        // 学習時50,heuristic時10とかでいいかも
        rBody.AddForce(new Vector3(controlSignal.x * 10, controlSignal.y * 10, 0));

        // 最も近い星に近づくと報酬
        GameObject nearestStar = GetNearestStar();
        if (nearestStar != null)
        {
            float distanceToStar = Vector3.Distance(this.transform.position, nearestStar.transform.position);
            AddReward(-0.01f * distanceToStar); // 距離に応じたペナルティ
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "star")
        {
            AddReward(2.0f); // 星を取った時の報酬
            aud.PlayOneShot(starSE);
            starCount++;

            // リストから星を削除
            stars.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    public override void Heuristic(in ActionBuffers actionBuffers)
    {
        var actionsOut = actionBuffers.ContinuousActions;
        actionsOut[0] = Input.GetAxis("PS5HorizontalR");
        actionsOut[1] = Input.GetAxis("PS5VerticalR");
    }

    // 星を管理するためのメソッド
    public void AddStar(GameObject star)
    {
        if (!stars.Contains(star))
        {
            stars.Add(star);
        }
    }

    // 最も近い星を取得
    private GameObject GetNearestStar()
    {
        GameObject nearestStar = null;
        float minDistance = float.MaxValue;

        foreach (GameObject star in stars)
        {
            if (star == null) continue; // 星が削除されている場合を無視

            float distance = Vector3.Distance(this.transform.position, star.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestStar = star;
            }
        }

        return nearestStar;
    }
}
