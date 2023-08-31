using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBeginner : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inRange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            SceneManage.BeginScene();
        }   
    }
    private void OnMouseDown()
    {
        Debug.Log("Mouse button down!");
        SceneManage.BeginScene();
    }
    private void OnMouseEnter()
    {
        inRange = true;
        Debug.Log("Mouse entered object area!");
    }

    private void OnMouseExit()
    {
        inRange = false;
        Debug.Log("Mouse left object area!");
    }
}
