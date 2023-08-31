using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public int elementType = (int)ElementInformation.ElementIndex.e_fire;
    public int elementRank = (int)ElementInformation.RankIndex.D;
    public float timeIterval = 0.5f;
    private float timeItervalCounter = 0f;
    private Transform parent;
    public bool state = true;
    public bool alwaysShoot = true;
    public bool isInit = false;
    public string shooter_tag = "shooter"; 
    private TrapInformation trapInformation;
    void Start()
    {
        parent = transform.parent;
        trapInformation = gameObject.GetComponent<TrapInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        ChangeState();
    }
    void Shoot(){
        if(state || alwaysShoot){
            timeItervalCounter += Time.deltaTime;
            if(timeItervalCounter > timeIterval){
                Magic();
                timeItervalCounter = 0f;
            }
        }else{
            timeItervalCounter = 0f;
        }
    }
    void ChangeState(){
        if(trapInformation.GetTrapState()){
            if(isInit == true){
                trapInformation.Init(0);
            }
            state = !state;
        }
    }
    void Magic(){
        MagicCreate.CreateAttackMagic(elementRank, elementType, new Vector3(Mathf.Cos((parent.rotation.eulerAngles.z / 360) * 2 * Mathf.PI),Mathf.Sin((parent.rotation.eulerAngles.z / 360) * 2 * Mathf.PI) ,0), transform.position, shooter_tag);
    }
}
