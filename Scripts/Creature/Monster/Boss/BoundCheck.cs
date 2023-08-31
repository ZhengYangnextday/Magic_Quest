using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D[] In;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D()
    {
        // 返回与触发器接触的所有碰撞器
        In = Physics2D.OverlapBoxAll(transform.position, transform.localScale / 2f, 0);
    }
}
