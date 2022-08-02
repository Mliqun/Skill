using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class yg2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject kk;
    public Vector2 startPos, OnPos;
    public bool IsGo;
    float len = 30;
    private void Awake()
    {
        kk.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        IsGo = true;
        startPos = transform.position;
        kk.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnPos = eventData.position - startPos;
        transform.position = Vector2.ClampMagnitude(OnPos, len) + startPos;
        float angle;
        if (OnPos.x < 0)
        {
            angle = -Vector2.Angle(Vector2.up, OnPos);
        }
        else
        {
            angle = Vector2.Angle(Vector2.up, OnPos);
        }
        kk.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        OnPos = Vector2.zero;
        IsGo = false;
        kk.SetActive(false);
        gameObject.SetActive(false);
    }
    private void Update()
    {

    }
}
