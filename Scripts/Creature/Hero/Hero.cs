using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class Hero : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private GameObject magic;//存储魔法预制体
    private int skill_state;//选择使用哪个魔法
    private bool summonMode = false;//是否处于召唤状态
    //public float force = 0.4f;//跳跃时向上施加力
    //***
    
    public float speed = 4f;//向左/向右的速度
    //public float speed_changetime = 0.2f;//0.5s加速到最大速度
    private bool air;//判断是否在空中, true - 空中， false - 地面
    private float distance = 1f; 
    private bool air_direction;//判断空中朝向， true - 上升， false - 下降
    private bool forward_direction = true;//人物朝向， false - 左， true - 右
    private Vector3 last_position;
    private bool X_change;
    // public float jumpCount = 0f;//按的越久，跳的越高
    
    //***目前功能被移除
    public float jumpCoolTime = 0.1f;//跳跃按键时间
    private float jumpDuration;
    private float jumpStartTime;
    // public float jumpForce = 8f;//起跳功率
    public float jumpInitSpeed = 0.5f;//跳跃有初始速度
    private bool isJumping = false;//是否处于起跳阶段
    // public float minJumpHeight = 1f; // 最小起跳高度
    // public float maxJumpHeight = 2f; // 最大起跳高度
    // public float distance = 0.1f;//离地距离，目前已经不用了

    private float attack_cool_time = 0f;//攻击间隔计时器
    public float max_attack_cool_time = 0.15f;//攻击间隔
    public enum Hero_Mode{
        h_normal,
        h_attack,
        h_magic,
        h_summon
    } 
    public int hero_mode = (int)Hero_Mode.h_normal;
    public LayerMask groundLayer;
    //判定是否浮空
    void Start()
    {
        summonMode = BookSystem.GetSummonMode();
        last_position = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        magic = Resources.Load("Test/Circle") as GameObject;
        gameObject.GetComponent<CapsuleCollider2D>().sharedMaterial.friction = 0;
        gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 0;
        DistanceCalculate();
    }

    // Update is called once per frame
    void Update()
    {
        if(air == true && air_direction == false){
            rb.gravityScale = 20;
        }else{
            rb.gravityScale = 1;
        }   
        // if(rb.velocity.y <= -0.1 | rb.velocity.y >= 0.1f){
        //     air = true;
        // }else{
        //     air = false;
        // } 最高处速度为0
        DistanceCalculate();
        rb.gravityScale = 1;
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance + 0.2f, groundLayer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance + 0.3f, groundLayer);
        
        // if(hit.collider != true){
        //     air = true;
        // }else{
        //     air = false;
        // }
        State_Change();
        if(summonMode == false){
            Attack();
        }else{
            Summon();
        }
        ChangeSummonMode();
        air = (!rb.IsTouchingLayers(groundLayer)) || (hit.collider != true);
        // air = (!rb.IsTouchingLayers(groundLayer));
        XChangeCheck();
        SpeedUp();
        Air_direction_check();
        Jump();
        //air_direction_test();
        MaxJumpCheck();
        SetCheatMode();
        // 检测是否在空中
        // SpeedUp();
        // Air_direction_check();
        // Jump();
        // air_direction_test();
        Die();
    }
    private void SetCheatMode(){
        if(Input.GetKeyDown(KeyCode.C)){
            BookSystem.SetCheatMode();
        }
    }
    private void air_direction_test(){
        if(air == false){
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }else{
            if(air_direction == true){
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
            }else{
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            }
        }
        
    }
    public bool GetXChange(){
        return X_change;
    }
    private void XChangeCheck(){
        if(Mathf.Abs(transform.position.x - last_position.x) <= 0.001){
            X_change = false;
        }else{
            X_change = true;
        }
        last_position = transform.position;
    }
    private void Air_direction_check(){
        if(!air){
            // jumpCount = 0f;
            //rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }else{
            if(rb.velocity.y > 0.01)
            {
                air_direction = true;//上升阶段
            }else if(rb.velocity.y < 0.01)
            {
                air_direction = false;//下降阶段
            }
        }
    }
    // private void Jump(){
    //     if(air && Input.GetKeyDown(KeyCode.W)){
    //         Vector2 v = rb.velocity;
    //         v[1] = jumpInitSpeed;
    //         rb.velocity = v;
    //     }
    //     jumpCount += Time.deltaTime;
    //     if(jumpCount < jumpCoolTime && Input.GetKey(KeyCode.W)){
    //         rb.AddForce(Vector3.up * (force), ForceMode2D.Impulse);
    //     }
    // }
    private void MaxJumpCheck(){
        if(rb.velocity.y > jumpInitSpeed * 4){
            rb.velocity = new Vector2(rb.velocity.x, jumpInitSpeed * 4);
        }
    }
    private void Jump(){
        if (Input.GetKeyDown(KeyCode.W) && !isJumping && !air) {
            isJumping = true;
            //jumpCount += Time.deltaTime;
            jumpStartTime = Time.time;
            rb.velocity = new Vector2(rb.velocity[0], jumpInitSpeed);
            rb.gravityScale = 0;
       }
       else if (Input.GetKey(KeyCode.W) && isJumping) {
           jumpDuration = Time.time - jumpStartTime;
           if (jumpDuration < jumpCoolTime) {
               //rb.velocity += Vector2.up * ( jumpForce / jumpDuration) * Time.smoothDeltaTime;
                rb.velocity = new Vector2(rb.velocity[0], jumpInitSpeed * (1 + 2 * jumpDuration / jumpCoolTime));
           }
           else {
                rb.velocity = new Vector2(rb.velocity[0], jumpInitSpeed * 3);
                isJumping = false;
                rb.gravityScale = 1;
           }
       }
       else if (Input.GetKeyUp(KeyCode.W) && isJumping) {
            jumpDuration = Time.time - jumpStartTime;
            if (jumpDuration < jumpCoolTime) {
                //rb.velocity += Vector2.up * ( jumpForce / jumpDuration) * Time.smoothDeltaTime;
                    rb.velocity = new Vector2(rb.velocity[0], jumpInitSpeed * (1 + 2 * jumpDuration / jumpCoolTime));
            }
            else {
                    rb.velocity = new Vector2(rb.velocity[0], jumpInitSpeed * 3);
                    // isJumping = false;
                    // rb.gravityScale = 1;
            }
            rb.gravityScale = 1;
            isJumping = false;
            // float jumpHeight = Mathf.Clamp(jumpCount, minJumpHeight, maxJumpHeight);
            // rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
       }
    }
    private void Die(){
        if(!gameObject.GetComponent<CreatureInformation>().Get_Live()){
            Destroy(gameObject);
            DeadMenu();
        }
    }
    //这里是可以添加动画的

    public bool GetAir(){
        return air;
    }
    //接口，判定是否在空中

    public bool GetDirection(){
        return forward_direction;
    }
    //接口，判定方向
    private void ChangeSummonMode(){
        //Debug.Log("Summon Mode");
        if(Input.GetKeyDown(KeyCode.M)){
            summonMode = !summonMode;
            BookSystem.SetSummonMode(summonMode);
        }
    }
    private void SpeedUp(){
        // int direction = 0;
        // if (Input.GetKey(KeyCode.A))
        // {
        //     direction += -1;
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     direction += 1;
        // }
        // if(direction > 0){
        //     if(forward_direction == false){
        //         rb.velocity = new Vector2(0, rb.velocity.y);
        //         forward_direction = true;
        //     }
        // }else if(direction < 0){
        //     if(forward_direction == true){
        //         rb.velocity = new Vector2(0, rb.velocity.y);
        //         forward_direction = false;
        //     }
        // }
        // rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(speed * direction, rb.velocity.y), speed_changetime);
        // if(Mathf.Abs(rb.velocity[0]) >= speed){
        //     rb.velocity = new Vector2(forward_direction?speed:-speed, rb.velocity.y);
        // }
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal > 0){
            forward_direction = true;
        }else if(moveHorizontal < 0){
            forward_direction = false;
        }
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    private void Attack(){
        if(hero_mode == (int)Hero_Mode.h_normal && !summonMode){
            attack_cool_time = 0;
            // Debug.Log("Attack");
            if(Input.GetKeyDown(KeyCode.J)){
                hero_mode = (int)Hero_Mode.h_attack;
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.K))
            {
                hero_mode = (int)Hero_Mode.h_magic;
                int id = BookSystem.GetNowEquip();
                // Debug.LogWarning(id);
                if(BookSystem.IdInRange(id))
                {
                    int rank = BookSystem.GetCardRank(id);
                    int type = BookSystem.GetCardType(id);
                    if(BookSystem.UseCard(id, 1))
                    {
                        switch(BookSystem.GetCardFunction(id))
                        {
                            case (int)MonsterInformation.function.Attack: //Attack
                                MagicCreate.CreateAttackMagic(rank, type, forward_direction?Vector3.right:Vector3.left, forward_direction?new Vector3(transform.position.x + 0.7f, transform.position.y, 0):new Vector3(transform.position.x - 1.0f, transform.position.y, 0), tag = "positive");
                                break;
                            case (int)MonsterInformation.function.Defend:
                                MagicCreate.CreateDefendMagic(rank, type, gameObject);
                                break;
                            case (int)MonsterInformation.function.Heal:
                                MagicCreate.CreateHealMagic(rank, type, gameObject);
                                break;
                        //Magic_Create_Test();//测试代码
                        }
                    }
                }
            }
        }
        else
        {
            attack_cool_time += Time.deltaTime;
            if(attack_cool_time > max_attack_cool_time){
                hero_mode = (int)Hero_Mode.h_normal;
            }
        }
    }
    private void Summon(){
        if(hero_mode == (int)Hero_Mode.h_normal && summonMode){
            attack_cool_time = 0;
            // Debug.Log("Attack");
            if(Input.GetKeyDown(KeyCode.J)){
                hero_mode = (int)Hero_Mode.h_attack;
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.K))
            {
                hero_mode = (int)Hero_Mode.h_summon;
                int id = BookSystem.GetNowEquip();
                // Debug.LogWarning(id);
                if(BookSystem.IdInRange(id))
                {
                    if(BookSystem.UseCard(id, BookSystem.GetCardSummonNum(id))){
                        GameObject Summon_Monster = Instantiate(BookSystem.MonsterIndexQuest[id]);
                        Summon_Monster.tag = gameObject.tag;
                        Summon_Monster.transform.position = gameObject.transform.position;
                        Summon_Monster.GetComponent<MonsterInformation>().SetSummon();
                    }
                }
            }
        }
        else
        {
            attack_cool_time += Time.deltaTime;
            if(attack_cool_time > max_attack_cool_time){
                hero_mode = (int)Hero_Mode.h_normal;
            }
        }
    }
    public bool GetSummonState(){
        return summonMode;
    }
    public bool GetAirDirection(){
        return air_direction;
    }

    public int GetHeroMode(){
        return (int)hero_mode;
        /*
        Hero Mode
        1. Normal - 0
        2. Attack - 1
        3. Magic - 2
        */
    }
    // private void Magic_Create_Test(){
    //     GameObject new_magic = Instantiate(magic);
    //     new_magic.transform.position = transform.position;
    //     new_magic.transform.position = new Vector3(0.5f + new_magic.transform.position[0], new_magic.transform.position[1], new_magic.transform.position[2]);
    //     new_magic.GetComponent<ElementInformation>().SetElementType(skill_state);
    //     //new_magic.GetComponent<MagicInformation>().Init(); //这一步还未执行start函数，会报错
    //     new_magic.GetComponent<MagicInformation>().direction = forward_direction;
    //     new_magic.GetComponent<MagicInformation>().Init(); //这一步还未执行start函数，会报错
    //     new_magic.tag = "positive";
    // }
    private void DeadMenu(){
        SceneManager.LoadScene("Scenes/Formal/System/DeadMenu");
    }
    private void State_Change(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            skill_state = 0;
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            skill_state = 1;
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            skill_state = 2;
        }
        BookSystem.SetEquipNum(skill_state);
    }
    private void DistanceCalculate(){
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        // Vector3 bottomPoint = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
        CapsuleCollider2D capsulecollider = gameObject.GetComponent<CapsuleCollider2D>();
        Vector3 bottomPoint = (Vector2)transform.position + capsulecollider.offset - Vector2.up * (capsulecollider.bounds.extents.y);
        //Debug.Log(bottomPoint);
        // 计算中心点的位置
        Vector3 centerPoint = transform.position;

        // 计算两点之间的距离
        distance = Vector3.Distance(centerPoint, bottomPoint);
        // Debug.Log(distance);
    }
}
