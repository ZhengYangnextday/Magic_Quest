using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    private ElementTrigger elementTrigger;
    private TrapInformation trapInformation;
    private bool burnDown;
    private float burnDownTime = 3f;
    private float burnDownTimeCounter = 0f;
    void Start()
    {
        elementTrigger = gameObject.GetComponent<ElementTrigger>();
        trapInformation = gameObject.GetComponent<TrapInformation>();
        if(elementTrigger == null || trapInformation == null){
            Debug.Log("Trigger or trap's information is missing");
            Destroy(gameObject);
        }
        elementTrigger.Init(0b00010);
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        BurnDown();
    }
    private void CheckState(){
        if(trapInformation.GetTrapState()){
            int elementType = elementTrigger.GetTriggerElementType();
            if(elementType != -1){
                if(elementType == (int)ElementInformation.ElementIndex.e_fire){
                    burnDown = true;
                }
            }
        }
    }
    private void BurnDown(){
        if(burnDown){
            burnDownTimeCounter += Time.deltaTime;
            if(burnDownTimeCounter < 0.25 * burnDownTime){
                transform.Find("fire1").gameObject.SetActive(true);
            }
            else if(burnDownTimeCounter < 0.5 * burnDownTime){
                transform.Find("fire2").gameObject.SetActive(true);
            }
            else if(burnDownTimeCounter < 0.75 * burnDownTime){
                transform.Find("fire3").gameObject.SetActive(true);
            }
            else if(burnDownTimeCounter > burnDownTime){
                Destroy(gameObject);
            }
        }
    }
}
