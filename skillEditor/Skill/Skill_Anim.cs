using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Anim : SkillBase
{
    Player player;

    Animator anim;

    public AnimationClip animClip;
    AnimatorOverrideController controller;
    public Skill_Anim(Player _player)
    {
        player = _player;
        anim = player.gameObject.GetComponent<Animator>();
        controller = player.overrideController;
    }
    public void SetAnimClip(AnimationClip _animClip)
    {
        Debug.Log(_animClip.name);
        animClip = _animClip;
        name = _animClip.name;
        controller["Start"] = animClip;
    }
    public override void Init()
    {
        controller["Start"] = animClip;
        animClip = controller["Start"];
    }
    public override void Play()
    {
        base.Play();
        starttime = Time.time;
        isBegin = true;
    }
    public void Begin()
    {
        anim.StopPlayback();
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle1"))
        {
            anim.SetTrigger("Play");
        }
    }
    public override void Stop()
    {
        //base.Play();
        anim.StartPlayback();
    }
    public override void Update(float times)
    {
        if (isBegin&&(times-starttime)>float.Parse(trigger))
        {
            isBegin = false ;
            Begin();
        }
    }
}
