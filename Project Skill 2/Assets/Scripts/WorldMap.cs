using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    RectTransform map;
    RectTransform playerMap;
    RectTransform monsterMap;
    RectTransform monster2Map;
    public GameObject player;
    List<GameObject> objs = new List<GameObject>();
    private void Awake()
    {
        map = transform.GetComponent<RectTransform>();
        playerMap = transform.GetChild(0).Find("PlayerMap").GetComponent<RectTransform>();
        monsterMap = transform.GetChild(0).Find("MonsterMap").GetComponent<RectTransform>();
        monster2Map = transform.GetChild(0).Find("Monster2Map").GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    public void GetPlayer(GameObject _player)
    {
        player = _player;
        objs.Add(player);
    }
    public void Rem(GameObject _player)
    {
        foreach (var item in objs)
        {
            if (item==_player)
            {
                objs.Remove(item);
                switch (item.name)
                {
                    case "Teddy": playerMap.gameObject.SetActive(false); break;
                    case "Cube": monsterMap.gameObject.SetActive(false); break;
                    case "Sphere":monster2Map.gameObject.SetActive(false); break;
                }
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (var item in objs)
        {
            RectTransform rect=new RectTransform();
            switch(item.name)
            {
                case "Teddy":rect=playerMap;break;
                case "Cube":rect = monsterMap; break;
                case "Sphere": rect = monster2Map; break;
            }
            rect.anchoredPosition = new Vector2(item.transform.position.x / 30 * map.rect.width, item.transform.position.z / 30 * map.rect.height);
            rect.eulerAngles = new Vector3(0, 0, item.transform.eulerAngles.y);
        }
    }
}
