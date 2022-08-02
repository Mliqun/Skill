using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.AI;
using System;

public class SkillXml
{
    public string name;
    public Dictionary<string, List<SkillComponentData>> Skilldic = new Dictionary<string, List<SkillComponentData>>();
}
public class SkillComponentData
{
    public string componentName;
    public string trigger;
    public SkillComponentData(string com,string str)
    {
        componentName = com;
        trigger = str;
    }
}
public class Player : MonoBehaviour
{
    public AnimatorOverrideController overrideController;
     RuntimeAnimatorController controller;
    Animator anim;
    AudioSource audioSource;
    public Transform effectParent;
    public Dictionary<string, List<SkillBase>> skillslist = new Dictionary<string, List<SkillBase>>();
    public  List<SkillBase> currentSkills = new List<SkillBase>();
    public bool isMove;
    Camera camera;
    Transform sx;
    bool sz=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void Init2()
    {
        camera = Camera.main;
        overrideController = new AnimatorOverrideController();
        controller = Resources.Load<RuntimeAnimatorController>("Player");
        overrideController.runtimeAnimatorController = controller;
        anim.runtimeAnimatorController = overrideController;
        audioSource = gameObject.AddComponent<AudioSource>();
        effectParent = transform.Find("effectsparent");
        sx = transform.Find("sx");
       
        LoadSkill();
        Debug.Log(skillslist.Count);
    }
    
