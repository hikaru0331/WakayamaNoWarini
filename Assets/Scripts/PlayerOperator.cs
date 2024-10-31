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
    [SerializeField]
    private DSLInterpreter dSLInterpreter;

    private Dictionary<string, float?[]> commandDic;

    /// <summary>
    /// プレイヤー挙動の初期化
    /// </summary>
    private void Start() 
    {
        playerBehavior.Initialize();
        // commandDic = dSLInterpreter.ReturnDictionary();
    }

    public void OnClick()
    {
        commandDic = dSLInterpreter.ReturnDictionary();
    }

    private void Update() 
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            commandDic = dSLInterpreter.ReturnDictionary();

            ReadDictionary();
        }
    }

    private void ReadDictionary()
    {
        foreach (var item in commandDic)
        {
            if (Regex.IsMatch(item.Key, @"^Jump\d$"))
            {
                Debug.Log("Jump Key: " + item.Key + " : " + item.Value[0] + " : " + item.Value[1]);
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
                Debug.Log("OverwritePhysicsMaterial Key: " + item.Key + " : " + item.Value[0] + " : " + item.Value[1]);
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
