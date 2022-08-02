using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class World : SingleTon<World>
{
    public GameObject MyYg;
    public Transform canvas;
    public List<ModelCountorl> models=new List<ModelCountorl>();
    public string sceneName;
    Transform npcroot;
    public List<GameObject>  monsters = new List<GameObject>();
    Transform skillRoot;
    public List<GameObject> monsters2 = new List<GameObject>();
    public bool isYg = false;
    public GameObject npc;
    public List<GameObject> collects = new List<GameObject>();
    public GameObject MyMap;
    public PlayerObj player;
    public void Init(string sceneName)
    {
        this.sceneName = sceneName;
        npcroot = GameObject.Find("NpcRoot").transform;
        canvas = GameObject.Find("Canvas").transform;
        MyYg = GameObject.Instantiate(Resources.Load<GameObject>("Yg"), canvas, false);
        skillRoot = GameObject.Instantiate(Resources.Load<GameObject>("SkillRoot"), canvas, false).transform;

        MyMap = GameObject.Instantiate(Resources.Load<GameObject>("Map"), canvas, false);
        MyMap.AddComponent<WorldMap>();
        player_Info Info = new player_Info();
        Info.hp = 100;
        Info.modelPath = "Teddy";
        Info.name = "Teddy";
        Info.pos = new Vector3(0, 0, 0);
        Info.ros = new Vector3(0, 0, 0);
        Info.type = RoleType.Player;
        Info.skills = null;
        player = new PlayerObj(Info);
        //Collider[] colliders = Physics.OverlapSphere(player.obj.transform.position, 3);
        Yg yg = MyYg.transform.GetChild(0).GetComponent<Yg>();
        yg.GetPlayer(player.obj);
        //MyMap.GetComponent<WorldMap>().GetPlayer(player.obj);
        skillRoot.gameObject.AddComponent<PutSkill>().SetPlayer(player);
        CreateMonster();
        RwManager.Ins.Init(player);
        monsters2 = monsters;
    }
    public void CreateMonster()
    {
        models = UserData.Ins.GetMonsterDatas(sceneName);
        Obj_Info info;
        for (int i = 0; i < models.Count; i++)
        {
            info = new Obj_Info();
            info.name = models[i].name;
            info.pos = new Vector3(models[i].px, models[i].py, models[i].pz);
            info.ros = new Vector3(models[i].rx, models[i].ry, models[i].rz);
            info.type = models[i].type;
            info.modelPath=models[i].pathName;
            CreateObj(info);
        }
    }
    public void DesMonst(string name)
    {
        foreach (var item in monsters)
        {
            if (item.name==name)
            {
                monsters.Remove(item);
                return;
            }
        }
    }

    public void CreateObj(Obj_Info info)
    {
        ObjBase monster = null;
        if (info.type==RoleType.Monster)
        {
            monster = new MonsterObj(info);
            monster.obj.transform.SetParent(npcroot, false);
            monster.obj.AddComponent<Enemy>();
            monster.obj.tag = "monster";
            //MyMap.GetComponent<WorldMap>().GetPlayer(monster.obj);
            monsters.Add(monster.obj);
            
        }else if(info.type==RoleType.NPC)
        {
            monster = new NpcObj(info);
            npc = monster.obj;
        }else if(info.type==RoleType.Collect)
        {
            monster = new CollectObj(info);
            monster.obj.AddComponent<CollectComponent>().SetPlayer(player.obj);
            collects.Add(monster.obj);
        }
    }
}
