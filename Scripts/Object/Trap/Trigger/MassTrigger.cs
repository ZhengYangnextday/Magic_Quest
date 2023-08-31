using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassTrigger : MonoBehaviour
{
    private TriggerInformation trigger;
    private float mass = 0;
    public float triggerMass = 2f;
    private bool isInit = false;
    private float massRate = 0;
    private float inittime = 0f;
    private enum MassTriggerType{
        unknown,
        discrete,
        continous
    }
    //discrete代表一次性，一次满足条件即可，contious代表连续，需一直满足条件
    public int type = (int)MassTriggerType.unknown;
    void Awake(){
        mass = 0;
        trigger = gameObject.GetComponent<TriggerInformation>();
        if(trigger == null){
            Debug.Log("Without Trigger");
            Destroy(gameObject);
        }
        trigger.SetTriggerState(false);//初始触发器状态为0
    }
    void Start()
    {
        if(type == (int)MassTriggerType.unknown){
            type = (int)MassTriggerType.discrete;
        }//代表一次性
    }

    // Update is called once per frame
    void Update()
    {
        InitCalculate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        mass = 0f;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Rigidbody2D rb = contact.otherCollider.attachedRigidbody;
            if (rb != null)
            {
                Debug.Log("Mass of " + rb.name + ": " + rb.mass);
                mass += rb.mass;
            }
        }
        if(mass >= triggerMass && !isInit){
            trigger.SetTriggerState(true);
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
        
        Rigidbody2D rb = collider.attachedRigidbody;
        if (rb != null)
        {
            // Debug.Log("Mass of " + rb.name + ": " + rb.mass);
            mass += rb.mass;
        }
        if(mass >= triggerMass && !isInit){
            trigger.SetTriggerState(true);
        }
    }
    void OnCollisionStay2D(Collision2D collision){
        mass = 0f;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Rigidbody2D rb = contact.otherCollider.attachedRigidbody;
            if (rb != null)
            {
                Debug.Log("Mass of " + rb.name + ": " + rb.mass);
                mass += rb.mass;
            }
        }
        if(mass >= triggerMass && !isInit){
            trigger.SetTriggerState(true);
        }else if(type == (int)MassTriggerType.continous){
            trigger.SetTriggerState(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        mass = 0f;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Rigidbody2D rb = contact.otherCollider.attachedRigidbody;
            if (rb != null)
            {
                Debug.Log("Mass of " + rb.name + ": " + rb.mass);
                mass += rb.mass;
            }
        }
        if(mass >= triggerMass && !isInit){
            trigger.SetTriggerState(true);
        }else if(type == (int)MassTriggerType.continous){
            trigger.SetTriggerState(false);
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        Rigidbody2D rb = collider.attachedRigidbody;
        if (rb != null)
        {
            // Debug.Log("Mass of " + rb.name + ": " + rb.mass);
            mass -= rb.mass;
        }
        if(mass >= triggerMass && !isInit){
            trigger.SetTriggerState(true);
        }else if(type == (int)MassTriggerType.continous){
            trigger.SetTriggerState(false);
        }
    }

    public float GetMassRate(){
        // Debug.Log(mass/triggerMass);
        return mass/triggerMass;
    }

    public void Init(){
        isInit = true;
        trigger.SetTriggerState(false);
    }
    private void InitCalculate(){
        if(isInit){
            inittime += Time.deltaTime;
            if(inittime > 1f){
                isInit = false;
            }
        }
    }
    public bool GetState(){
        return trigger.GetTriggerState();
    }
}
