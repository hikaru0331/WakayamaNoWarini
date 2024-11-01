using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    float blinkInterval = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkLogo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // logoを一定間隔で点滅させる
    private IEnumerator BlinkLogo()
    {
        while (true)
        {
            logo.SetActive(false);
            yield return new WaitForSeconds(blinkInterval);
            logo.SetActive(true);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
