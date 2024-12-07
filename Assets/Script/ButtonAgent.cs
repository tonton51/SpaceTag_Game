using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class ButtonClickAgent : Agent
{
    public Button targetButton; // ターゲットのボタン
    private bool isButtonClicked;

    void Start()
    {
        // ボタンにクリックイベントリスナーを追加
        targetButton.onClick.AddListener(() => OnButtonClicked());
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // ボタンのスクリーン上の正規化された位置を観測
        Vector3 buttonPosition = targetButton.transform.position;
        sensor.AddObservation(buttonPosition.x / Screen.width);
        sensor.AddObservation(buttonPosition.y / Screen.height);

        // ボタンがクリックされた状態を観測
        sensor.AddObservation(isButtonClicked ? 1.0f : 0.0f);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // エージェントが自動操作でクリックする場合
        var discreteActions = actionBuffers.DiscreteActions;
        int action = discreteActions[0];

        if (action == 1) // 1: ボタンをクリック
        {
            SimulateMouseClick();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            Vector3 mousePos = Input.mousePosition;
            if (IsMouseOverButton(mousePos))
            {
                discreteActions[0] = 1; // ボタンをクリックする行動
            }
            else
            {
                discreteActions[0] = 0; // 何もしない
            }
        }
        else
        {
            discreteActions[0] = 0; // 何もしない
        }
    }

    public override void OnEpisodeBegin()
    {
        // 状態をリセット
        isButtonClicked = false;
    }

    private void OnButtonClicked()
    {
        // ボタンがクリックされたときに報酬を与える
        if (!isButtonClicked)
        {
            AddReward(1.0f);
            isButtonClicked = true;
        }
        EndEpisode(); // エピソード終了
    }

    private bool IsMouseOverButton(Vector3 mousePosition)
    {
        // マウス位置がボタン領域内にあるか確認
        RectTransform rectTransform = targetButton.GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(mousePosition);
        return rectTransform.rect.Contains(localMousePosition);
    }

    private void SimulateMouseClick()
    {
        // エージェントがボタンをクリックするシミュレーション
        targetButton.onClick.Invoke();
    }
}
