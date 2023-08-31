using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrap : MonoBehaviour
{
    // Start is called before the first frame update
    private TrapInformation trapInformation;
    private bool open = false;
    public bool isMagicThrough = true;
    private Collider2D myCollider;
    //魔法是否可穿过, true代表可穿过，false代表不可穿过
    void Awake(){
        trapInformation = gameObject.GetComponent<TrapInformation>();
        trapInformation.trapTargetObject = gameObject;
        if(trapInformation == null){
            Debug.Log("Require trap information");
            Destroy(gameObject);
        }
        myCollider = gameObject.GetComponent<Collider2D>();
        if(myCollider == null){
            Debug.Log("Require collider");
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
        //OpenStateChange();
        //testDoor();//仅限测试使用
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<MagicInformation>()){
            if(isMagicThrough){
                Physics2D.IgnoreCollision(myCollider, collision.collider);
            }else{
                collision.gameObject.GetComponent<MagicInformation>().SetAlive(false);
            }
        }
    }
    public bool GetTrapState(){
        return open;
    }
    private void CheckState(){
        open = trapInformation.GetTrapState();
    }
    private void OpenStateChange(){
        if(open){
            myCollider.enabled = false;
        }else{
            myCollider.enabled = true;
        }
    }
    private void testDoor(){
        if(open){
            Destroy(gameObject);
        }
    }
}
