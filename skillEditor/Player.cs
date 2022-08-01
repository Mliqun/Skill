using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

public class SkillBB
{
    public string name;
    public Dictionary<string, List<SkillComponentsData>> skillComponents = new Dictionary<string, List<SkillComponentsData>>();
}
public class SkillComponentsData
{
    public string ComponentName;
    public string trigger;
    public SkillComponentsData(string cn, string str)
    {
        ComponentName = cn;
        trigger = str;
    }
}
public class Player : MonoBehaviour
{
    public Dictionary<string, List<SkillBase>> skillsList = new Dictionary<string, List<SkillBase>>();

    RuntimeAnimatorController controller;

    public AnimatorOverrideController overrideController;

    public Transform effectsparent;

    AudioSource audioSource;
    //Player

    Animator anim;


    public List<SkillBase> currSkillComponets = new List<SkillBase>();
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void InitData()
    {
        overrideController = new AnimatorOverrideController();
        controller = Resources.Load<RuntimeAnimatorController>("Player");
        overrideController.runtimeAnimatorController = controller;
        anim.runtimeAnimatorController = overrideController;
        audioSource = gameObject.AddComponent<AudioSource>();
        effectsparent = transform.Find("effectsparent");
        //gameObject.name = path;
        //LoadAllSkill();
    }
    /// <summary>
    /// Player初始化  
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Player Init(string path)
    {
        Debug.Log(path);
        if (path!=null)
        {
            string str = "Assets/aaa" + path + ".prefab";
            //GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(str);
            GameObject obj = Resources.Load<GameObject>(path);
            if (obj!=null)
            {
                Player player = Instantiate(obj).AddComponent<Player>();
                player.overrideController = new AnimatorOverrideController();
                player.controller = Resources.Load<RuntimeAnimatorController>("Player");
                player.overrideController.runtimeAnimatorController = player.controller;
                player.anim.runtimeAnimatorController = player.overrideController;
                player.audioSource = player.gameObject.AddComponent<AudioSource>();
                player.effectsparent = player.transform.Find("effectsparent");
                player.gameObject.name = path;
                //player.LoadAllSkill();
                return player;
            }
        }
        return null;
    }
    public void play()
    {
        //根据条件判断播放不同的组件  遍历   skillsList  item.player
        //_Anim.Play();
        //声音的
        //特效的
        foreach (var item in currSkillComponets)
        {
            Debug.Log("进入");
            item.Play();
        }
    }
    public void LoadAllSkill(string skillName)
    {
        Debug.Log("name" + gameObject.name);
        if (File.Exists("Assets/" +gameObject.name+".txt"))
        {
            string str=File.ReadAllText("Assets/" + gameObject.name + ".txt");
            List<SkillBB> skills = JsonConvert.DeserializeObject<List<SkillBB>>(str);
            foreach (var item in skills)
            {
                if (item.name == skillName)
                {
                    currSkillComponets.Clear();
                    foreach (var ite in item.skillComponents)
                    {
                        foreach (var it in ite.Value)
                        {
                            if (ite.Key.Equals("动画"))
                            {
                                print(it.ComponentName);
                                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/GameData/Anim/" + it.ComponentName + ".anim");
                                Skill_Anim _Anim = new Skill_Anim(this);
                                _Anim.SetAnimClip(clip);
                                _Anim.SetTrigger(it.trigger);
                                //skillsList[item.name].Add(_Anim);
                                currSkillComponets.Add(_Anim);
                            }
                            else if (ite.Key.Equals("音效"))
                            {
                                AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/GameData/Audio/" + it.ComponentName + ".mp3");
                                Skill_Audio _Audio = new Skill_Audio(this);
                                _Audio.SetAudioClip(clip);
                                _Audio.SetTrigger(it.trigger);
                                //skillsList[item.name].Add(_Audio);
                                currSkillComponets.Add(_Audio);
                            }
                            else if (ite.Key.Equals("特效"))
                            {
                                GameObject effect = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameData/Effect/Skill/" + it.ComponentName + ".prefab");
                                Skill_Effects _Effects = new Skill_Effects(this);
                                _Effects.SetGameClip(effect);
                                _Effects.SetTrigger(it.trigger);
                                //skillsList[item.name].Add(_Effects);
                                currSkillComponets.Add(_Effects);
                            }
                        }
                    }
                }
                
            }
        }
    }
   
    public void SaveData()
    {
        List<SkillBB> skills = new List<SkillBB>();
        foreach (var item in skillsList)
        {
            SkillBB skillBB = new SkillBB();
            skillBB.name = item.Key;
            foreach (var ite in item.Value)
            {
                if (ite is Skill_Anim)
                {
                    if (!skillBB.skillComponents.ContainsKey("动画"))
                    {
                        skillBB.skillComponents.Add("动画", new List<SkillComponentsData>());
                    }
                    skillBB.skillComponents["动画"].Add(new SkillComponentsData(ite.name, ite.trigger));
                }
                else if(ite is Skill_Audio)
                {
                    if (!skillBB.skillComponents.ContainsKey("音效"))
                    {
                        skillBB.skillComponents.Add("音效", new List<SkillComponentsData>());
                    }
                    skillBB.skillComponents["音效"].Add(new SkillComponentsData(ite.name, ite.trigger));

                }
                else if (ite is Skill_Effects)
                {
                    if (!skillBB.skillComponents.ContainsKey("特效"))
                    {
                        skillBB.skillComponents.Add("特效", new List<SkillComponentsData>());
                    }
                    skillBB.skillComponents["特效"].Add(new SkillComponentsData(ite.name, ite.trigger));

                }
            }
            skills.Add(skillBB);
        }
        string str = JsonConvert.SerializeObject(skills);
        File.WriteAllText("Assets/" + gameObject.name + ".txt", str);
        AssetDatabase.Refresh();
    }
    public List<SkillBase> AddNewSkill(string newSkillName)
    {
        if (skillsList.ContainsKey(newSkillName))
        {
            return skillsList[newSkillName];
        }
        skillsList.Add(newSkillName, new List<SkillBase>());
        Debug.Log(skillsList[newSkillName]);
        return skillsList[newSkillName];
    }
    public List<SkillBase> GetSkill(string skillName)
    {
        if (skillsList.ContainsKey(skillName))
        {
            return skillsList[skillName];//List<skillbase>
        }
        return null;
    }
    public void RevSkill(string newSkillName)
    {
        if (skillsList.ContainsKey(newSkillName))
        {
            skillsList.Remove(newSkillName);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in currSkillComponets)
        {
            item.Update(Time.time);
        }
    }
}
