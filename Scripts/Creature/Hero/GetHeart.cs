using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHeart : MonoBehaviour
{
    // Start is called before the first frame update
    void Awakw(){
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<Hero>()){
            float maxHeartPoint = collider.GetComponent<CreatureInformation>().mMaxHeartPoint;
            maxHeartPoint += 5;
            collider.GetComponent<CreatureInformation>().SetMaxHeartPoint(maxHeartPoint);
            MagicCreate.CreateHealMagic(Random.Range(0,5), Random.Range(1,4), collider.gameObject);
            BookSystem.HeroHeartNumUp();
            Destroy(gameObject);
        }
    }
}
