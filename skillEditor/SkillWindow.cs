using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class SkillWindow :EditorWindow
{
    Player player;
    List<SkillBase> skillComponents;
    float currSpeed = 1;

    int skillComponentIndex = 0;
    Vector2 ScrollViewPos = new Vector2(0, 0);

    public void SetInitSkill(List<SkillBase> _skillComponents,Player _player)
    {
        player = _player;
        currSpeed = 1;
        skillComponents = _skillComponents;
        player.currSkillComponets = skillComponents;
    }
    string[] skillComponent = new string[] { "null", "����", "����", "��Ч" };
    
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("����"))
        {
            foreach (var item in skillComponents)
            {
                item.Play();
            }
        }
        if (GUILayout.Button("ֹͣ"))
        {
            foreach (var item in skillComponents)
            {
                item.Stop();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("�ٶ�");
        float speed = EditorGUILayout.Slider(currSpeed, 0, 5);
        if (speed!=currSpeed)
        {
            currSpeed = speed;
            Time.timeScale = currSpeed;
        }
        GUILayout.BeginHorizontal();
        skillComponentIndex = EditorGUILayout.Popup(skillComponentIndex, skillComponent);
        if (GUILayout.Button("���"))
        {
            switch(skillComponentIndex)
            {
                case 1:
                    skillComponents.Add(new Skill_Anim(player));
                    break;
                case 2:
                    skillComponents.Add(new Skill_Audio(player));
                    break;
                case 3:
                    skillComponents.Add(new Skill_Effects(player));
                    break;
            }
        }
        GUILayout.EndHorizontal();
        ScrollViewPos = GUILayout.BeginScrollView(ScrollViewPos, false, true);
        foreach (var item in skillComponents)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.name);
            if (GUILayout.Button("ɾ��"))
            {
                skillComponents.Remove(item);
                break;
            }
            GUILayout.EndHorizontal();
            if (item is Skill_Anim)
            {
                ShowSkill_Anim(item as Skill_Anim);
            }
            else if (item is Skill_Audio)
            {
                ShowSkill_Audio(item as Skill_Audio);
            }
            else if (item is Skill_Effects)
            {
                ShowSkill_Effects(item as Skill_Effects);
            }
            GUILayout.Space(0.5f);
        }
        GUILayout.EndScrollView();

    }

    private void ShowSkill_Effects(Skill_Effects _Effects)
    {
        _Effects.trigger = EditorGUILayout.TextField(_Effects.trigger);
        GameObject gameClip = EditorGUILayout.ObjectField(_Effects.gameClip, typeof(GameObject), false) as GameObject;
        if (_Effects.gameClip != gameClip)
        {
            _Effects.SetGameClip(gameClip);
        }
    }

    private void ShowSkill_Audio(Skill_Audio _Audio)
    {
        _Audio.trigger = EditorGUILayout.TextField(_Audio.trigger);
        AudioClip audioClip = EditorGUILayout.ObjectField(_Audio.audioClip, typeof(AudioClip), false) as AudioClip;
        if (_Audio.audioClip != audioClip)
        {
            _Audio.SetAudioClip(audioClip);
        }
    }

    void ShowSkill_Anim(Skill_Anim _Anim)
    {
        _Anim.trigger = EditorGUILayout.TextField(_Anim.trigger);
        AnimationClip animClip = EditorGUILayout.ObjectField(_Anim.animClip, typeof(AnimationClip), false) as AnimationClip;
        if (_Anim.animClip != animClip)
        {
            _Anim.SetAnimClip(animClip);
        }
    }
}
