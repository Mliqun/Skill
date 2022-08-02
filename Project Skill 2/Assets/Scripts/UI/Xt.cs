using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Xt : MonoBehaviour
{
    public GameObject xt;
    public float maxX = 100;
    public Text xtname;
    public Slider hp;
    public Transform fu;
    // Start is called before the first frame update
    private void Awake()
    {
        fu = GameObject.Find("Canvas/hp").transform;
        xt = Instantiate(Resources.Load<GameObject>("hp"), fu, false);
        xtname = xt.transform.GetChild(0).GetComponent<Text>();
        hp = xt.transform.GetChild(1).GetComponent<Slider>();
    }
    public void Init(float max,string name)
    {
        xtname.text = name;
        maxX = max;
    }
    public void Kx(float x)
    {
        maxX -= x;
        if (maxX <= 0)
        {
            if (RwManager.Ins.isDg)
            {
                RwManager.Ins.num++;
                RwManager.Ins.RefreshTask();
            }
            Destroy(xt.gameObject);
            Destroy(gameObject);
            World.Ins.MyMap.GetComponent<WorldMap>().Rem(gameObject);
            World.Ins.DesMonst(gameObject.name);
        }
    }
    public void Jx(float x)
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (xt != null)
        {
            xt.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up*2);
            hp.value = maxX / 100f;
        }
        
    }
}
