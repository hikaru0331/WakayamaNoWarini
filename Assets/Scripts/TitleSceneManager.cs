using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleSceneManager : MonoBehaviour
{
    public Button StartButton;

    private void Start()
    {
        AudioManager.instance_AudioManager.PlayBGM(1);
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
        AudioManager.instance_AudioManager.PlaySE(1);
        AudioManager.instance_AudioManager.StopBGM();
    }

}
