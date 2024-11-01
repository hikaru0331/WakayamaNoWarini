using System;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class PlayerBehavior : MonoBehaviour
{
    // ジャンプ力の最大値をInspectorから設定可能にする
    [Header("ジャンプ設定")]
    public float maxJumpForce = 10f; // ジャンプ力
    public float jumpAngle = 45f; // ジャンプの角度（度単位）

    private PhysicsMaterial2D playerMaterial;

    // プレイヤーが地面にいるかどうかの判定
    private bool isGrounded = true;

    // Rigidbody2Dコンポーネント
    private Rigidbody2D rb;

    // プレイヤーの向き
    [System.NonSerialized]
    public bool facingRight = true;

    // ジャンプ時のイベント
    public event Action OnJumpCallback;
    // 着地時のイベント
    public event Action OnLandCallback;

    private System.Threading.CancellationToken token;

    /// <summary>
    /// PlayerBehaviorの初期化処理
    /// </summary>
    public void Initialize()
    {
        // プレイヤーのRigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        playerMaterial = new PhysicsMaterial2D();
        OverwritePhysicsMaterial(50.0f, 0.1f);
    }

    /// <summary>
    /// ジャンプや向きの変更を処理します。最終的には消す
    /// </summary>
    void Update()
    {
        // 矢印キーでプレイヤーの向きを変更
        HandleDirectionChange();

        // Spaceキーが押され、プレイヤーが地面にいるときにジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(maxJumpForce, jumpAngle);
        }
    }

    /// <summary>
    /// 矢印キーの入力によってプレイヤーの向きを変更します。
    /// </summary>
    public void HandleDirectionChange()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 左右の入力を取得。最終的には消す行

        // 右向きに移動する場合
        if (horizontalInput > 0 && !facingRight)
        {
            Flip(); // プレイヤーの向きを右に変更
            Debug.Log("右向き");
        }
        // 左向きに移動する場合
        else if (horizontalInput < 0 && facingRight)
        {
            Flip(); // プレイヤーの向きを左に変更
            Debug.Log("左向き");
        }
    }

    /// <summary>
    /// プレイヤーを現在の向きに応じて放物線状にジャンプさせます。
    /// </summary>
    public async UniTask Jump(float? maxJumpForce, float? jumpAngle)
    {
        if(isGrounded)
        {
            // ジャンプ時のイベントを発火
            OnJumpCallback?.Invoke();

            await UniTask.WaitForSeconds(0.3f, cancellationToken: token);

            // ジャンプ力の最大値を0.0fから20.0fの範囲に制限
            float clampedJumpForce = Mathf.Clamp((float)maxJumpForce, 0.0f, 21.0f);

            // ジャンプの角度をラジアンに変換
            float angleInRadians = (float)jumpAngle * Mathf.Deg2Rad;

            // 右向きか左向きかでジャンプ方向を決定
            float jumpDirectionX = facingRight ? Mathf.Cos(angleInRadians) : -Mathf.Cos(angleInRadians);

            // ジャンプ力のベクトルを計算
            Vector2 jumpForce = new Vector2(jumpDirectionX, Mathf.Sin(angleInRadians)) * clampedJumpForce;

            // プレイヤーに力を加える
            rb.AddForce(jumpForce, ForceMode2D.Impulse);

            // ジャンプ中は地面から離れる
            isGrounded = false;
        }        
    }

    /// <summary>
    /// プレイヤーの向きを反転させます。
    /// </summary>
    public void Flip()
    {
        facingRight = !facingRight; // 向きを反転
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // X軸方向のスケールを反転
        transform.localScale = scaler;
        Debug.Log(facingRight ? "右向き" : "左向き");
    }

    public void OverwritePhysicsMaterial(float? friction, float? bounciness)
    {
        playerMaterial.friction = (float)friction;
        playerMaterial.bounciness = Mathf.Clamp((float)bounciness, 0.1f, 0.9f);
        rb.sharedMaterial = playerMaterial;
    }

    //GroundJudgerが地面に触れている場合のみ、isGroundedをtrueにする
    void OnTriggerEnter2D(Collider2D other) 
    { 
        isGrounded = true;
        OnLandCallback?.Invoke();
    }
}
