using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoleType
{
    nullType = -1,
    Player,
    Monster,
    NPC,
    Collect,
    BOSS
}
public class Obj_Info 
{
    public string name;
    public string modelPath;
    public Vector3 pos;
    public Vector3 ros;
    public RoleType type;
}
public class player_Info:Obj_Info
{
    public float hp;
    public List<SkillBase> skills;
}
public class Monster_Info:Obj_Info
{
    public float hp;
}
public class Npc_Info:Obj_Info
{
    
}
public class Collect_Info:Obj_Info
{

}