using UnityEngine;

public class CameraController : MonoBehaviour
{
    // カメラが追従する対象（Player）
    [SerializeField] private Transform playerTransform;

    // カメラが動く基準となるy座標の範囲
    private float cameraMoveThreshold = 9.5f;

    // カメラが移動する際のy座標の位置
    private float cameraMoveValueY = 19f;

    /// <summary>
    /// プレイヤーのy座標に基づいてカメラの位置を更新する
    /// </summary>
    void Update()
    {
        // プレイヤーが上のしきい値を超えた場合、カメラを上げる
        if (playerTransform.position.y > this.transform.position.y + cameraMoveThreshold)
        {
            transform.position = new Vector3(transform.position.x, this.transform.position.y + cameraMoveValueY, transform.position.z);
        }
        // プレイヤーが下のしきい値を下回った場合、カメラを下げる
        else if (playerTransform.position.y < this.transform.position.y - cameraMoveThreshold)
        {
            transform.position = new Vector3(transform.position.x, this.transform.position.y - cameraMoveValueY, transform.position.z);
        }
    }
}
