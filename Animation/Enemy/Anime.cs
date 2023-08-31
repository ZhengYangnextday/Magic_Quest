using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime : MonoBehaviour
{
    private CreatureInformation creatureInformation;
    private Animator _animator;
    private Rigidbody2D rb;
    private bool direction;
    private Vector2 size;
    // Start is called before the first frame update
    void Start()
    {   
        creatureInformation = gameObject.GetComponent<CreatureInformation>();
        _animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        size = transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(creatureInformation.Get_Live() == false){
            _animator.SetTrigger("dead");
        }
        /*if(){
            _animator.SetTrigger("getHurt");
        }*/
        get_direction();
        change_dire();
        
        
    }

    void get_direction(){
        if(rb.velocity.x>=0){
            direction = true;
        }
        else direction = false;
    }
    
    void change_dire(){
        //bool forward_direction = _hero.GetDirection();
        transform.localScale = new Vector2((direction?-1:1)*size.x,size.y);
        //Debug.Log(forward_direction);
    }
}
