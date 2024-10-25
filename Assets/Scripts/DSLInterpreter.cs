using UnityEngine;
using System.Text.RegularExpressions;

public class DSLInterpreter : MonoBehaviour
{
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void ExecuteScript(string script)
    {
        // 改行でスクリプトを分割
        string[] lines = script.Split(new[] { ';', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // 各行を処理
        foreach (var line in lines)
        {
            ParseCommand(line.Trim());
        }
    }

    private void ParseCommand(string command)
    {
        // moveコマンド
        if (Regex.IsMatch(command, @"move\(\d+\)"))
        {
            var match = Regex.Match(command, @"move\((\d+)\)");
            int distance = int.Parse(match.Groups[1].Value);
            // characterController.Move(distance);
        }
        // turnコマンド
        else if (Regex.IsMatch(command, @"turn\(\d+\)"))
        {
            var match = Regex.Match(command, @"turn\((\d+)\)");
            int angle = int.Parse(match.Groups[1].Value);
            // characterController.Turn(angle);
        }
        // jumpコマンド
        else if (command == "jump()")
        {
            // characterController.Jump();
        }
        else
        {
            Debug.LogWarning($"Unknown command: {command}");
        }
    }
}