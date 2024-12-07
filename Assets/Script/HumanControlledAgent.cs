using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using UnityEngine.UI;
public class HumanControlledAgent : Agent
{
    public Button targetButton; // ボタン参照
    int count=0;

    // managerで5回過ぎたらエージェントをfalseにする
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        if (Input.GetMouseButtonDown(0))
        {
            // ボタンがクリック範囲内の場合、アクションを記録
            Vector3 mousePos = Input.mousePosition;
            if (IsMouseOverButton(mousePos))
            {
                discreteActions[0] = 1; // クリックする
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


    private bool IsMouseOverButton(Vector3 mousePosition)
    {
        count++;
        Debug.Log(count);
        if(count<5){
            RectTransform rectTransform = targetButton.GetComponent<RectTransform>();
            Vector2 localMousePosition = rectTransform.InverseTransformPoint(mousePosition);
            return rectTransform.rect.Contains(localMousePosition);
        }else{
            return false;
        }
    }
}
