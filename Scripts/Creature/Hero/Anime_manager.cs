using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime_manager : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Animator _animator;
    private Hero _hero;
    private Rigidbody2D rb;
    private float attack_cooldownTime = 0.15f;
    public float attackTime = 0f;

    void Start()
    {   _hero = GameObject.Find("Hero").GetComponent<Hero>();
        _animator = GameObject.Find("Hero"). GetComponent<Animator>();
        rb = GameObject.Find("Hero"). GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        update_ani_para();//更新动画器参数
        change_dire();//改变人物朝向
        
    }

    void update_ani_para(){
        _animator.SetBool("isAir",_hero.GetAir());
        _animator.SetFloat("V_X",rb.velocity.x);
        _animator.SetFloat("V_Y",rb.velocity.y);
        _animator.SetBool("pos_change",_hero.GetXChange());
        //_animator.SetBool("isJump",_hero.Air_direction_check());
        
        //&& _hero.GetHeroMode()==1
        if(Input.GetKeyDown(KeyCode.J) && ((Time.time-attackTime)>=0.3f)){
            _animator.SetTrigger("attack");
            attackTime = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.K)){
            _animator.SetTrigger("magic");
        }
    }

    void change_dire(){
        bool forward_direction = _hero.GetDirection();
        transform.localScale = new Vector2((forward_direction?1:-1),1);
        //Debug.Log(forward_direction);
    }
    
}



    