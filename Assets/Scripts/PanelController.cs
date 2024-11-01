using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelController : MonoBehaviour
{
    public GameObject optionPanel;
    public Button optionButton;

    public Button backButton;


    // Start is called before the first frame update
    void Start()
    {
        optionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPanel()
    {
        optionPanel.SetActive(true);
        AudioManager.instance_AudioManager.PlaySE(1);

    }

    public void ClosePanel()
    {
        optionPanel.SetActive(false);
        AudioManager.instance_AudioManager.PlaySE(3);

    }
}
