using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RwManager : SingleTon<RwManager>
{
    public bool isDg = false;
    public bool isRw = false;
    public bool isJq = true;
    public bool isCj = false;

    Text dialogContnet;
    GameObject dialog;
    Text npcName;

    List<Taskk> taskks = new List<Taskk>();
    string[] dialogNr;
    Taskk currTask = new Taskk();
    public int num;
    public GameObject rw;
    Button rwBtn;
    Text rwName;
    Text rwNr;
    public Vector3 mdd;
    int index = 0;
    public void Init(PlayerObj player)
    {
        taskks = UserData.Ins.GetTask();
        Jq();
        mdd = new Vector3(currTask.x, currTask.y, currTask.z);
        rw = GameObject.Instantiate(Resources.Load<GameObject>("rw"), GameObject.Find("Canvas").transform, false);
        rwBtn = rw.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
        rwBtn.onClick.AddListener(() =>
        {
            if (!currTask.isFinish)
            {
                if (isJq)
                {
                    player.SetMdd(mdd);
                }
                else
                {
                    player.SetMdd(Vector3.zero);
                }
            }
            else
            {
                player.SetMdd(mdd);
            }
        });
        rwName = rwBtn.transform.GetChild(0).GetComponent<Text>();
        rwNr = rwBtn.transform.GetChild(1).GetComponent<Text>();
    }
    public void Combat()
    {
        
    }
    public void Gather()
    {

    }
    public void Jq()
    {
        foreach (var item in taskks)
        {
            if (item.isFinish==false)
            {
                Debug.Log(item.taskName);
                dialogNr = item.dialog.Split('|');
                currTask = item;
                isJq = true;
                if (item.type==TaskType.Dg)
                {
                    isDg = true;
                }else if (item.type==TaskType.Cj)
                {
                     isCj= false;
                }
                break;
            }
        }
    }
    public void OpenNpcDialog(string name, string content)
    {
        if (dialog == null)
        {
            dialog = GameObject.Instantiate(Resources.Load<GameObject>("Dialog"), GameObject.Find("Canvas").transform, false);
            npcName = dialog.transform.GetChild(0).GetComponent<Text>();
            dialogContnet = dialog.transform.GetChild(1).GetComponent<Text>();
            Button cBtn = dialog.GetComponent<Button>();
            cBtn.onClick.AddListener(() =>
            {
                if (!currTask.isFinish)
                {
                    RefreshDialog();
                }
                else
                {
                    dialog.gameObject.SetActive(false);
                }
               
            });
        }
        else
        {
            dialog.SetActive(true);
        }
        npcName.text = name;
        dialogContnet.text = content;
    }
    public void RefreshTask()
    {
        if (num>= currTask.num)
        {
            rwNr.text = "已完成任务";
            FinishTask();
        }
        else
        {
            rwName.text = currTask.taskName;
            rwNr.text = num + "/" + currTask.num;
        }
        isJq = false;
    }
    public void FinishTask()
    {
        currTask.isFinish = true;
        foreach (var item in taskks)
        {
            if (item==currTask)
            {
                item.isFinish = true;
                Debug.Log(item.taskName);
            }
        }
    }
    public void RefreshDialog()
    {
        if (currTask.isFinish)
        {
            FinishDialog();
            return;
        }
        if (index==dialogNr.Length)
        {
            dialog.gameObject.SetActive(false);
            index = 0;
            RefreshTask();
            return;
        }
        OpenNpcDialog(currTask.npcName, dialogNr[index]);
        index++;
    }
    public void FinishDialog()
    {
        dialogContnet.text = "恭喜你完成任务";
        dialog.gameObject.SetActive(true);
        isJq = true;
        num = 0;
        Jq();
    }

}
