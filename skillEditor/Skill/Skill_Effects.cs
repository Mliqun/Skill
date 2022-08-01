using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Effects : SkillBase
{
    public GameObject gameClip;
    Player player;

    ParticleSystem particleSystem;

    GameObject obj;
    public Skill_Effects(Player _player)
    {
        player = _player;

    }
    public void SetGameClip(GameObject _gameClip)
    {
        gameClip = _gameClip;
        if (gameClip.GetComponent<ParticleSystem>())
        {
            obj = GameObject.Instantiate(gameClip, player.effectsparent);
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
        name = gameClip.name;
    }
    public override void Init()
    {
        if (gameClip.GetComponent<ParticleSystem>())
        {
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
    }
    public override void Play()
    {
        starttime = Time.time;
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
        if (isBegin&&(times-starttime)>float.Parse(trigger))
        {
            isBegin = false;
            Begin();
        }
    }
    private void Begin()
    {
        if (particleSystem!=null)
        {
            particleSystem.Play();
        }
    }
}
