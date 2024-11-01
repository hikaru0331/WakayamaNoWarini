using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameOpitonManager : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private SceneObject titleScene;

    // Start is called before the first frame update
    void Start()
    {
        optionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
