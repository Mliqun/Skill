using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using UnityEditor;

public class RegisterScripts : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;
    public InputField account;
    public InputField password;
    public Button backBtn;
    public Button registerBtn;
    List<Zh> zhs = new List<Zh>();
    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            loginPanel.SetActive(true);
            registerPanel.SetActive(false);
            
        });
        string path = "Assets/a.json";
        registerBtn.onClick.AddListener(() =>
        {
            if (account.text.Length > 0 && password.text.Length > 0)
            {
                Zh zh = new Zh();
                zh.account = account.text;
                zh.password = password.text;

                string txt = File.ReadAllText(path);
                zhs = JsonConvert.DeserializeObject<List<Zh>>(txt);
                zhs.Add(zh);
                string json = JsonConvert.SerializeObject(zhs);
                File.WriteAllText(path, json);
                Debug.Log("“—–¥»Î");
                AssetDatabase.Refresh();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
