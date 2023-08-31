using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private TriggerInformation trigger;
    public int targetElementType = 0b00000;
    //和元素反应那里相同
    private string beginTag = "neural";
    private int triggerElementType = -1;//触发机关的元素，如果需要的化可以用
    void Awake(){
        trigger = gameObject.GetComponent<TriggerInformation>();
        if(trigger == null){
            Debug.Log("Without Trigger");
            Destroy(gameObject);
        }
        trigger.SetTriggerState(false);
        // if(targetElementType == 0){
        //     Debug.Log("No Element Set");
        //     Destroy(gameObject);
        // }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        ElementInformation collisionElement = collision.gameObject.GetComponent<ElementInformation>();
        if(collisionElement){
            if(((1 << collisionElement.GetElementType()) & targetElementType) > 0){
                trigger.SetTriggerState(true);
                triggerElementType = collisionElement.GetElementType();
            }
        }
        beginTag = collision.gameObject.tag;
    }

    void OnTriggerEnter2D(Collider2D collider){
        ElementInformation colliderElement = collider.gameObject.GetComponent<ElementInformation>();
        Debug.Log("trigger enter");
        if(colliderElement){
            Debug.Log("asd");
            if(((1 << colliderElement.GetElementType()) & targetElementType) > 0){
                trigger.SetTriggerState(true);
                triggerElementType = colliderElement.GetElementType();
            }
        }
        //beginTag = collider.gameObject.tag;
    }
    public void Init(int newElementType){
        if(newElementType >= 32 || newElementType < 0){
            Debug.Log("Wrong Change");
            return;
        }
        trigger.SetTriggerState(false);
        targetElementType = newElementType;
        triggerElementType = -1;
    }

    public bool getState(){
        return trigger.GetTriggerState();
    }
    public int GetTriggerElementType(){
        return triggerElementType;
    }
}
