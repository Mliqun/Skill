using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PlayerEditor
{
    public int _characterIndex = 0;
    public int _folderIndex = 0;
    public string characterName = string.Empty;
    public string folderName = string.Empty;
    public string characterFilter = string.Empty;
    public List<string> characteList = new List<string>();
    public Player player=null;
}
public class SkillEditorWindow : EditorWindow
{
    PlayerEditor m_player = new PlayerEditor();
    //文件名
    List<string> m_folderList = new List<string>();
    //所有预制体名
    List<string> m_characterList = new List<string>();
    //按文件名存储 预制体
    Dictionary<string, List<string>> m_folderPredabs = new Dictionary<string, List<string>>();
    //技能详情窗口
    SkillWindow skillWindow;
    //储存 创建新的技能的名字

    string newSkillName = string.Empty;

    [MenuItem("Tools/技能编辑器")]
    public static void Init()
    {
        if (Application.isPlaying)
        {
            SkillEditorWindow window = EditorWindow.GetWindow<SkillEditorWindow>("SkillEditor");
            if (window!=null)
            {
                window.Show();
            }
        }
    }
    
    private void OnEnable()
    {
        DoSearchFolder();
        DoSearchCharacter();
    }
    void DoSearchFolder()
    {
        m_folderList.Clear();
        m_folderList.Add("all");
        string[] folders = Directory.GetDirectories(GetCharacterPath());
        foreach (var item in folders)
        {
            m_folderList.Add(Path.GetFileName(item));
        }
    }
    void DoSearchCharacter()
    {
        string[] files = Directory.GetFiles(GetCharacterPath(), "*.prefab", SearchOption.AllDirectories);
        m_characterList.Clear();
        foreach (var item in files)
        {
            m_characterList.Add(Path.GetFileNameWithoutExtension(item));
        }
        m_characterList.Sort();
        m_characterList.Insert(0, "null");
        m_player.characteList.AddRange(m_characterList);
    }
    string GetCharacterPath()
    {
        return Application.dataPath + "/MyGameData/Model";
    }
    int folderIndex;
    Vector2 ScrollViewPos = new Vector2(0, 0);
    private void OnGUI()
    {
        folderIndex = EditorGUILayout.Popup(m_player._folderIndex, m_folderList.ToArray());
        if (folderIndex!=m_player._folderIndex)
        {
            m_player._folderIndex = folderIndex;
            m_player._characterIndex = -1;
            string folderName = m_folderList[m_player._folderIndex];
            List<string> list;
            if (folderName.Equals("all"))
            {
                list = m_characterList;
            }
            else
            {
                if (!m_folderPredabs.TryGetValue(folderName,out list))
                {
                    list = new List<string>();
                    string[] files = Directory.GetFiles(GetCharacterPath() + "/" + folderName, "*.prefab", SearchOption.AllDirectories);
                    foreach (var item in files)
                    {
                        list.Add(Path.GetFileNameWithoutExtension(item));
                    }
                    m_folderPredabs[folderName] = list;
                }
            }
            m_player.characteList.Clear();
            m_player.characteList.AddRange(list);
        }
        int characterIndex = EditorGUILayout.Popup(m_player._characterIndex, m_player.characteList.ToArray());
        if (characterIndex != m_player._characterIndex)
        {
            m_player._characterIndex = characterIndex;
            if (m_player.characterName!=m_player.characteList[m_player._characterIndex])
            {
                m_player.characterName = m_player.characteList[m_player._characterIndex];
                if (!string.IsNullOrEmpty(m_player.characterName))
                {
                    if (m_player.player!=null)
                    {
                        m_player.player.Destroy();
                    }
                    m_player.player = Player.Init(m_player.characterName);
                    
                }
            }
        }

        newSkillName = GUILayout.TextField(newSkillName);
        if (GUILayout.Button("创建新的技能"))
        {
            if (!string.IsNullOrEmpty(newSkillName) && m_player.player != null)
            {
                List<SkillBase> skills = m_player.player.AddNewSkill(newSkillName);
                OpenSkillWindow(newSkillName, skills);
                newSkillName = "";
            }
        }
        if (m_player.player != null)
        {
            ScrollViewPos = GUILayout.BeginScrollView(ScrollViewPos, false, true);
            foreach (var item in m_player.player.skillsList)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(item.Key))
                {
                    List<SkillBase> skillComponents = m_player.player.GetSkill(item.Key);
                    foreach (var ite in skillComponents)
                    {
                        ite.Init();
                    }
                    OpenSkillWindow(item.Key, skillComponents);
                }

                GUILayoutOption[] option = new GUILayoutOption[] {
                GUILayout.Width(60),
                GUILayout.Height(19)
                };

                if (GUILayout.Button("删除技能", option))
                {
                    m_player.player.RevSkill(item.Key);
                    break;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
        if (GUILayout.Button("保存数据"))
        {
           // m_player.player.SaveData();
        }

    }
    void OpenSkillWindow(string newSkillName,List<SkillBase> skillComponents)
    {
        if (skillComponents!=null)
        {
            if (skillWindow==null)
            {
                skillWindow = EditorWindow.GetWindow<SkillWindow>("");
                //Debug.Log(newSkillName);
            }
            skillWindow.titleContent = new GUIContent(newSkillName);
            skillWindow.SetInitSkill(skillComponents, m_player.player);
            skillWindow.Show();
            skillWindow.Repaint();
        }
    }
    
}
