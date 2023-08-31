using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrap : MonoBehaviour
{
    // Start is called before the first frame update
    private bool state = false;
    private TrapInformation trapInformation;
    private GameObject targetObject;
    public LayerMask changedLayer;
    void Awake(){
        trapInformation = gameObject.GetComponent<TrapInformation>();
        if(trapInformation == null){
            Debug.Log("Require trap information");
            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        // if(state){
        //     testLadder();
        // }
    }
    private void CheckState(){
        state = trapInformation.GetTrapState();
    }
    public bool getState(){
        return state;
    }
    public void testLadder(){
        //transform.localScale = new Vector2(2,9);
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        //gameObject.layer = (int)changedLayer;
    }
    //虽然叫梯子，实际工作方式为形成台阶，帮助玩家跳跃,具体工作原理如下
    //初始状态无碰撞体，经过碰撞后具有碰撞体，同时体积变大，形成跳跃的中间平台
}
