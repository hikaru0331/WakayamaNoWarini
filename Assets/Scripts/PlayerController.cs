using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ジャンプ力の最大値をInspectorから設定可能にする
    [Header("ジャンプ設定")]
    public float maxJumpForce = 10f; // ジャンプ力
    public float jumpAngle = 45f; // ジャンプの角度（度単位）

    // プレイヤーが地面にいるかどうかの判定
    private bool isGrounded = true;

    // Rigidbody2Dコンポーネント
    private Rigidbody2D rb;

    // プレイヤーの向き
    private bool facingRight = true;

    /// <summary>
    /// 初期化処理を行います。Rigidbody2Dコンポーネントを取得します。
    /// </summary>
    void Start()
    {
        // プレイヤーのRigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 毎フレーム、ユーザー入力をチェックし、ジャンプや向きの変更を処理します。
    /// </summary>
    void Update()
    {
        // 矢印キーでプレイヤーの向きを変更
        HandleDirectionChange();

        // Spaceキーが押され、プレイヤーが地面にいるときにジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    /// <summary>
    /// 矢印キーの入力によってプレイヤーの向きを変更します。
    /// </summary>
    void HandleDirectionChange()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 左右の入力を取得

        // 右向きに移動する場合
        if (horizontalInput > 0 && !facingRight)
        {
            Flip(); // プレイヤーの向きを右に変更
        }
        // 左向きに移動する場合
        else if (horizontalInput < 0 && facingRight)
        {
            Flip(); // プレイヤーの向きを左に変更
        }
    }

    /// <summary>
    /// プレイヤーを現在の向きに応じて放物線状にジャンプさせます。
    /// </summary>
    void Jump()
    {
        // ジャンプの角度をラジアンに変換
        float angleInRadians = jumpAngle * Mathf.Deg2Rad;

        // 右向きか左向きかでジャンプ方向を決定
        float jumpDirectionX = facingRight ? Mathf.Cos(angleInRadians) : -Mathf.Cos(angleInRadians);

        // ジャンプ力のベクトルを計算
        Vector2 jumpForce = new Vector2(jumpDirectionX, Mathf.Sin(angleInRadians)) * maxJumpForce;

        // プレイヤーに力を加える
        rb.AddForce(jumpForce, ForceMode2D.Impulse);

        // ジャンプ中は地面から離れる
        isGrounded = false;
    }

    /// <summary>
    /// プレイヤーの向きを反転させます。
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight; // 向きを反転
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // X軸方向のスケールを反転
        transform.localScale = scaler;
    }

    /// <summary>
    /// 地面に着地したかどうかを判定し、地面に戻った場合はisGroundedをtrueにします。
    /// </summary>
    /// <param name="collision">衝突したオブジェクト</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に着地した場合にのみisGroundedをtrueにする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
