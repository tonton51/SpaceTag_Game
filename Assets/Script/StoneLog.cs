using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoneLog : MonoBehaviour
{
    public int idx;  // idxをパブリック変数として宣言 (Inspectorで設定可能)
    private GameObject stoneObject;  // 探すオブジェクト
    private string stoneName;        // オブジェクト名 (star+idx)
    private List<string> logList = new List<string>();  // ログをリストで保持
    private string logFilePath;
    private bool objectFound = false;  // オブジェクトが見つかったかを記録するフラグ

    private GameDirector gameDirector; // GameDirectorのインスタンスを追加
    private int count;
    void Start()
    {
        // star+idxの名前を作成
        stoneName = "stone" + idx.ToString();
        count=StartDirector.Gamecount;
        string FolderName="Log"+count.ToString();
        // フォルダのパスを設定
        string directoryPath = Path.Combine("/Users/fukulabo/Desktop/SpaceTag_Log", FolderName);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);  // フォルダが存在しなければ作成
        }

        logFilePath = Path.Combine(directoryPath, stoneName + "_log.csv");  // Path.Combineを使用

        // ログリストの初期化（CSV形式でヘッダーを準備）
        logList.Add("Time,ObjectName,X,Y");
        
        // GameDirectorのインスタンスを取得
        gameDirector = FindObjectOfType<GameDirector>();
    }

    void Update()
    {
        // オブジェクトが見つかっていない場合、オブジェクトを探す
        if (!objectFound)
        {
            stoneObject = GameObject.Find(stoneName);

            if (stoneObject != null)
            {
                objectFound = true;
            }
            else
            {
                return; // まだ見つかっていないのでこのフレームの処理を終了
            }
        }

        // オブジェクトが見つかった場合、位置をログに記録
        if (stoneObject != null)
        {
            Vector2 position = stoneObject.transform.position;
            LogPosition(position);
        }
    }

    void LogPosition(Vector2 position)
    {
        // 各フレームでログをリストに追加 (時間, オブジェクト名, x座標, y座標)
        float time = gameDirector.time; // GameDirectorのdeltaを使用
        string log = time + "," + stoneName + "," + position.x + "," + position.y;
        logList.Add(log);
    }

    private void OnDestroy()
    {
        // オブジェクトが見つかっていれば削除時のログも記録
        if (objectFound)
        {
            logList.Add(gameDirector.time + "," + stoneName + ",destroyed,destroyed");
        }
        // 最後にCSVにログを保存
        ExportToCSV();
    }

    // リストのデータをCSVファイルに吐き出す
    void ExportToCSV()
    {
        try
        {
            File.WriteAllLines(logFilePath, logList.ToArray());
        }
        catch (System.Exception ex)
        {

        }
    }
}
