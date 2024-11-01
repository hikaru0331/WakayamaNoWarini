using UnityEngine;

public class PlayerSEController : MonoBehaviour
{
    private PlayerBehavior playerBehavior;

    public void Start()
    {
        playerBehavior = GetComponent<PlayerBehavior>();

        playerBehavior.OnJumpCallback += PlayJumpSound;
        playerBehavior.OnGoalCallback += PlayGoalSound;
    }

    private void PlayJumpSound()
    {
        AudioManager.instance_AudioManager.PlaySE(4);
    }

    private void PlayGoalSound()
    {
        AudioManager.instance_AudioManager.StopBGM();
        AudioManager.instance_AudioManager.PlaySE(7);
    }
}
