using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class DSLInterpreter : MonoBehaviour
{
    private CharacterController characterController;
    private string command = "jump(5, 45);\nTurnLeft(90);\nTurnRight(45);\nOverwritePhysicsMaterial(0.5, 0.3);\n";

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        ExecuteScript(command);
    }

    public void ExecuteScript(string script)
    {
        // 改行でスクリプトを分割
        string[] lines = script.Split(new[] { ';', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // 分割した配列をそのままParseCommandに渡す
        Dictionary<string, float?[]> commandDic = ParseCommand(lines);
        // 辞書の内容を表示するメソッドを呼び出す
        DisplayCommandDetails(commandDic);
    }

    private Dictionary<string, float?[]> ParseCommand(string[] commands)
    {
        Dictionary<string, float?[]> commandDic = new Dictionary<string, float?[]>();
        int jumpIndex = 1; // jump用のインデックス
        int turnLeftIndex = 1; // TurnLeft用のインデックス
        int turnRightIndex = 1; // TurnRight用のインデックス
        int overwritePhysicsMaterialIndex = 1; // OverwritePhysicsMaterial用のインデックス

        foreach (var command in commands)
        {
            string trimmedCommand = command.Trim(); // 各行の前後の空白をトリミング

            // jumpコマンド
            if (Regex.IsMatch(trimmedCommand, @"jump\(\d+(\.\d+)?,\s*\d+(\.\d+)?\)"))
            {
                var match = Regex.Match(trimmedCommand, @"jump\(\d+(\.\d+)?,\s*\d+(\.\d+)?\)");
                float jumpForce = float.Parse(match.Groups[1].Value); // 1つ目の引数
                float jumpAngle = float.Parse(match.Groups[2].Value);  // 2つ目の引数

                float?[] arguments = { jumpForce, jumpAngle };
                commandDic.Add("jump" + jumpIndex, arguments);
                jumpIndex++; // インデックスをインクリメント
            }
            // TurnLeftコマンド
            else if (Regex.IsMatch(trimmedCommand, @"TurnLeft\((\d+(\.\d+)?)\)", RegexOptions.IgnoreCase))
            {
                var match = Regex.Match(trimmedCommand, @"TurnLeft\((\d+(\.\d+)?)\)", RegexOptions.IgnoreCase);
                float angle = float.Parse(match.Groups[1].Value);

                float?[] arguments = { angle , null};
                commandDic.Add("TurnLeft" + turnLeftIndex, arguments);
                turnLeftIndex++; // インデックスをインクリメント
            }
            // TurnRightコマンド
            else if (Regex.IsMatch(trimmedCommand, @"TurnRight\((\d+(\.\d+)?)\)", RegexOptions.IgnoreCase))
            {
                var match = Regex.Match(trimmedCommand, @"TurnRight\((\d+(\.\d+)?)\)", RegexOptions.IgnoreCase);
                float angle = float.Parse(match.Groups[1].Value);

                float?[] arguments = { angle , null};
                commandDic.Add("TurnRight" + turnRightIndex, arguments);
                turnRightIndex++; // インデックスをインクリメント
            }
            // OverwritePhysicsMaterialコマンド
            else if (Regex.IsMatch(trimmedCommand, @"OverwritePhysicsMaterial\(\d+(\.\d+)?,\s*\d+(\.\d+)?\)", RegexOptions.IgnoreCase))
            {
                var match = Regex.Match(trimmedCommand, @"OverwritePhysicsMaterial\((\d+(\.\d+)?),\s*(\d+(\.\d+)?)\)", RegexOptions.IgnoreCase);
                float friction = float.Parse(match.Groups[1].Value); // 1つ目の引数
                float bounciness = float.Parse(match.Groups[3].Value); // 2つ目の引数

                float?[] arguments = { friction, bounciness };
                commandDic.Add("OverwritePhysicsMaterial" + overwritePhysicsMaterialIndex, arguments);
                overwritePhysicsMaterialIndex++; // インデックスをインクリメント
            }
            else
            {
                Debug.LogWarning($"Unknown command: {trimmedCommand}");
            }
        }

        return commandDic; // 辞書を返す
    }

    // コマンドの内容を表示するメソッド、本来はOperatorで実装するべき
    private void DisplayCommandDetails(Dictionary<string, float?[]> commandDic)
    {
        foreach (var entry in commandDic)
        {
            string commandName = entry.Key;
            float?[] arguments = entry.Value;

            // 正規表現を用いて場合分け
            if (Regex.IsMatch(commandName, @"^jump\d+$")) // jumpの後に数字が続く場合
            {
                float? jumpForce = arguments[0];
                float? jumpAngle = arguments[1];
                Debug.Log($"Jump 力：{jumpForce} 角度：{jumpAngle}");
            }
            else if (Regex.IsMatch(commandName, @"^TurnLeft\d+$")) // TurnLeftの後に数字が続く場合
            {
                float? angle = arguments[0];
                Debug.Log($"TurnLeft 角度：{angle}");
            }
            else if (Regex.IsMatch(commandName, @"^TurnRight\d+$")) // TurnRightの後に数字が続く場合
            {
                float? angle = arguments[0];
                Debug.Log($"TurnRight 角度：{angle}");
            }
            else if (Regex.IsMatch(commandName, @"^OverwritePhysicsMaterial\d+$")) // OverwritePhysicsMaterialの後に数字が続く場合
            {
                float? friction = arguments[0];
                float? bounciness = arguments[1];
                Debug.Log($"OverwritePhysicsMaterial 摩擦：{friction} 弾力性：{bounciness}");
            }
        }
    }
}