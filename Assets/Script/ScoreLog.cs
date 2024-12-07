using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreLog : MonoBehaviour
{

    private List<string> logList = new List<string>();  // ログをリストで保持
    private string logFilePath;
    private int StarCount;
    private int StoneCount;
    private int Gamecount;
    void Start()
    {
        StarCount=GameDirector.Endpoint;
        StoneCount=GameDirector.Stonecount;
        Gamecount=StartDirector.Gamecount;
        // star+idxの名前を作成
        string FileName = "Score" +Gamecount.ToString();
        string FolderName="Log"+Gamecount.ToString();
        // Assets/Starlog フォルダのパスを設定
        string directoryPath = Path.Combine("/Users/fukulabo/Desktop/SpaceTag_Log", FolderName);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);  // フォルダが存在しなければ作成
        }

        logFilePath = Path.Combine(directoryPath, FileName + "_log.csv");  // Path.Combineを使用

        // ログリストの初期化（CSV形式でヘッダーを準備）
        logList.Add("Clear/GameOver,Star,Stone");
        
        if(StarCount<3){
            logList.Add("Clear,"+StarCount+","+StoneCount);
        }else{  
            logList.Add("GameOver,"+StarCount+","+StoneCount);
        }
        // ログをCSVファイルに書き出す
        ExportToCSV();
    }



    // リストのデータをCSVファイルに吐き出す
    void ExportToCSV()
    {
        try
        {
            File.WriteAllLines(logFilePath, logList.ToArray());
            // Debug.Log("Log saved to: " + logFilePath);
        }
        catch (System.Exception ex)
        {
            // Debug.LogError("Failed to save log: " + ex.Message);  // エラーを出力
        }
    }
}
