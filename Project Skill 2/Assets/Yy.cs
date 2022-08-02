using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Yy : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    GameObject zx;
    yg2 yg;
    bool isAx = false;

    private void Awake()
    {
        yg = transform.GetChild(0).GetComponent<yg2>();
        Debug.Log(yg.IsGo);
        zx = transform.GetChild(0).gameObject;
        zx.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        zx.SetActive(true);
        isAx = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!yg.IsGo)
        {
            zx.SetActive(false);
        }
    }


    private void Update()
    {
       
    }

   
}
