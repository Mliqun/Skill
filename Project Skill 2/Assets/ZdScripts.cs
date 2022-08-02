using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZdScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnDestroy", 5f);
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag=="monster")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
