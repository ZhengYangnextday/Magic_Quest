using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeCounter = 0f;
    public float maxTime = 0.06f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter > maxTime){
            Destroy(gameObject);
        }
    }
}
