using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class SkillWindow : EditorWindow
{
    Player player;
    List<SkillBase> skillComponents = new List<SkillBase>();
    float currSpeed = 1;
    Vector2 scrllViewPos = new Vector2(0, 0);
    int index;
    string[] xx = new string[] { "null", "动画", "音效", "特效" };
    public void Init(List<SkillBase> skills,Player _player)
    {
        skillComponents = skills;
        player = _player;
        player.currentSkills=skills;
        currSpeed = 1;
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("播放"))
        {
            foreach (var item in skillComponents)
            {
                item.Play();
            }
        }
        if (GUILayout.Button("暂停"))
        {

            foreach (var item in skillComponents)
            {
                item.Stop();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("速度");
        float speed = EditorGUILayout.Slider(currSpeed, 0, 5);
        if (speed!=currSpeed)
        {
            currSpeed = speed;
        }
        GUILayout.BeginHorizontal();
        index = EditorGUILayout.Popup(index, xx);
        if (GUILayout.Button("添加"))
        {
            switch(index)
            {
                case 1:skillComponents.Add(new Skill_Anim(player));break;
                case 2: skillComponents.Add(new Skill_Audio(player)); break;
                case 3: skillComponents.Add(new Skill_Effect(player)); break;
            }
        }
        GUILayout.EndHorizontal();
        scrllViewPos = GUILayout.BeginScrollView(scrllViewPos, false, true);
        foreach (var item in skillComponents)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.name);
            if (GUILayout.Button("删除"))
            {
                skillComponents.Remove(item);
                break;
            }
            GUILayout.EndHorizontal();
            if (item is Skill_Anim)
            {
                ShowSkill_Anim(item as Skill_Anim);
            }else if(item is Skill_Audio)
            {
                ShowSkill_Audio(item as Skill_Audio);
            }
            else if(item is Skill_Effect)
            {
                ShowSkill_Effect(item as Skill_Effect);
            }
        }
        GUILayout.EndScrollView();
    }

    private void ShowSkill_Effect(Skill_Effect _Effect)
    {
        _Effect.trigger = GUILayout.TextField(_Effect.trigger);
        GameObject clip = EditorGUILayout.ObjectField(_Effect.gameClip, typeof(GameObject), false) as GameObject;
        if (clip != _Effect.gameClip)
        {
            _Effect.SetAudioClip(clip);
        }
    }

    private void ShowSkill_Audio(Skill_Audio _Audio)
    {
        _Audio.trigger = GUILayout.TextField(_Audio.trigger);
        AudioClip clip = EditorGUILayout.ObjectField(_Audio.audioClip, typeof(AudioClip), false) as AudioClip;
        if (clip != _Audio.audioClip)
        {
            _Audio.SetAudioClip(clip);
        }
    }

    private void ShowSkill_Anim(Skill_Anim _Anim)
    {
        _Anim.trigger = GUILayout.TextField(_Anim.trigger);
        AnimationClip clip = EditorGUILayout.ObjectField(_Anim.animClip, typeof(AnimationClip), false) as AnimationClip;
        if (clip!=_Anim.animClip)
        {
            _Anim.SetAnimClip(clip);
        }
    }
}
