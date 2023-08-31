using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInformation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trapTargetObject;
    public GameObject[] trapTrigger;
    private enum TrapIndex{
        Door,
        Ladder,
        Shooter
    }
    private int targetObjectType;
    private bool trapState;
    void Awake(){
        gameObject.tag = "neural";//避免魔法与其发生冲突,在各种类中单独判断如何抵消魔法
        foreach(GameObject trigger in trapTrigger){
            if(!trigger.GetComponent<TriggerInformation>()){
                Debug.Log("Trigger without trigger information");
                Destroy(gameObject);
            }
        }
        if(trapTargetObject.GetComponent<DoorTrap>()){
            targetObjectType = (int)TrapIndex.Door;
        }else if(trapTargetObject.GetComponent<LadderTrap>()){
            targetObjectType = (int)TrapIndex.Ladder;
        }else if(trapTargetObject.GetComponent<Shooter>()){
            targetObjectType = (int)TrapIndex.Shooter;
        }else{
            Debug.Log("Target without Details");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetAllTriggerState();
    }

    private void GetAllTriggerState(){
        bool now_state = true;
        foreach(GameObject triggers in trapTrigger){
            if(triggers.GetComponent<TriggerInformation>().GetTriggerState() == false){
                now_state = false;
                break;
            }
        }
        trapState = now_state;
    }
    public bool GetTrapState(){
        return trapState;
    }
    public void Init(int newElementType){
        trapState = false;
        foreach(GameObject triggers in trapTrigger){
            triggers.GetComponent<TriggerInformation>().Init(newElementType);
        }
    }
}
