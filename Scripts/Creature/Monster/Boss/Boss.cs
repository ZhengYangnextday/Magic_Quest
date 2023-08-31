using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject LeftUP;
    private GameObject MiddleUP;
    private GameObject RightUP;
    private GameObject LeftDown;
    private GameObject MiddleDown;
    private GameObject RightDown;
    public GameObject BoundObject;
    public GameObject targetObject;
    private Rigidbody2D rb;
    public Bounds bound;
    public int now_state;
    private bool BattleOn = false;
    private float speed = 2.5f;
    private Vector3 initDirection;
    private Vector2 residualPosition;
    private float attack_cool_time = 10f;
    private float attack_cool_time_cnt = 0f;
    public bool isattack = false;

    private float summon_cool_time = 3f;
    private float summon_cool_time_cnt = 0f;
    // private bool issummon;

    private float magic_cool_time = 2f;
    private float magic_cool_time_cnt;
    private Animator animator;
    public float change_time = 0f;
    // private bool ismagic;
    private enum state{
        idle,
        walk,
        attack,
        summon,
        magic,
        trans
    }
    private int BattleWay = 0;//初始化后变为0
    public bool IsDie = false;
    public GameObject canvas;
    void Awake(){
        initDirection = transform.localScale;
        LeftUP = GameObject.Find("LeftUP").gameObject;
        MiddleUP = GameObject.Find("MiddleUP").gameObject;
        RightUP = GameObject.Find("RightUP").gameObject;
        LeftDown = GameObject.Find("LeftDown").gameObject;
        MiddleDown = GameObject.Find("MiddleDown").gameObject;
        RightDown = GameObject.Find("RightDown").gameObject;
        bound = BoundObject.GetComponent<Collider2D>().bounds;
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<CreatureInformation>().BossAttackCount = 1;
        now_state = 0;
    }
    void Start()
    {
        gameObject.GetComponent<CreatureInformation>().BossAttackCount = 1;
        //gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ResetChangetime(){

    }
    void Update()
    {
        if(rb.sharedMaterial){
            rb.sharedMaterial.friction = 1.0f;
        }
        if(IsDie == true){
            canvas.GetComponent<BossUI>().enabled = false;
            Destroy(gameObject);
        }
        SearchTarget();
        if (targetObject == null){
            //now_state = 0;
            canvas.GetComponent<BossUI>().enabled = false;
            BattleOn = false;
            BattleWay = 0;
            transform.position = MiddleDown.transform.position;
            return;
        }
        canvas.GetComponent<BossUI>().enabled = true;
        BattleOn = true;
        residualPosition = (targetObject.transform.position - gameObject.transform.position);
        int type;
        if(gameObject.GetComponent<CreatureInformation>().BossAttackCount % 6 == 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
            now_state = (int)state.trans;
            gameObject.GetComponent<CreatureInformation>().BossAttackCount = 1;
            type = Random.Range(0, 3) + 1;
            if(Random.Range(0,100) > 50){
                type = Random.Range(0, 3) + 1;
                MagicCreate.CreateHealMagic(4, type, gameObject);
            }else if(Random.Range(0,100) < 50){
                type = Random.Range(0, 3) + 1;
                MagicCreate.CreateDefendMagic(4, type, gameObject);
            }
            BattleWay = (BattleWay + 1 ) % 3;
            int where = Random.Range(0,3);
            Debug.Log(2);
            if(BattleWay == 0){
                switch(where){
                    case 0:
                        transform.position = LeftDown.transform.position;
                        break;
                    case 1:
                        transform.position = MiddleDown.transform.position;
                        break;
                    case 2:
                        transform.position = RightDown.transform.position;
                        break;
                    default:
                        transform.position = MiddleDown.transform.position;
                        break;
                }
            }else{
                switch(where){
                    case 0:
                        transform.position = LeftUP.transform.position;
                        break;
                    case 1:
                        transform.position = MiddleUP.transform.position;
                        break;
                    case 2:
                        transform.position = RightUP.transform.position;
                        break;
                    default:
                        transform.position = MiddleUP.transform.position;
                        break;
                }
            }
        }

        switch(BattleWay){
            case 0:
                Attack();
                break;
            case 1:
                
                Magic();
                break;
            case 2:
                Summon();
                break;
        }
        animator.SetInteger("now_state", now_state);
        animator.SetBool("Live", gameObject.GetComponent<CreatureInformation>().Get_Live());
        //Debug.Log(gameObject.GetComponent<CreatureInformation>().Get_Live() == true);
    }
    void OnDrawGizmosSelected()
    {
        // 在场景视图中绘制搜索范围的边框
        Vector2 bounds_offset = BoundObject.GetComponent<Collider2D>().offset;
        Vector2 bounds_size = BoundObject.GetComponent<Collider2D>().bounds.size;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(bound.center, bound.extents * 2);
    }
    private void SearchTarget()
    {
        Vector2 bounds_offset = BoundObject.GetComponent<Collider2D>().offset;
        Vector2 bounds_size = BoundObject.GetComponent<Collider2D>().bounds.size;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(bound.center, bound.extents * 2, 0);
        //Collider2D[] hitColliders = BoundObject.GetComponent<BoundCheck>().In;
        float minDistance = Mathf.Infinity;
        GameObject closestObject = null;
        BattleOn = false;
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "positive")
            {
                BattleOn = true;
                CreatureInformation targetComponent = hitCollider.gameObject.GetComponent<CreatureInformation>();
                if (targetComponent != null)
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestObject = hitCollider.gameObject;
                    }
                }
            }
        }
        if(BattleOn == false){
            gameObject.GetComponent<CreatureInformation>().mheartPoint = gameObject.GetComponent<CreatureInformation>().mMaxHeartPoint;
            now_state = (int)state.idle;
            transform.position = MiddleDown.transform.position;
        }
        // if (closestObject == null)
        // {
        //     //TODO avoid edge
        //     rb.velocity = new Vector2(Random.Range(-1,2),0) * speed;
        // }
        targetObject = closestObject;
    }

    private void Attack(){
        if(targetObject == null){return;}
        if(now_state != (int)state.attack){
            rb.velocity = new Vector2(residualPosition[0] > 0?1 * speed : -1 * speed, rb.velocity.y);
            transform.localScale = new Vector3((residualPosition[0] > 0?-1 : 1) * initDirection[0], 1 * initDirection[1], 1 * initDirection[2]);
            now_state = (int)state.walk;
            //Debug.Log(now_state);
            // if(attack_cool_time_cnt == 0){
            //     now_state = (int)state.attack;
            //     animator.SetInteger("now_state", now_state);
            //     Debug.Log("attack");
            //     isattack = true;
            // }
        }
        // if(isattack == true){
        //     //now_state = (int)state.attack;
        //     attack_cool_time_cnt += Time.deltaTime;
        //     if(attack_cool_time_cnt > attack_cool_time){
        //         attack_cool_time_cnt = 0;
        //         isattack = false;
        //     }
        // }
    }

    private void Summon(){
        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x = 0f;
        rb.velocity = currentVelocity;
        if(summon_cool_time_cnt == 0 && now_state == 0){
            int id = Random.Range(1, BookSystem.GetCardIndexNum());
            GameObject Summon_Monster = Instantiate(BookSystem.MonsterIndexQuest[id]);
            Summon_Monster.tag = gameObject.tag;
            Summon_Monster.transform.position = gameObject.transform.position;
            transform.localScale = new Vector3((residualPosition[0] > 0?-1 : 1) * initDirection[0], 1 * initDirection[1], 1 * initDirection[2]);
            Debug.Log("Summon");
        }
        summon_cool_time_cnt += Time.deltaTime;
        if(summon_cool_time_cnt > summon_cool_time){
            now_state = 3;
            summon_cool_time_cnt = 0;
        }
    }

    private void Magic(){
        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x = 0f;
        rb.velocity = currentVelocity;
        if(magic_cool_time_cnt == 0 && now_state == 0){
            int type = Random.Range(0, 3) + 1;
            //Debug.Log(type);
            float x = Random.Range(-1,1);
            float y = Random.Range(-1,1);
            Vector2 attackDirection = (residualPosition.normalized + new Vector2(x,y) * 0.1f).normalized;
            transform.localScale = new Vector3((residualPosition[0] > 0?-1 : 1) * initDirection[0], 1 * initDirection[1], 1 * initDirection[2]);
            MagicCreate.CreateAttackMagic(4, type, attackDirection, gameObject.transform.position, tag = gameObject.tag);
            Debug.Log("Magic");
            if(Random.Range(0,100) > 70){
                type = Random.Range(0, 3) + 1;
                MagicCreate.CreateHealMagic(4, type, gameObject);
            }
        }
        magic_cool_time_cnt += Time.deltaTime;
        if(magic_cool_time_cnt > magic_cool_time){
            now_state = 3;
            magic_cool_time_cnt = 0;
        }
    }
    public void Set_State(int state){
        now_state = state;
    }
}
