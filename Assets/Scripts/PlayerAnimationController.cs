using UnityEngine;
public class PlayerAnimationController : MonoBehaviour
{
    private PlayerBehavior playerBehavior;
    private Animator playerAnimator;

    public void Start()
    {
        playerBehavior = GetComponent<PlayerBehavior>();
        playerAnimator = GetComponent<Animator>();

        playerBehavior.OnJumpCallback += PlayJumpAnimation;
        playerBehavior.OnLandCallback += StopJumpAnimation;
    }

    private void PlayJumpAnimation()
    {
        playerAnimator.SetBool("isJumping", true);
    }

    private void StopJumpAnimation()
    {
        playerAnimator.SetBool("isJumping", false);
    }
}
