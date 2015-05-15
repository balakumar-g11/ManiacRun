/**
 * File : SoundManager.cs
 * Author : Balakumar Marimuthu
 * Use : The script is used to manage the animations in the game
 **/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour
{
    //Delegate update function
    public delegate void AnimationHandle();
    public AnimationHandle animationState;

    private Animator anim;
    private int jumpHash = Animator.StringToHash("GapJump");
    private int runStateHash = Animator.StringToHash("Base Layer.Sprint");

    //Variable private field 
    private PlayerControl controller;
    private float speed_Run;
    private float default_Speed_Run;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        controller = this.GetComponent<PlayerControl>();
        default_Speed_Run = GameAttribute.gameAttribute.speed;
        speed_Run = default_Speed_Run;
        animationState = Run;
    }

    void Update()
    {
        if (animationState != null)
        {
            animationState();
        }
    }

    //Run State
    public void Run()
    {
        speed_Run = (GameAttribute.gameAttribute.speed);
        anim.SetFloat("speed", 4.0f);
    }

    //Jump State
    public void Jump()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.nameHash == runStateHash)
        {
            anim.SetTrigger(jumpHash);
        }
    }

    public void LongJump()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.nameHash == runStateHash)
        {
            anim.SetTrigger(jumpHash);
        }
    }

    public void Roll()
    { 
    
    }

    //Dead State
    public void Dead()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.nameHash == runStateHash)
        {
            anim.SetTrigger(jumpHash);
        }
    }
}
