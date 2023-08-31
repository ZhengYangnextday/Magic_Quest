using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTrap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject magic;
    private TrapInformation trapInformation;


    public Vector2 direction = Vector2.right;

    private float coolTime = 0f;
    public float coolTimeDuration = 0.6f;
    public bool state = true;
    public int stateOnChangeNumber = 0b00001;
    public int stateOffChangeNumber = 0b00000; 
    //false - 关闭， true - 开启
    void Awake(){
        MagicInformation magicInformation = magic.GetComponent<MagicInformation>();
        if(magicInformation == null){
            Debug.Log("magic without information");
            Destroy(gameObject);
        }
        magicInformation.isDirectionSet = true;
        magicInformation.setDirection = direction;
        trapInformation = gameObject.GetComponent<TrapInformation>();
        if(trapInformation == null){
            Debug.Log("Require trap information");
            Destroy(gameObject);
        }
        Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        CheckState();
    }
    private void Shoot(){
        if(state){
            coolTime += Time.deltaTime;
            if(coolTime > coolTimeDuration){
                coolTime = 0;
                Instantiate(magic);
            }
        }else{
            coolTime = 0f;
        }
    }
    private void CheckState(){
         if(trapInformation.GetTrapState()){
            state = !state;
            Init();
         }
    }
    private void Init(){
        switch(state){
            case true:
                trapInformation.Init(stateOnChangeNumber);
                return;
            case false:
                trapInformation.Init(stateOffChangeNumber);
                return;
        }
    }
}
