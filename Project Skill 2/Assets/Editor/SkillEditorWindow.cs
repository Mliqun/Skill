using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class PlayerEditor
{
    public string folderName=string.Empty;
    public string characterName = string.Empty;
    public int characterIndex=0;
    public int folderIndex=0;
    public List<string> chararList = new List<string>();
    public Player player = null;
}
public class SkillEditorWindow : EditorWindow
{
    PlayerEditor m_player = new PlayerEditor();
    List<string> folderList = new List<string>();
    List<string> characterList = new List<string>();
    string newSkillName;
    Dictionary<string, List<string>> characterPrefabs = new Dictionary<string, List<string>>();
    Vector2 scrllViewPos = new Vector2(0, 0);
    SkillWindow skillWindow;
    private void OnEnable()
    {
        SearchFolder();
        SearchCharacter();
    }
    public void SearchFolder()
    {
        folderList.Clear();
        folderList.Add("all");
        string[] files = Directory.GetDirectories(GetPath());
        foreach (var item in files)
        {
            folderList.Add(Path.GetFileName(item));
        }
    }
    public void SearchCharacter()
    {
        characterList.Clear();
        string[] files = Directory.GetFiles(GetPath(), "*.prefab", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            characterList.Add(Path.GetFileNameWithoutExtension(item));
        }
        characterList.Sort();
        characterList.Insert(0, "null");
        m_player.chararList.AddRange(characterList);
    }
    public string GetPath()
    {
        return Application.dataPath + "/MyGameData/Model";
    }
    [MenuItem("Tools/技能编辑器")]
    public static void Init()
    {
        SkillEditorWindow window = EditorWindow.GetWindow<SkillEditorWindow>("SkillEditorWindow");
        if (window!=null)
        {
            window.Show();
        }
    }
    private void OnGUI()
    {
        int folderindex = EditorGUILayout.Popup(m_player.folderIndex, folderList.ToArray());
        if (folderindex!=m_player.folderIndex)
        {
            m_player.folderIndex = folderindex;
            m_player.characterIndex = -1;
            string folderName = folderList[m_player.folderIndex];
            List<string> list;
            if (folderName=="all")
            {
                list = characterList;
            }
            else
            {
                if (!characterPrefabs.TryGetValue(folderName,out list))
                {
                    list = new List<string>();
                    string[] files = Directory.GetFiles(GetPath() + "/" + folderName, "*.prefab", SearchOption.AllDirectories);
                    foreach (var item in files)
                    {
                        list.Add(Path.GetFileNameWithoutExtension(item));
                    }
                    characterPrefabs[folderName] = list;
                }
            }
            m_player.chararList.Clear();
            m_player.chararList.AddRange(list);
        }
        int characterIndex = EditorGUILayout.Popup(m_player.characterIndex, m_player.chararList.ToArray());
        if (characterIndex!=m_player.characterIndex)
        {
            m_player.characterIndex = characterIndex;
            if (m_player.characterName!=m_player.chararList[m_player.characterIndex])
            {
                m_player.characterName = m_player.chararList[m_player.characterIndex];
                if (!string.IsNullOrEmpty(m_player.characterName))
                {
                    if (m_player.player!=null)
                    {
                        m_player.player.OnDestroy();
                    }
                    m_player.player = Player.Init(m_player.characterName);
                }
            }
        }
        GUILayout.BeginHorizontal();
        newSkillName = GUILayout.TextField(newSkillName);
        if (GUILayout.Button("创建新技能"))
        {
            if (!string.IsNullOrEmpty(newSkillName)&&m_player.player!=null)
            {
                List<SkillBase> skills = m_player.player.AddSkill(newSkillName);
                OpenSkillWindow(skills, newSkillName);
                newSkillName = "";
            }
        }
        GUILayout.EndHorizontal();
        if (m_player.player!=null)
        {
            scrllViewPos= GUILayout.BeginScrollView(scrllViewPos, false, true);
            foreach (var item in m_player.player.skillslist)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(item.Key))
                {
                    List<SkillBase> skills = m_player.player.GetSkill(item.Key);
                    foreach (var ite in skills)
                    {
                        ite.Init();
                    }
                    OpenSkillWindow(skills, item.Key);
                }
                GUILayoutOption[] options = new GUILayoutOption[]
                {
                    GUILayout.Width(60),
                    GUILayout.Height(19)
                };
                if (GUILayout.Button("删除",options))
                {
                    m_player.player.RevSkill(item.Key);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            if (GUILayout.Button("保存数据"))
            {
                m_player.player.SaveData();
            }
            
        }

    }
    public void OpenSkillWindow(List<SkillBase> skills,string skillName)
    {
        if (skills!=null)
        {
            if (skillWindow==null)
            {
                skillWindow = EditorWindow.GetWindow<SkillWindow>("");
            }
            skillWindow.titleContent = new GUIContent(skillName);
            skillWindow.Init(skills, m_player.player);
            skillWindow.Show();
            skillWindow.Repaint();
        }
    }
}
