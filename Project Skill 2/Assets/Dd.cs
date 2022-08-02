using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dd : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.DOShakePosition(1, new Vector3(0.5f, 0.5f, 0).normalized);
        }
        Camera.main.transform.LookAt(player.transform);
        Camera.main.transform.position= Vector3.Lerp(Camera.main.transform.position, player.transform.position+new Vector3(0,6,-5), Time.deltaTime * 3);
        player.transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime *Vector3.forward*5);
        player.transform.Rotate(Input.GetAxis("Horizontal") * Time.deltaTime *Vector3.up* 100f);
    }
}
