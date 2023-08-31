using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPause : MonoBehaviour
{
    private float speed;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P)){
            active = !active;
        }

        if (!active){
            return;
        }
        transform.position += new Vector3(0f, 1f, 0f) * speed * Time.deltaTime;
    }
}

