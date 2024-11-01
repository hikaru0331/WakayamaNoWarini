using UnityEngine;

public class PlayerSEController : MonoBehaviour
{
    private PlayerBehavior playerBehavior;

    public void Start()
    {
        playerBehavior = GetComponent<PlayerBehavior>();

        playerBehavior.OnJumpCallback += PlayJumpSound;
    }

    private void PlayJumpSound()
    {
        AudioManager.instance_AudioManager.PlaySE(4);
    }
}
