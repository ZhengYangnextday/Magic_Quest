using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicControlTest : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject magic_element;
    public int state = 1;
    void Start()
    {
        magic_element = Resources.Load("Test/Circle") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_Create();
        State_Change();
    }
    private void Mouse_Create(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos[2] = 0f;
            MagicCreate.CreateAttackMagic(1, state, Vector3.right, pos, "positive");
        }else if(Input.GetMouseButtonDown(1)){
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos[2] = 0f;
            MagicCreate.CreateAttackMagic(1, state, Vector3.left, pos, "negative");
        }
    }

    private void State_Change(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            state = 1;
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            state = 2;
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            state = 3;
        }
    }
}
