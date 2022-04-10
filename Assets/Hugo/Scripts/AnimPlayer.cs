using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    private Player player;
    private Animator animator;

    public AudioManager audioManager;
    public AudioSource walkAudioSource;
    public AudioSource jumpAudioSource;
    public AudioSource throwAudioSource;
    public AudioSource breakAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();

        animator.SetBool("isJumping", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", true);
        animator.SetBool("isThrowing", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowProjectileAnim()
    {
        animator.SetBool("isThrowing", true);
        animator.SetBool("isJumping", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", false);

        audioManager.PlayEffect(throwAudioSource, throwAudioSource.clip);
    }

    public void IdleAnim()
    {
        animator.SetBool("isThrowing", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", true);

    }

    public void WalkAnim()
    {
        animator.SetBool("isThrowing", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
    }

    public void JumpAnim()
    {
        animator.SetBool("isThrowing", false);
        animator.SetBool("isJumping", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", false);

        audioManager.PlayEffect(jumpAudioSource, jumpAudioSource.clip);
    }
}