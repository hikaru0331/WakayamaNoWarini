using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScenesManager : MonoBehaviour
{
    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance_AudioManager.PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
        AudioManager.instance_AudioManager.PlaySE(0);
    }

}
