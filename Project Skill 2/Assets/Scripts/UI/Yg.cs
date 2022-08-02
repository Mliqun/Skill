using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Yg : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public Vector2 startPos, OnPos;
    public bool IsGo;
    float len=30;
    bool isJr = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        IsGo = true;
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnPos = eventData.position - startPos;
        transform.position = Vector2.ClampMagnitude(OnPos, len) + startPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        IsGo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject player;
    public void GetPlayer(GameObject play)
    {
        player = play;
    }
    // Update is called once per frame
    void Update()
    {
        if (player!=null)
        {
            if (IsGo)
            {
                player.transform.LookAt(new Vector3(OnPos.x,0,OnPos.y));
                player.transform.Translate(Vector3.forward * Time.deltaTime*5);
                player.transform.GetComponent<Animator>().SetBool("run", true);
                World.Ins.isYg = true;
                player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            }
            else
            {
                player.transform.GetComponent<Animator>().SetBool("run", false);
                player.GetComponent<NavMeshAgent>().isStopped = false;
                World.Ins.isYg = false;
            }
        }
        if (Vector3.Distance(player.transform.position,World.Ins.npc.transform.position)<2)
        {
            if (isJr)
            {
                RwManager.Ins.RefreshDialog();
                isJr = false;
            }
        }
        else
        {
            isJr = true;
        }
    }
}
