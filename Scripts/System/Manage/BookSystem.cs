using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private static int[] CardNum;
    private static BookSystem book;
    private static bool[] IsCardGet;
    private static int CardIndexNum;
    public List<GameObject> MonsterIndex;
    private static bool summonMode = false;
    public static GameObject[] MonsterIndexQuest;
    public static Vector3Int CurrentEquipCard;
    private static int HeartNum;
    public static int CurrentEquip = 0;
    //0 - 左，1 - 中， 2 - 右
    void Awake(){
        for(int i = 0; i < 3;i++){
            CurrentEquipCard[i] = -1;
        }
        book = this;
        SetCardIndexNum(MonsterIndex.Count);
        MonsterIndexCheck();
        HeartNum = 0;
    }
    void Start()
    {
        // book = this;
        // SetCardIndexNum(MonsterIndex.Count);
        // MonsterIndexCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetCardIndexNum(int num){
        CardNum = new int[num];
        IsCardGet = new bool[num];
        MonsterIndexQuest = new GameObject[num];
        CardIndexNum = num;
        for(int i = 0; i < num; i++){
            CardNum[i] = 10;
            IsCardGet[i] = false;
        }
        // Debug.Log(num);
    }
    public static bool GetSummonMode(){
        return summonMode;
    }
    public static void SetSummonMode(bool hero_summon_mode){
        summonMode = hero_summon_mode;
    }
    private void MonsterIndexCheck(){
        bool[] hasCheckedId = new bool[MonsterIndex.Count];
        CreatureInformation checkCreatureInformation;
        for(int i = 0; i < MonsterIndex.Count; i++){
            GameObject CheckMonsterCard = Instantiate(MonsterIndex[i]);
            checkCreatureInformation = CheckMonsterCard.GetComponent<CreatureInformation>();
            if(checkCreatureInformation == null || MonsterGameObjectValid(CheckMonsterCard) == false){
                Debug.Log(i);
                Debug.Log("Wrong Monster");
                return;
            }
            if(!hasCheckedId[checkCreatureInformation.mcreatureId]){
                hasCheckedId[checkCreatureInformation.mcreatureId] = true;
                MonsterIndexQuest[checkCreatureInformation.mcreatureId] = MonsterIndex[i];
            }//主角编号为0，剩余怪物依次编号，主角死亡时获取主角的Card
            else{
                Debug.Log("Two Creature with the same Id");
                return;
            }
            Destroy(CheckMonsterCard);
        }
    }
    public static GameObject GetInformation(int id){
        if(!IdInRange(id)){
            Debug.Log("Wrong Card Index");
            return null;
        }
        return MonsterIndexQuest[id];
    }
    public static int GetCardType(int id){
        GameObject Monster = Instantiate(MonsterIndexQuest[id]);
        int quest_type = Monster.GetComponent<ElementInformation>().GetElementType();
        Destroy(Monster);
        return quest_type;
    }
    public static int GetCardRank(int id){
        GameObject Monster = Instantiate(MonsterIndexQuest[id]);
        int quest_rank = Monster.GetComponent<ElementInformation>().GetElementRank();
        Destroy(Monster);
        return quest_rank;
    }
    public static Sprite GetCardSprite(int id){
        GameObject Monster = Instantiate(MonsterIndexQuest[id]);
        Sprite quest_sprite = Monster.GetComponent<SpriteRenderer>().sprite;
        Destroy(Monster);
        return quest_sprite;
    }
    public static int GetCardIndexNum(){
        return CardIndexNum;
    }
    public static int GetCardNum(int id){
        if(!IdInRange(id)){
            Debug.Log("Wrong Card Index");
            return -1;
        }else{
            return CardNum[id];
        }
    }
    public static int GetCardFunction(int id){
        GameObject Monster = Instantiate(MonsterIndexQuest[id]);
        int quest_func = Monster.GetComponent<MonsterInformation>().GetFunctionMode();
        Destroy(Monster);
        return quest_func;
    }
    //0-代表攻击，1代表防御，2代表治疗
    public static int GetCardSummonNum(int id){
        GameObject Monster = Instantiate(MonsterIndexQuest[id]);
        int quest_summon = Monster.GetComponent<MonsterInformation>().numForSummon;
        Destroy(Monster);
        return quest_summon;
    }
    public static int GetCardDropNum(int id){
        GameObject Monster = Instantiate(MonsterIndexQuest[id]);
        int quest_drop = Monster.GetComponent<MonsterInformation>().DropNum;
        Destroy(Monster);
        return quest_drop;
    }
    public static bool UseCard(int id, int used_num){
        if(!IdInRange(id)){
            Debug.Log("Wrong Card Index");
            return false;
        }else{
            if(used_num <= GetCardNum(id)){
                CardNum[id] -= used_num;
                return true;
            }else{
                return false;
            }
        }
    }
    public static void SetEquip(int EquipNum, int id){
        CurrentEquipCard[EquipNum] = id;
    }
    public static void SetEquipNum(int EquipNum){
        CurrentEquip = EquipNum;
    }
    public static int GetEquipNum(){
        return CurrentEquip;
    }
    public static int GetEquip(int EquipNum){
        return CurrentEquipCard[EquipNum];
    }
    public static int GetNowEquip(){
        return CurrentEquipCard[CurrentEquip];
    }
    public static bool IdInRange(int id){
        if(id >= CardIndexNum || id < 0){
            // Debug.Log("Wrong Card Index");
            return false;
        }else{
            return true;
        }
    }
    private static bool MonsterGameObjectValid(GameObject MonsterCard){
        if((MonsterCard.GetComponent<MonsterInformation>() == null) || (MonsterCard.GetComponent<ElementInformation>() == null)){
            return false;
        }else{
            return true;
        }
    }
    public static void GetMonsterCard(int id, int num){
        CardNum[id] += num;
    }
    public static void SetCheatMode(){
        for(int i = 0; i < CardIndexNum; i++){
            CardNum[i] = 99;
        }
    }
    public static void HeroHeartNumUp(){
        HeartNum++;
    }
    public static int GetHeroHeart(){
        return HeartNum;
    }
}
