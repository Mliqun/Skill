using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Effect : SkillBase
{
    Player player;
    public GameObject gameClip;
    ParticleSystem particleSystem;
    GameObject obj;

    public Skill_Effect(Player _player)
    {
        player = _player;
    }
    public void SetAudioClip(GameObject clip)
    {
        if (player.effectParent.childCount>0)
        {
            GameObject.Destroy(player.effectParent.GetChild(0).gameObject);
        }
        
        gameClip = clip;
        if (gameClip.GetComponent<ParticleSystem>())
        {
            obj = GameObject.Instantiate(gameClip, player.effectParent);
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
        name = clip.name;
    }
    public override void Init()
    {
        if (gameClip.GetComponent<ParticleSystem>())
        {
            obj = GameObject.Instantiate(gameClip, player.effectParent);
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
    }
    public override void Play()
    {
        startTime = Time.time;
        isBegin = true;
    }
    public override void Stop()
    {
        if (particleSystem!=null)
        {
            particleSystem.Stop();
        }
    }
    public override void Update(float times)
    {
        if (isBegin && (times - startTime) > float.Parse(trigger))
        {
            isBegin = false;
            Begin();
        }
    }
    public void Begin()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}
