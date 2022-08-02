using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectComponent : MonoBehaviour
{
    public GameObject player;
    float times=0;
    bool isGo = false;
    GameObject collectObj;
    public Button cjBtn;
    public Slider cjSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        collectObj = GameObject.Instantiate(Resources.Load<GameObject>("Collect"), GameObject.Find("Canvas").transform, false);
        cjBtn = collectObj.transform.GetChild(0).GetComponent<Button>();
        cjBtn.gameObject.SetActive(false);
        cjSlider = collectObj.transform.GetChild(1).GetComponent<Slider>();
        cjSlider.gameObject.SetActive(false);

        cjBtn.onClick.AddListener(() =>
        {
            cjSlider.value = 0;
            cjSlider.gameObject.SetActive(true);
            isGo = true;
        });
    }
    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,player.transform.position)<2)
        {
            cjBtn.gameObject.SetActive(true);
        }
        else
        {
            cjBtn.gameObject.SetActive(false);
        }
        if (isGo)
        {
            times += Time.deltaTime;
            cjSlider.value = times / 5f;
            if (times>=5)
            {
                times = 0;
                RwManager.Ins.num++;
                RwManager.Ins.RefreshTask();
                cjBtn.gameObject.SetActive(false);
                cjSlider.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
