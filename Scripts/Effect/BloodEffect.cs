using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private float TimetoDestory = 0.4f;
    void Start()
    {
        Destroy(gameObject, TimetoDestory);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
