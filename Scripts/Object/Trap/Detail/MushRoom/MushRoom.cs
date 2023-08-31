using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private float init_distance;
    private float init_position_y;
    private ElementTrigger elementTrigger;
    private bool grownUP;
    private bool burnDown;
    public float targetScaleSize = 4f;
    private float growUPTime = 1f;
    private float growUPTimeCounter = 0f;
    private float burnDownTime = 3f;
    private float burnDownTimeCounter = 0f;
    void Start()
    {
        init_distance = DistanceCalculate();
        init_position_y = transform.position.y;
        elementTrigger = gameObject.GetComponent<ElementTrigger>();
        if(elementTrigger == null){
            Debug.Log("Trigger is missing");
            Destroy(gameObject);
        }
        elementTrigger.Init(0b00110);
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        BurnDown();
        GrowUp();
    }
    private void CheckState(){
        if(elementTrigger.getState()){
            int elementType = elementTrigger.GetTriggerElementType();
            if(elementType != -1){
                if(elementType == (int)ElementInformation.ElementIndex.e_fire){
                    burnDown = true;
                }
                if(elementType == (int)ElementInformation.ElementIndex.e_water){
                    grownUP = true;
                    elementTrigger.Init(0b00010);
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
    private void GrowUp(){
        if(grownUP){
            growUPTimeCounter += Time.deltaTime;
            transform.localScale = Vector3.one * (1 + growUPTimeCounter / growUPTime * (targetScaleSize - 1));
            float distance_differ = DistanceCalculate() - init_distance;
            transform.position = new Vector3(transform.position.x, init_position_y + distance_differ, 0);
            if(growUPTimeCounter > growUPTime){
                transform.Find("AfterCollider").gameObject.SetActive(true);
                grownUP = false;
            }
        }

    }
    private float DistanceCalculate(){
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        Vector3 bottomPoint = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);

        // 计算中心点的位置
        Vector3 centerPoint = transform.position;

        // 计算两点之间的距离
        float distance = Vector3.Distance(centerPoint, bottomPoint);
        return distance;
        // Debug.Log(distance);
    }
}
