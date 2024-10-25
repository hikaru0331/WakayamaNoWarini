using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerOperator : MonoBehaviour
{
    /// <summary>
    /// プレイヤー挙動クラス
    /// </summary>
    [SerializeField]
    private PlayerBehavior playerBehavior;

    private Dictionary<string, float[]> testDict = new Dictionary<string, float[]>();

    private float[] jumpArguments = { 12.0f, 45.0f };
    private float[] physicsArguments;

    /// <summary>
    /// プレイヤー挙動の初期化
    /// </summary>
    private void Start() 
    {
        playerBehavior.Initialize();
        physicsArguments = new float[] { 50.0f, 0.3f };

        // ディクショナリの初期化
        testDict.Add("OverwritePhysicsMaterial1", physicsArguments);
        testDict.Add("TurnLeft1", null);
        // testDict.Add("TurnRight1", null);
        testDict.Add("Jump1", jumpArguments);
        testDict.Add("Jump2", jumpArguments);
        testDict.Add("TypoCommand1", null);

        ReadDictionary();
    }

    private void ReadDictionary()
    {
        foreach (var item in testDict)
        {
            if (Regex.IsMatch(item.Key, @"^Jump\d$"))
            {
                Debug.Log("Jump Key: " + item.Key + " : " + item.Value);
                playerBehavior.Jump(item.Value[0], item.Value[1]);
            }
            else if (Regex.IsMatch(item.Key, @"^TurnLeft\d$"))
            {                
                playerBehavior.facingRight = true;
                playerBehavior.Flip();
            }
            else if (Regex.IsMatch(item.Key, @"^TurnRight\d$"))
            {
                playerBehavior.facingRight = false;
                playerBehavior.Flip();
            }
            else if (Regex.IsMatch(item.Key, @"^OverwritePhysicsMaterial\d$"))
            {
                playerBehavior.OverwritePhysicsMaterial(item.Value[0], item.Value[1]);
            }
            else
            {
                // その他のキーに対する処理
                Debug.Log("例外処理");
            }
        }
    }
}
