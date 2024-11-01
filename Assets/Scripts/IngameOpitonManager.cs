using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameOpitonManager : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private SceneObject titleScene;
    [SerializeField] private GameObject cheatsheetPanel;

    // Start is called before the first frame update
    void Start()
    {
        optionPanel.SetActive(false);
        cheatsheetPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // コントロールキーとオルトキーを押すと、チートシートを表示する。もう一度押すと非表示にする。
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (cheatsheetPanel.activeSelf)
            {
                cheatsheetPanel.SetActive(false);
            }
            else
            {
                cheatsheetPanel.SetActive(true);
            }
        }
    }

    public void OpenOptionPanel()
    {
        optionPanel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        optionPanel.SetActive(false);
    }

    public void ToTitle()
    {
        // タイトル画面に戻る処理
        SceneManager.LoadScene(titleScene);
    }

    public void PlaySE()
    {
        AudioManager.instance_AudioManager.PlaySE(1);
    }
}
