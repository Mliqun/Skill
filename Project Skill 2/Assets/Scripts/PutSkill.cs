using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutSkill : MonoBehaviour
{
    List<Button> skillBtns = new List<Button>();
    PlayerObj player;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            skillBtns.Add(transform.GetChild(i).GetComponent<Button>());
        }
        for (int i = 0; i < skillBtns.Count; i++)
        {
            int index = i;
            skillBtns[index].onClick.AddListener(() =>
            {
                if (player != null)
                {
                    player.SetDh(skillBtns[index].name);
                }
            });
        }
        //foreach (var item in skillBtns)
        //{
            
        //    item.onClick.AddListener(() =>
        //    {
                
        //    });
        //}
    }
    public void SetPlayer(PlayerObj _player)
    {
        player = _player;
    }
   
    void Update()
    {
        
    }
}
