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

    private float[] arguments = { 12.0f, 45.0f };

    /// <summary>
    /// プレイヤー挙動の初期化
    /// </summary>
    private void Start() 
    {
        playerBehavior.Initialize();

        // ディクショナリの初期化
        testDict.Add("Jump1", arguments);
        testDict.Add("TurnLeft1", arguments);
        testDict.Add("TurnRight1", arguments);
        testDict.Add("Jump2", arguments);
        testDict.Add("TypoCommand1", arguments);

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
                // "TurnLeft1" などのキーに対する処理
                Debug.Log("TurnLeft Key: " + item.Key + " : " + item.Value);
            }
            else if (Regex.IsMatch(item.Key, @"^TurnRight\d$"))
            {
                // "TurnRight1" などのキーに対する処理
                Debug.Log("TurnRight Key: " + item.Key + " : " + item.Value);
            }
            else
            {
                // その他のキーに対する処理
                Debug.Log("例外処理");
            }
        }
    }
}
