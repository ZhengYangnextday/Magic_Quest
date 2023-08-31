using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteractive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int Interactive(GameObject A, GameObject B){
        if(A.tag == B.tag){
            return -1;
        }//tag包含positive, negative, neural以及map
        if(!A.GetComponent<ElementInformation>() || !B.GetComponent<ElementInformation>()){
            return -1;
        }//仅带元素组件的物体可相互交互,避免和地图瓦片交互
        else{
            int state = 0;
            state = 1 << A.GetComponent<ElementInformation>().GetElementType() | 1 << B.GetComponent<ElementInformation>().GetElementType();
            Debug.Log(state);
            if(TypeMerge(state) == -1){
                Debug.Log("Wrong state");
                return -1;
            }
            if(A.GetComponent<MagicInformation>() && B.GetComponent<MagicInformation>()){
                MagicMerge(state, A, B);
                return 0;
            }
            if(A.GetComponent<CreatureInformation>() && B.GetComponent<CreatureInformation>()){
                
                Vector2 Attack_Calculate_Result = AttackCalculate(state, A, B);

                if(A.GetComponent<Hero>()){
                    Attack_Calculate_Result[1] = 0;
                }else if(B.GetComponent<Hero>()){
                    Attack_Calculate_Result[0] = 0;
                }
                //敌方单位和英雄撞在一起，只有英雄收到伤害,这里可能还要调整
                
                A.GetComponent<CreatureInformation>().SetAttackHurt(Attack_Calculate_Result[0]);
                B.GetComponent<CreatureInformation>().SetAttackHurt(Attack_Calculate_Result[1]);
                return 0;
            }
            if(A.GetComponent<CreatureInformation>() && B.GetComponent<MagicInformation>()){
                Vector2 Attack_Calculate_Result = AttackCalculate(state, A, B);
                A.GetComponent<CreatureInformation>().SetAttackHurt(Attack_Calculate_Result[0]);
                B.GetComponent<MagicInformation>().SetAlive(false);
                return 0;
            }
            if(B.GetComponent<CreatureInformation>() && A.GetComponent<MagicInformation>()){
                Vector2 Attack_Calculate_Result = AttackCalculate(state, A, B);
                B.GetComponent<CreatureInformation>().SetAttackHurt(Attack_Calculate_Result[1]);
                A.GetComponent<MagicInformation>().SetAlive(false);
                return 0;
            }

            Debug.Log("Interactive Wrong");
            return -1;
        }
    }// 除地图外任何元素碰撞时触发此判定, 不直接销毁，交由物体自己判断

    private static int TypeMerge(int state){
        switch(state){
            case 0b00001:
            case 0b00010:
            case 0b00100:
            case 0b01000:
            case 0b10000:
            case 0b00011:
            case 0b00101:
            case 0b01001:
            case 0b10001:
            case 0b00110:
            case 0b01010:
            case 0b10010:
            case 0b01100:
            case 0b10100:
            case 0b11000:
                return 0;
        }
        return -1;
    }

    private static int MagicMerge(int state, GameObject A, GameObject B){
        ElementInformation element_A = A.GetComponent<ElementInformation>();
        ElementInformation element_B = B.GetComponent<ElementInformation>();
        MagicInformation magic_A = A.GetComponent<MagicInformation>();
        MagicInformation magic_B = B.GetComponent<MagicInformation>();
        switch(state){
            case 0b00001:
            case 0b00010:
            case 0b00100:
            case 0b01000:
            case 0b10000:
            //元素相同，依靠刚体计算
            case 0b00011:
            case 0b00101:
            case 0b01001:
            case 0b10001:
                return 0;
            //近战攻击系统，可以弹反，不在这里过多设计
            case 0b00110:
                element_A.SetAlive(false);
                element_B.SetAlive(false);
                magic_A.SetAlive(false);
                magic_B.SetAlive(false);
                return 0;
            //水火相互抵消
            case 0b01010:
                if(element_A.GetElementType() == (int)ElementInformation.ElementIndex.e_fire){
                    element_B.SetAlive(false);
                    magic_B.SetAlive(false);
                    Destroy(B);
                    element_A.Rankup();
                    MagicCreate.CreateAttackMagic(element_A.rank, element_A.type, magic_A.setDirection, A.transform.position, A.tag);
                    Destroy(A);
                    // magic_A.Set_Avoid();
                }else{
                    element_A.SetAlive(false);
                    magic_A.SetAlive(false);
                    Destroy(A);
                    element_B.Rankup();
                    //magic_B.Init();
                    MagicCreate.CreateAttackMagic(element_B.rank, element_B.type, magic_B.setDirection, B.transform.position, B.tag);
                    Destroy(B);
                    // magic_B.Set_Avoid();
                }
            //火把木烧毁
                return 0;
            case 0b01100:
                if(element_A.GetElementType() == (int)ElementInformation.ElementIndex.e_wood){
                    element_B.SetAlive(false);
                    magic_B.SetAlive(false);
                    Destroy(B);
                    element_A.Rankup();
                    //magic_A.Init();
                    MagicCreate.CreateAttackMagic(element_A.rank, element_A.type, magic_A.setDirection, A.transform.position, A.tag);
                    Destroy(A);
                    // magic_A.Set_Avoid();
                }else{
                    element_A.SetAlive(false);
                    magic_A.SetAlive(false);
                    Destroy(A);
                    element_B.Rankup();
                    //magic_B.Init();
                    MagicCreate.CreateAttackMagic(element_B.rank, element_B.type, magic_B.setDirection, B.transform.position, B.tag);
                    Destroy(B);
                    // magic_B.Set_Avoid();
                }
            //木将水吸收
                return 0;
            //发生元素反应的魔法
            case 0b10010:
            case 0b10100:
            case 0b11000:
                return 0;
            //时魔法攻击，设计不完全，待系统完善后设计
        }
        return -1;
    }
    private static Vector2 AttackCalculate(int state, GameObject A, GameObject B){
        float Hurt_A = 1f;
        float Hurt_B = 1f;
        ElementInformation element_A = A.GetComponent<ElementInformation>();
        ElementInformation element_B = B.GetComponent<ElementInformation>();
        switch(state){
            case 0b00001:
            case 0b00010:
            case 0b00100:
            case 0b01000:
            case 0b10000:
                return new Vector2(B.GetComponent<ElementInformation>().GetElementRank() + 1, A.GetComponent<ElementInformation>().GetElementRank() + 1);
            case 0b00011:
            case 0b00101:
            case 0b01001:
            case 0b10001:
                return new Vector2(B.GetComponent<ElementInformation>().GetElementRank() + 1, A.GetComponent<ElementInformation>().GetElementRank() + 1);
            case 0b00110:
                Hurt_A = 1.5f;
                Hurt_B = 1.5f;
                return new Vector2((B.GetComponent<ElementInformation>().GetElementRank() + 1) * Hurt_B, (A.GetComponent<ElementInformation>().GetElementRank() + 1) * Hurt_A);
            case 0b01010:
                if(element_A.GetElementType() == (int)ElementInformation.ElementIndex.e_fire){
                    Hurt_A = 2f;
                    Hurt_B = 0.1f;
                }else{
                    Hurt_A = 0.1f;
                    Hurt_B = 2f;
                }
                return new Vector2((B.GetComponent<ElementInformation>().GetElementRank() + 1) * Hurt_B, (A.GetComponent<ElementInformation>().GetElementRank() + 1) * Hurt_A);
            case 0b01100:
                if(element_A.GetElementType() == (int)ElementInformation.ElementIndex.e_wood){
                    Hurt_A = 2f;
                    Hurt_B = 0.1f;
                }else{
                    Hurt_A = 0.1f;
                    Hurt_B = 2f;
                }
                return new Vector2((B.GetComponent<ElementInformation>().GetElementRank() + 1) * Hurt_B, (A.GetComponent<ElementInformation>().GetElementRank() + 1) * Hurt_A);
                //以上为存在克制关系的属性
            case 0b10010:
            case 0b10100:
            case 0b11000:
                return Vector2.zero;//有关时元素的计算尚未确定，待定
        }
        return -Vector2.one;
    }


}
