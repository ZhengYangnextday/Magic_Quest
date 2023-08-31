using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInformation : MonoBehaviour
{   
    public enum ElementIndex{
        e_normal,
        e_fire,
        e_water,
        e_wood,
        e_time,
    }
    //元素种类，普通元素 - e_normal - 0, 火 - e_fire - 1, 水 - e_water - 2, 木 - e_wood - 3，时 - e_time - 5
    public enum RankIndex{
        D,
        C,
        B,
        A,
        S,
    }
    //阶级， D - 0, C - 1, B - 2, A - 3, S - 4
    private Dictionary<string, int> Function;
    //特殊效果，目前暂无
    private bool alive = true;
    //表示该元素绑定的物体是否存在，由于生物、物体不同的交互逻辑不同，所以不直接销毁
    public int rank = 0;
    public int type = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (type == 0){
            type = (int)ElementIndex.e_normal;
        }
        if (rank == 0){
            rank = (int)RankIndex.D;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetElementType(){
        return type;
    }
    public int GetElementRank(){
        return rank;
    }
    public string SetElementRank(int rank_to_set){
        if(rank_to_set < 0 || rank_to_set > 5){
            return "Wrong Type Number";
        }
        switch(rank_to_set){
            case 0:
                return "D";
            case 1:
                return "C";
            case 2:
                return "B";
            case 3:
                return "A";
            case 4:
                return "S";
            default:
                return "Wrong Type Number";
        }
    }
    public string SetElementType(int type_to_set){
        if(type_to_set < 0 || type_to_set > 4){
            return "Wrong Type Number";
        }
        switch(type_to_set){
            case 0:
            type = type_to_set;
                return "Normal";
            case 1:
            type = type_to_set;
                return "Fire";
            case 2:
            type = type_to_set;
                return "Water";
            case 3:
            type = type_to_set;
                return "Wood";
            case 4:
            type = type_to_set;
                return "time";
            default:
                return "Wrong Rank Number";
        }
    }

    public void Rankup(){
        rank++;
        if(rank > (int)RankIndex.S){
            rank = (int)RankIndex.S;
        }
    }
    public void SetAlive(bool alive_to_set){
        alive = alive_to_set;
    }
    public bool GetAlive(){
        return alive;
    }
}
