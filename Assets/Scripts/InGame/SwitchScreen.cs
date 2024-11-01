using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SwitchScreen : MonoBehaviour
{
    [SerializeField] private float transitionTime = 0.5f;

    [SerializeField] private GameObject commandObject;
    private RectTransform commandRectTransform;
    [SerializeField] private TMP_InputField commandPanel;
    private RectTransform commandPanelRectTransform;
    [SerializeField] private TextMeshProUGUI commandPanelText;

    [SerializeField] private GameObject screen;

    [SerializeField] private GameObject button;

    // 二次元配列でスクリーンの座標を管理
    private Vector3[,] screenPos = new Vector3[2, 2]
    {
        { new Vector3(195, 60, 0), new Vector3(375, 250, 0) }, // コマンド入力モード
        { new Vector3(100, -15, 0), new Vector3(565, 380, 0) } // プレイモード
    };

    private Vector3[,] commandPanelPos = new Vector3[2, 2]
    {
        { new Vector3(-200, 15, 0), new Vector3(390, 390, 0) }, // コマンド入力モード
        { new Vector3(-295, 110, 0), new Vector3(200, 200, 0) } // プレイモード
    };

    private int[] fontSize = { 16, 5 };
    private int[] commandPanelTop = { 35, 15 };
    private int[] buttonPosY = { 0, -160 };

    // Start is called before the first frame update
    void Start()
    {
        commandRectTransform = commandObject.GetComponent<RectTransform>();
        commandPanelRectTransform = commandPanel.GetComponent<RectTransform>();
        ExpandScreen(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ExpandScreen(1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            ExpandScreen(0);
        }
    }

    public void ExpandScreen(int mode)
    {
        screen.transform.DOLocalMove(screenPos[mode, 0], transitionTime);
        screen.transform.DOScale(screenPos[mode, 1], transitionTime);

        commandObject.transform.DOLocalMove(commandPanelPos[mode, 0], transitionTime);
        commandRectTransform.DOSizeDelta(commandPanelPos[mode, 1], transitionTime);

        // フォントサイズをアニメーション
        DOTween.To(() => commandPanelText.fontSize, x => commandPanelText.fontSize = x, fontSize[mode], transitionTime);

        // Topの値をアニメーション、Bottomは0固定
        DOTween.To(
            () => commandPanelRectTransform.offsetMax.y,
            y => commandPanelRectTransform.offsetMax = new Vector2(commandPanelRectTransform.offsetMax.x, y),
            -commandPanelTop[mode],  // 目標Topの値を設定
            transitionTime
        );

        // Bottomを0に固定
        commandPanelRectTransform.offsetMin = new Vector2(commandPanelRectTransform.offsetMin.x, 0);

        // ボタンの位置をアニメーション
        button.transform.DOLocalMoveY(buttonPosY[mode], transitionTime);
    }
}
