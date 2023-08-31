using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInformation : MonoBehaviour
{
    private string targetTag;
    private bool forward_direction;
    private CreatureInformation creatureInformation;
    private ElementInformation elementInformation;
    //public Component AISystem;
    public float maxLife = 10f;
    public bool SummonMonster = false;
    public float maxLifeTime = 15f;
    public float maxLifeTimeCount = 0f;
    public GameObject triangle;
    private int id;
    public enum function{
        Attack,
        Defend,
        Heal
    }
    public int functionMode = -1;
    public int numForSummon;
    public int DropNum;
    // Start is called before the first frame update
    void Awake(){
        creatureInformation = gameObject.GetComponent<CreatureInformation>();
        if(creatureInformation == null){
            Debug.Log("Without CreatureInformation");
            Destroy(gameObject);
        }
        elementInformation = gameObject.GetComponent<ElementInformation>();
        if(elementInformation == null){
            Debug.Log("Without ElementInformation");
            Destroy(gameObject);
        }
        // if(AISystem == null){
        //     Debug.Log("Without AISystem");
        //     Destroy(gameObject);
        // }
        creatureInformation.SetMaxHeartPoint(maxLife);
        id = creatureInformation.mcreatureId;
        if(functionMode == -1){
            functionMode = (int)function.Attack;
        }
        triangle = Resources.Load("Prefabs/Objects/green_triangle") as GameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        SummonLive();
        Die();
    }
    private void Die(){
        if(creatureInformation.Get_Live() == false){
            if(!SummonMonster){
                BookSystem.GetMonsterCard(id, DropNum);
            }
            Debug.Log("Monster Card");
            Destroy(gameObject);//这里可以播放死亡动画
        }
    }

    private void SummonLive(){
        if(SummonMonster){
            maxLifeTimeCount += Time.deltaTime;
        }
        if(maxLifeTimeCount > maxLifeTime && SummonMonster){
            creatureInformation.SetAttackHurt(maxLife * 2);
            Destroy(gameObject);
            //直接给予2倍伤害，让creatureInformation来设置死亡
        }
    }
    public void SetSummon(){
        SummonMonster = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public int GetFunctionMode(){
        return functionMode;
    }
}
