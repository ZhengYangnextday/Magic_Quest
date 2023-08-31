using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassTriggerButton : MonoBehaviour
{
    // Start is called before the first frame update
    MassTrigger masstrigger;
    void Awake(){
        masstrigger = gameObject.GetComponent<MassTrigger>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PositionChange();
    }
    void PositionChange(){
        if(masstrigger.GetState()){
            transform.localPosition = Vector3.zero;
        }else{
            transform.localPosition = new Vector3(0,  0.5f * (1 - masstrigger.GetMassRate()), 0);
        }
    }
}
