using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInformation : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private ElementInformation element;
    public float mheartPoint = 0f;
    public float mMaxHeartPoint = 10f;
    //生物血量
    public int mcreatureId = 0;
    //生物编号，0为主角编号
    private bool malive = true;
    //是否存活，初始状态下为true
    private float mdeadCount = 0f;
    //死亡时间缓冲
    private float this_hurt = 0f;

    public float normalBounceForce = 4f; // 普通弹跳力度
    public float specialBounceForce = 8f; // 特殊弹跳力度

    private PhysicsMaterial2D normalMaterial;
    private PhysicsMaterial2D specialMaterial;
    private PhysicsMaterial2D specialHeroMaterial;

    private float normalMaterial_friction = 0f;
    private float normalMaterial_bounciness = 0f;

    private float specialMaterial_friction = 0f;
    private float specialMaterial_bounciness = 0.02f;
    private float specialHeroMaterial_friction = 0f;
    private float specialHeroMaterial_bounciness = 0.005f;
    private float attackHeroTime = 1.5f;//受击后1.5s内不受到伤害
    private float attackHeroTimeConuter = 0f;
    public float BossAttackCount = 0;
    private bool isHeroAttack = false;
    public bool isBossAttack = false;
    //生物与生物之间相撞时，弹性碰撞增加
    void Awake(){
        element = gameObject.GetComponent<ElementInformation>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb == null || element == null){
            Destroy(gameObject);
        }
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        collider.sharedMaterial = normalMaterial;
    }
    void Start()
    {
        if(!gameObject.GetComponent<Hero>()){
            mheartPoint = mMaxHeartPoint;
        }else{
            mheartPoint = SceneManage.HeroLife;
            mMaxHeartPoint = SceneManage.HeroMaxLife;
        }
        //如果人物初始血量未定，则初始状态下为10f
        ElementInformationCheck();
        //检查是否带有元素组件
        GameObject[] targets = GameObject.FindGameObjectsWithTag(gameObject.tag);
        // Ignore collision between the current rigidbody and all targets
        foreach (GameObject target in targets)
        {
            if(target.GetComponent<Collider2D>()){
                Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
            }else{
                Debug.Log(target.name);
            }
        }

        // 创建两个新的物理材质
        normalMaterial = new PhysicsMaterial2D();
        specialMaterial = new PhysicsMaterial2D();
        specialHeroMaterial = new PhysicsMaterial2D();

        // 设置摩擦系数和恢复系数
        normalMaterial.friction = normalMaterial_friction;
        normalMaterial.bounciness = normalMaterial_bounciness;

        specialMaterial.friction = specialMaterial_friction;
        specialMaterial.bounciness = specialMaterial_bounciness;
        specialHeroMaterial.friction = specialHeroMaterial_friction;
        specialHeroMaterial.bounciness = specialHeroMaterial_bounciness;

        // 将初始物理材料应用于游戏对象的碰撞器
        Collider2D collider = GetComponent<Collider2D>();
        collider.sharedMaterial = normalMaterial;
        // 生物与生物之间相撞时，弹性碰撞增加


    }

    // Update is called once per frame
    void Update()
    {
        HurtCaculate();
        Set_Death();
        HeroAttackAvoid();
    }
    void OnCollisionEnter2D(Collision2D collision){
        //Debug.Log("Collision");
        if(collision.gameObject.tag == "map"){
            return;
        }else if(collision.gameObject.tag == OppositeTag()){
            if (collision.gameObject.GetComponent<CreatureInformation>() && !collision.gameObject.GetComponent<Boss>() && !collision.gameObject.GetComponent<Hero>()) {
                //如果碰到了特殊物体，则增加弹性系数并施加更大的推力
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                collider.sharedMaterial = specialMaterial;
                Debug.Log("creature");
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                rb.AddForce(direction * specialBounceForce, ForceMode2D.Impulse);
            }
            else if(collision.gameObject.GetComponent<Hero>() && collision.gameObject.GetComponent<Boss>()){
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                collider.sharedMaterial = specialHeroMaterial;
                Debug.Log("creature");
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                rb.AddForce(direction * specialBounceForce * 0.6f, ForceMode2D.Impulse);
            }
            ElementInteractive.Interactive(gameObject, collision.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
       //离开特殊物体后回到原来的弹性系数和弹跳力度
        if(collision.gameObject.tag == OppositeTag()){
            if (collision.gameObject.GetComponent<CreatureInformation>()) {
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                collider.sharedMaterial = normalMaterial;
            }
        }
   }

    private void Set_Death(){
        if(mheartPoint < 0.01f){
            Debug.Log("Dangerous");
            //play(animation)//放死亡动画
            //Card.Create(mcreatureId);//掉落物品
            mdeadCount += Time.deltaTime;
            if(mdeadCount >= 0.3f){
                malive = false;
                Debug.Log("Dead");
            }
            //0.3s时间供玩家或者敌人恢复血量，若血量依旧小于0，则设置为死亡状态
        }else{
            mdeadCount = 0;
        }
    }
    public bool Get_Live(){
        return malive;
    }
    private void ElementInformationCheck(){
        if(!gameObject.GetComponent<ElementInformation>()){
            Destroy(this);
        }
    }
    public float GetHeartRate(){
        return mheartPoint/mMaxHeartPoint;
    }

    public void SetAttackHurt(float hurt){
        this_hurt += hurt;
    }

    private void HurtCaculate(){
        mheartPoint -= this_hurt;
        if(mheartPoint < 0){
            mheartPoint = 0;
        }
        //TextManager.SetPosition(new Vector2(gameObject.transform.position.x, gameObject.transform.transform.position.y));
        if(this_hurt != 0){
            BossAttackCount ++;//此代码仅用于Boss战斗时供Boss行为逻辑使用
            if(isBossAttack == true){
                return;
            }
            Debug.Log("set attack hit");
            Instantiate(Resources.Load("Prefabs/Creature/Attack") as GameObject).transform.SetParent(gameObject.transform);
            GameObject bloodEffect = Instantiate(Resources.Load("Effect/BloodEffect") as GameObject);
            bloodEffect.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            TextManager.SetText(this_hurt.ToString());
            if(gameObject.GetComponent<Hero>() != null){
                isHeroAttack = true;
                Debug.Log("Hero Avoid");
                GameObject[] targets = GameObject.FindGameObjectsWithTag(OppositeTag());
                // Ignore collision between the current rigidbody and all targets
                foreach (GameObject target in targets)
                {
                    if(target.GetComponent<Collider2D>()){
                        Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
                    }
                }
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.gray;
            }
            Vector2 position = new Vector2(gameObject.transform.position.x, gameObject.transform.transform.position.y);
            Vector2 cameraPosition = GameObject.Find("Main Camera").GetComponent<Transform>().position;
            TextManager.SetPosition(position-cameraPosition);
            //Debug.Log(new Vector2(gameObject.transform.position.x, gameObject.transform.transform.position.y));
        }
        this_hurt = 0;
    }
    private void HeroAttackAvoid(){
        if(gameObject.GetComponent<Hero>() != null){
            if(isHeroAttack){
                attackHeroTimeConuter += Time.deltaTime;
                if(attackHeroTimeConuter > attackHeroTime){
                    isHeroAttack = false;
                    GameObject[] targets = GameObject.FindGameObjectsWithTag(OppositeTag());
                    // Ignore collision between the current rigidbody and all targets
                    attackHeroTimeConuter = 0f;
                    foreach (GameObject target in targets)
                    {
                        if(target.GetComponent<Collider2D>()){
                            Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), false);
                        }
                    }
                    gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                }
            }
        }
    }
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
    public void Heal(int heal_num){
        mheartPoint += heal_num;
        if(mheartPoint > mMaxHeartPoint){
            mheartPoint = mMaxHeartPoint;
        }
    }
    public void SetMaxHeartPoint(float maxHeartPoint){
        mMaxHeartPoint = maxHeartPoint;
        mheartPoint = mMaxHeartPoint;
        //设置人物最大血量
    }
}
