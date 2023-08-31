using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSystem : MonoBehaviour
{
    GameObject hero;
    // Start is called before the first frame update
    void Awake(){
        hero = GameObject.Find("Hero");
        if(hero == null){
            Debug.Log("Hero not found");
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SummonMonster(int id){
        int num = BookSystem.GetCardNum(id);
        if(num == -1){
            Debug.Log("Monster not found");
            return;
        }
        GameObject monster = Instantiate(BookSystem.GetInformation(id));
        monster.tag = "positive";
        monster.GetComponent<MonsterInformation>().SetSummon();
    }
}
