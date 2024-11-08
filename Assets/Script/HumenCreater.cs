using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HumenCreater : MonoBehaviour
{
    private float humentime = -2.0f;
    private int nextIndex = 0;
    public GameObject Star;
    public GameObject Sh;

    // データを格納するクラス
    [System.Serializable]
    public class TimedObject
    {
        public float time;
        public string objectName;
        public int place;

        public TimedObject(float time, string objectName, int place)
        {
            this.time = time;
            this.objectName = objectName;
            this.place = place;
        }
    }

    // リストにデータを格納
    public List<TimedObject> timedObjects = new List<TimedObject>();

    void Start()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/itemtime.csv");

        if (File.Exists(filePath))
        {
            string[] csvData = File.ReadAllLines(filePath);

            for (int i = 1; i < csvData.Length; i++)  // 1行目はヘッダーなのでスキップ
            {
                string[] row = csvData[i].Split(',');

                if (float.TryParse(row[0], out float time) && int.TryParse(row[2], out int place))
                {
                    string objectName = row[1];
                    TimedObject timedObject = new TimedObject(time, objectName, place);
                    timedObjects.Add(timedObject);
                }
            }
        }
        else
        {
            Debug.LogError("CSVファイルが見つかりません: " + filePath);
        }

        // 読み込んだデータを確認（デバッグ用）
        foreach (var obj in timedObjects)
        {
            Debug.Log($"Time: {obj.time}, Object Name: {obj.objectName}, Place: {obj.place}");
        }
    }

    void Update()
    {
        humentime += Time.deltaTime;
        GameObject item = null;
        
        while (nextIndex < timedObjects.Count && humentime >= timedObjects[nextIndex].time)
        {
            bool sameflag = false;
            if (nextIndex + 1 < timedObjects.Count && timedObjects[nextIndex].time == timedObjects[nextIndex + 1].time)
            {
                sameflag = true;
            }

            if (timedObjects[nextIndex].objectName == "star")
            {
                item = Instantiate(Star) as GameObject;
            }
            else
            {
                item = Instantiate(Sh) as GameObject;
            }

            item.transform.position = new Vector3(timedObjects[nextIndex].place, 7, 0);
            nextIndex++;

            if (sameflag)
            {
                if (timedObjects[nextIndex].objectName == "star")
                {
                    item = Instantiate(Star) as GameObject;
                }
                else
                {
                    item = Instantiate(Sh) as GameObject;
                }

                item.transform.position = new Vector3(timedObjects[nextIndex].place, 7, 0);
                nextIndex++;
            }
        }
    }
}
