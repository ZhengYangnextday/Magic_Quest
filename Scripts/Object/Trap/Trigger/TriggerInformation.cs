using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInformation : MonoBehaviour
{
    private bool triggerState = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTriggerState(bool afterState){
        triggerState = afterState;
    }
    public bool GetTriggerState(){
        return triggerState;
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.GetComponent<MagicInformation>()){
            collision.gameObject.GetComponent<MagicInformation>().SetAlive(false);
        }
    }
    public void Init(int newElementType){
        if(gameObject.GetComponent<MassTrigger>()){
            gameObject.GetComponent<MassTrigger>().Init();
        }else{
            gameObject.GetComponent<ElementTrigger>().Init(newElementType);
        }
    }
}
