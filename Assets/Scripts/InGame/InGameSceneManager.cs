using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance_AudioManager.PlayBGM(2);
    }

    /// <summary>
    /// ゲームクリア演出後、タイトルシーンに遷移するメソッド
    /// </summary>
    public void OnGameCleared()
    {
        SceneManager.LoadScene("TitleScene");
        AudioManager.instance_AudioManager.PlaySE(1);
        AudioManager.instance_AudioManager.StopBGM();
    }
}
