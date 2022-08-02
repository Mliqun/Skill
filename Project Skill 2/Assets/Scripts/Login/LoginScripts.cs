using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class Zh
{
    public string account;
    public string password;
}
public class LoginScripts : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;
    public InputField account;
    public InputField password;
    public Button regBtn;
    public Button loginBtn;
    List<Zh> zhs = new List<Zh>();
    public GameObject scene;
    bool isJr=false;
    // Start is called before the first frame update
    void Start()
    {
        regBtn.onClick.AddListener(() =>
        {
            registerPanel.SetActive(true);
            loginPanel.SetActive(false);
        });
        string path = "Assets/a.json";
        loginBtn.onClick.AddListener(() =>
        {
            if (account.text.Length>0&&password.text.Length>0)
            {
                string txt = File.ReadAllText(path);
                zhs = JsonConvert.DeserializeObject<List<Zh>>(txt);
                foreach (var item in zhs)
                {
                    if (item.account==account.text&&item.password==password.text)
                    {
                        scene.gameObject.SetActive(true);
                        loginPanel.SetActive(false);
                        isJr = true;
                    }
                }
            }
            if (isJr==false)
            {
                UIManager.Ins.OpenTips("¥ÌŒÛÃ· æ", "√‹¬Î¥ÌŒÛ");
            }
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
