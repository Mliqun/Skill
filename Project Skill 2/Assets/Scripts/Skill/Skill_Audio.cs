using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Audio :SkillBase
{
    Player player;
    public AudioClip audioClip;
    AudioSource audioSource;

    public Skill_Audio(Player _player)
    {
        player = _player;
        audioSource = player.gameObject.GetComponent<AudioSource>();
    }
    public void SetAudioClip(AudioClip clip)
    {
        audioClip = clip;
        name = clip.name;
        audioSource.clip = audioClip;
    }
    public override void Init()
    {
        audioSource.clip = audioClip;
    }
    public override void Play()
    {
        startTime = Time.time;
        isBegin = true;
    }
    public override void Stop()
    {
        audioSource.Stop();
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
        audioSource.Play();
    }
}
