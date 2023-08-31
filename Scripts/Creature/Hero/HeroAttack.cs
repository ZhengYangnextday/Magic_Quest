using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private float ExistTime = 1f;
    private float ExistTimeCounter = 0f;
    void Awake(){

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Exist();
    }
    void Exist(){
        // ExistTimeCounter += Time.deltaTime;
        // if(ExistTimeCounter > ExistTime){
        //     ExistTimeCounter = 0;
        //     gameObject.SetActive(false);
        // }
        if(transform.parent.gameObject.GetComponent<Hero>().hero_mode != (int)Hero.Hero_Mode.h_attack){
            gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        MagicInformation magicInformation = collision.gameObject.GetComponent<MagicInformation>();
        ElementInformation elementInformation = collision.gameObject.GetComponent<ElementInformation>();
        if(magicInformation){
            MagicCreate.CreateAttackMagic(elementInformation.rank, elementInformation.type, -magicInformation.setDirection, collision.transform.position, "positive");
        }
    }
}
