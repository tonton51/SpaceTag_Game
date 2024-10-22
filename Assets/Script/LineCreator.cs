using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderer2DConnector : MonoBehaviour
{
    public GameObject springRocket;  // Rocketオブジェクト (2D)
    public GameObject springKuma;    // Kumaオブジェクト (2D)

    private LineRenderer lineRenderer;
    private float distance;

    void Start()
    {
        // LineRendererコンポーネントを取得
        lineRenderer = GetComponent<LineRenderer>();

        // LineRendererの設定
        lineRenderer.positionCount = 2;  // 頂点数は2つ
        float linesize;
        lineRenderer.startWidth =0.1f;  // ラインの太さ
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;  // ワールド座標系を使用
    }

    void Update()
    {
        distance = Vector2.Distance(springKuma.transform.position, springRocket.transform.position);
        float linesize=distance/2f; // 距離によって太さを変える
        lineRenderer.startWidth=linesize;
        if (springRocket != null && springKuma != null)
        {
            // 2つのオブジェクト間にラインを描画
            lineRenderer.SetPosition(0, springRocket.transform.position);  // ロケットの位置
            lineRenderer.SetPosition(1, springKuma.transform.position);    // クマの位置
        }
    }
}