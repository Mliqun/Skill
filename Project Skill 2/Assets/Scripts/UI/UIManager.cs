using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{
    public Text title;
    public Text myContent;
    public GameObject tips;
    Button closeBtn;


   
    public void OpenTips(string name,string content)
    {
        
        if (tips==null)
        {
            tips =GameObject.Instantiate( Resources.Load<GameObject>("Tip"),GameObject.Find("Canvas").transform,false);
            title = tips.transform.GetChild(0).GetComponent<Text>();
            myContent = tips.transform.GetChild(1).GetComponent<Text>();
            closeBtn=tips.transform.GetChild(2).GetComponent<Button>();
            closeBtn.onClick.AddListener(() =>
            {
                tips.SetActive(false);
            });
        }
        else
        {
            tips.SetActive(true);
        }
        title.text = name;
        myContent.text = content;
    }
   

}
