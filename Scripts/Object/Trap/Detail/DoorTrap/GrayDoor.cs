using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayDoor : MonoBehaviour
{
    // Start is called before the first frame update
    DoorTrap doortrap;
    void Start()
    {
        doortrap = gameObject.GetComponent<DoorTrap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(doortrap.GetTrapState() == true){
            Debug.LogWarning("Trap");
            Destroy(gameObject);
        }
    }
}
