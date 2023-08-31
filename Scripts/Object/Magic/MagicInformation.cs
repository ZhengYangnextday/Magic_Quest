using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicInformation : MonoBehaviour
{
    private Rigidbody2D rb;
    private ElementInformation element;
    public float speed = 6f;
    public float alive_time = 5f;
    //public bool direction = false;
    //false - 左， true - 右
    //后续版本中被替代了
    public float min_speed = 4f;
    public float mass = 1f;
    public bool isDie = true;
    private bool alive = true;
    public bool killedByMap = false;
    //private float avoid_time = 0f;
    //避免二次碰撞时间，仅在元素反应时使用
    public bool isDirectionSet = false;
    public Vector2 setDirection = Vector2.right;
    // public GameObject follower;
    // public bool isFollower = false; 
    // Start is called before the first frame update
    void Awake(){
        element = gameObject.GetComponent<ElementInformation>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb == null || element == null){
            Destroy(gameObject);
        }
        rb.gravityScale = 0;
        // GameObject[] targets = GameObject.FindGameObjectsWithTag(gameObject.tag);
        // Ignore collision between the current rigidbody and all targets
        // foreach (GameObject target in targets)
        // {
        //     Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
        // }
        // Init();
        // 如果在这里写可能会因为tag尚未重置导致出错
        //magic没有重力
    }
    void Start()
    {
        // element = gameObject.GetComponent<ElementInformation>();
        // rb = gameObject.GetComponent<Rigidbody2D>();
        // if(rb == null || element == null){
        //     Destroy(gameObject);
        // }
        // rb.gravityScale = 0;
        // //magic没有重力
        // Init();
        if(isDirectionSet){
            //rb.velocity = setDirection * speed;
            Init();
        }else{
            Debug.Log("Direction Must Set");
            Destroy(gameObject);
        }
        GameObject[] targets = GameObject.FindGameObjectsWithTag(gameObject.tag);
        // Ignore collision between the current rigidbody and all targets
        foreach (GameObject target in targets)
        {
            if(target.GetComponent<Collider2D>()){
                Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
            }
        }
        //Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive == false && isDie){
            Destroy(gameObject);
        }
        if(!isDie){
            SetAlive(true);
        }
        Destroy_Check();
        ChangeRotation();
        // Avoid_Confliction();
    }

    private void Destroy_Check(){
        alive_time -= Time.deltaTime;
        if(alive_time <= 0){
            SetAlive(false);
        }
        Velocity_Check();
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "map" && killedByMap){
            SetAlive(false);
        }else if(collision.gameObject.tag != gameObject.tag){
            if(gameObject.tag == "positive" && collision.collider.tag == "negative"){
                ElementInteractive.Interactive(gameObject, collision.gameObject);
            }else if(gameObject.tag == "shooter"){
                ElementInteractive.Interactive(gameObject, collision.gameObject);
            }
            if(collision.gameObject.GetComponent<CreatureInformation>()){
                //应该有点啥才行
            }
        }
    }

    public void SetAlive(bool alive_to_set){
        alive = alive_to_set;
        element.SetAlive(alive_to_set);//后续代码中没体现出区别，在这里set
    }
    public bool GetAlive(){
        return alive;
    }
    private void Velocity_Check(){
        if(rb.velocity.magnitude < min_speed){
            SetAlive(false);
        }
    }
    public void Init(){
        //Debug.Log(rb != null);
        GameObject[] targets = GameObject.FindGameObjectsWithTag(gameObject.tag);
        // Ignore collision between the current rigidbody and all targets
        foreach (GameObject target in targets)
        {
            if(target.GetComponent<Collider2D>()){
                Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
            }
        }
        // if(isDirectionSet){
        //     rb.velocity = setDirection * speed;
        // }else{
        //     if(direction == false){
        //         rb.velocity= new Vector2(-1, 0) * speed * (int)(element.GetElementRank() + 1);
        //     }
        //     else{
        //         rb.velocity= new Vector2(1, 0) * speed * (int)(element.GetElementRank() + 1);
        //     }
        // }
        rb.mass = mass * (int)(element.GetElementRank() + 1);
        speed = speed * 1 + ((int)(element.GetElementRank()) / 4);
        rb.velocity = setDirection * speed;
        if(!killedByMap){
            GameObject[] map_targets = GameObject.FindGameObjectsWithTag("map");
            // Ignore collision between the current rigidbody and all targets
            foreach (GameObject target in map_targets)
            {
                
                if(target.GetComponent<CompositeCollider2D>()){
                    //Debug.Log("asdf");
                    Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), target.GetComponent<CompositeCollider2D>());
                }                  
            }
        }
        //Test_Show();
    }

    private void Test_Show(){
        SpriteRenderer game_render = gameObject.GetComponent<SpriteRenderer>();
        switch(element.GetElementType()){
            case (int)ElementInformation.ElementIndex.e_fire:
                game_render.material.color = Color.red;
                break;
            case (int)ElementInformation.ElementIndex.e_water:
                game_render.material.color = Color.blue;
                break;
            case (int)ElementInformation.ElementIndex.e_wood:
                game_render.material.color = Color.green;
                break;
        }
        transform.localScale = (int)(element.GetElementRank() + 1) * Vector3.one;
    }
    // private void Avoid_Confliction(){
    //     if(avoid_time > 0){
    //         avoid_time -= Time.deltaTime;
    //         rb.isKinematic = true;
    //     }else{
    //         rb.isKinematic = false;
    //     }
    // }
    // public void Set_Avoid(){
    //     avoid_time = 0.05f;
    // }
    //出现bug再修吧...

    private string OppositeTag(){
        switch(gameObject.tag){
            case "positive":
                return "negative";
            case "negative":
                return "positive";
            default:
                return "";
        }
    }
    void ChangeRotation(){
        float result = CalculateAngle(Vector2.right, rb.velocity);
        result = rb.velocity.y >= 0.0f?result:360f - result;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, result);
    }
    private float CalculateAngle(Vector2 a, Vector2 b){
        float sum = a.magnitude * b.magnitude;
        if(sum == 0){
            return 0;
        }
        float cross = a[0] * b[0] + a[1] * b[1];
        return 360 * (Mathf.Acos(cross/sum)/ (2 * Mathf.PI)); 
    }
}