    public void DoPlay(string skillName)
    {
        Debug.Log(skillName + "***2");
        if (skillslist.ContainsKey(skillName))
        {
            foreach (var item in skillslist[skillName])
            {
                item.Play();
            }
            if (skillName.Equals("Attack"))
            {
                foreach (var item in World.Ins.monsters.ToArray())
                {
                    if (item != null)
                    {
                        if (Vector3.Distance(gameObject.transform.position, item.transform.position) < 3)
                        {
                            item.GetComponent<Xt>().Kx(10);
                        }
                    }
                }
            }else if(skillName.Equals("2"))
            {
                sx.gameObject.SetActive(true);
                foreach (var item in World.Ins.monsters.ToArray())
                {
                    if (item != null)
                    {
                        //float ang = Vector3.Angle(transform.forward, item.transform.position);
                        //if (ang>330&&ang<360||ang>0&&ang<30)
                        //{
                        //    Debug.Log("进入入入");
                        //    item.GetComponent<Xt>().Kx(10);
                        //}
                        //Vector3 dis = item.transform.position - transform.position;
                        //float dot = Vector3.Dot(dis.normalized, transform.forward);
                        //float ang = Mathf.Acos(dot) * Mathf.Rad2Deg;
                        //if (ang <= 60 / 2 && dis.magnitude <= 4)
                        //{
                        //    item.GetComponent<Xt>().Kx(10);
                        //}
                        if (Vector3.Distance(gameObject.transform.position,item.transform.position)<5)
                        {
                            if (Vector3.Angle(gameObject.transform.forward, item.transform.position - gameObject.transform.position) < 60)
                            {
                                item.GetComponent<Xt>().Kx(10);
                            }
                        }
                    }
                }
                Invoke("ScSx", 1);
            }
        }
    }
    public void ScSx()
    {
        sx.gameObject.SetActive(false);
    }
    public static Player Init(string path)
    {
        if (path!=null)
        {
            string str = "Assets/aa/" + path+".prefab";
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(str);
            if (obj!=null)
            {
                Player player = Instantiate(obj).AddComponent<Player>();
                player.overrideController = new AnimatorOverrideController();
                player.controller = Resources.Load<RuntimeAnimatorController>("Player");
                player.overrideController.runtimeAnimatorController = player.controller;
                player.anim.runtimeAnimatorController = player.overrideController;
                player.audioSource = player.gameObject.AddComponent<AudioSource>();
                player.effectParent = player.transform.Find("effectsparent");
                player.name = path;
                player.LoadSkill();
                return player;
            }
        }
        return null;
    }
    public void LoadSkill()
    {
        string path = "Assets/" + gameObject.name + ".txt";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            List<SkillXml> skillXmls = JsonConvert.DeserializeObject<List<SkillXml>>(json);

            foreach (var item in skillXmls)
            {
                skillslist.Add(item.name, new List<SkillBase>());
                foreach (var ite in item.Skilldic)
                {
                    foreach (var it in ite.Value)
                    {
                        if (ite.Key.Equals("动画"))
                        {
                            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/GameData/Anim/" + it.componentName + ".anim");
                            Skill_Anim _Anim = new Skill_Anim(this);
                            _Anim.SetAnimClip(clip);
                            skillslist[item.name].Add(_Anim);
                            currentSkills.Add(_Anim);
                        }
                        else if(ite.Key.Equals("音效"))
                        {
                            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/GameData/Audio/" + it.componentName + ".mp3");
                            Skill_Audio _Audio =new Skill_Audio(this);
                            _Audio.SetAudioClip(clip);
                            skillslist[item.name].Add(_Audio);
                            currentSkills.Add(_Audio);
                        }
                        else if(ite.Key.Equals("特效"))
                        {
                            GameObject clip = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameData/Effect/Skill/" + it.componentName + ".prefab");
                            Skill_Effect _Effect = new Skill_Effect(this);
                            _Effect.SetAudioClip(clip);
                            skillslist[item.name].Add(_Effect);
                            currentSkills.Add(_Effect);
                        }
                    }
                    
                }
            }
        }
        
    }
    public void SaveData()
    {
        List<SkillXml> skillXmls = new List<SkillXml>();
        foreach (var item in skillslist)
        {
            SkillXml skillXml = new SkillXml();
            skillXml.name = item.Key;
            foreach (var it in item.Value)
            {
                if (it is Skill_Anim)
                {
                    if (!skillXml.Skilldic.ContainsKey("动画"))
                    {
                        skillXml.Skilldic.Add("动画", new List<SkillComponentData>());
                    }
                    skillXml.Skilldic["动画"].Add(new SkillComponentData(it.name, it.trigger));
                }else if (it is Skill_Audio)
                {
                    if (!skillXml.Skilldic.ContainsKey("音效"))
                    {
                        skillXml.Skilldic.Add("音效", new List<SkillComponentData>());
                    }
                    skillXml.Skilldic["音效"].Add(new SkillComponentData(it.name, it.trigger));
                }
                else if(it is Skill_Effect)
                {
                    if (!skillXml.Skilldic.ContainsKey("特效"))
                    {
                        skillXml.Skilldic.Add("特效", new List<SkillComponentData>());
                    }
                    skillXml.Skilldic["特效"].Add(new SkillComponentData(it.name, it.trigger));
                }
            }
            skillXmls.Add(skillXml);
        }
        string nr = JsonConvert.SerializeObject(skillXmls);
        File.WriteAllText("Assets/" + gameObject.name + ".txt", nr);
        AssetDatabase.Refresh();
    }
    public List<SkillBase> AddSkill(string skillName)
    {
        if (skillslist.ContainsKey(skillName))
        {
            return skillslist[skillName];
        }
        skillslist.Add(skillName, new List<SkillBase>());
        return skillslist[skillName];
    }
    public List<SkillBase> GetSkill(string skillName)
    {
        if (skillslist.ContainsKey(skillName))
        {
            return skillslist[skillName];
        }
        return null;
    }
    public void RevSkill(string skillName)
    {
        if (skillslist.ContainsKey(skillName))
        {
            skillslist.Remove(skillName);
        }
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
    GameObject zd;
    Transform aa;
    // Update is called once per frame
    void Update()
    {
        foreach (var item in currentSkills)
        {
            item.Update(Time.time);
        }
        if (isMove)
        {
            if (!gameObject.GetComponent<NavMeshAgent>().pathPending && gameObject.GetComponent<NavMeshAgent>().remainingDistance < gameObject.GetComponent<NavMeshAgent>().stoppingDistance)
            {
                Debug.Log("移动结束");
                gameObject.GetComponent<Animator>().SetBool("run1", false);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                isMove = false;
            }
        }
        camera.transform.LookAt(transform.position);
        camera.transform.position = transform.position + new Vector3(0, 5, -5);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            bool isSz = Physics.Raycast(ray, out RaycastHit hit);
            if (isSz)
            {
                if (hit.collider.tag == "monster")
                {
                    zd = Instantiate(Resources.Load<GameObject>("Sphere"));
                    zd.transform.position = gameObject.transform.position + transform.forward + Vector3.up;
                    zd.AddComponent<ZdScripts>();
                    aa = hit.transform;
                    sz = true;
                    isSz = false;
                    Debug.Log(hit.collider.transform.position);
                }
            }
        }
        if (sz)
        {
            if (Vector3.Distance(zd.transform.position,aa.transform.position)>1)
            {
                zd.transform.position = Vector3.Lerp(zd.transform.position, aa.transform.position, Time.deltaTime * 5);
            }
            else
            {
                aa.GetComponent<Xt>().Kx(10);
                Destroy(zd.gameObject);
                sz = false;
            }
            
        }

    }
}
