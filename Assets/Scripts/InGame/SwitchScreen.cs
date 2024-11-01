using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SwitchScreen : MonoBehaviour
{
    [SerializeField] private float transitionTime = 0.5f;

    [SerializeField] private TMP_InputField commandPanel;
    private RectTransform commandPanelRectTransform;
    [SerializeField] private TextMeshProUGUI commandPanelText;

    [SerializeField] private GameObject screen;

    // 二次元配列でスクリーンの座標を管理
    private Vector3[,] screenPos = new Vector3[2, 2]
    {
        { new Vector3(195, 60, 0), new Vector3(375, 250, 0) }, // コマンド入力モード
        { new Vector3(100, -15, 0), new Vector3(565, 380, 0) } // プレイモード
    };

    private Vector3[,] commandPanelPos = new Vector3[2, 2]
    {
        { new Vector3(-200, 0, 0), new Vector3(375, 420, 0) }, // コマンド入力モード
        { new Vector3(-315, 0, 0), new Vector3(140, 420, 0) } // プレイモード
    };

    private int[] fontSize = { 16, 5 };

    // Start is called before the first frame update
    void Start()
    {
        commandPanelRectTransform = commandPanel.GetComponent<RectTransform>();
        ExpandScreen(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ExpandScreen(1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExpandScreen(0);
        }
    }

    void ExpandScreen(int mode)
    {
        screen.transform.DOLocalMove(screenPos[mode, 0], transitionTime);
        screen.transform.DOScale(screenPos[mode, 1], transitionTime);

        commandPanel.transform.DOLocalMove(commandPanelPos[mode, 0], transitionTime);
        commandPanelRectTransform.DOSizeDelta(commandPanelPos[mode, 1], transitionTime);
        DOTween.To(() => commandPanelText.fontSize, x => commandPanelText.fontSize = x, fontSize[mode], transitionTime);
    }
}
