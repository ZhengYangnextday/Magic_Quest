using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetTag;
    public float searchRadius = 15f;
    public GameObject targetObject;
    private Rigidbody2D rb;
    public GameObject test_hero;
    public float speed = 3f;
    private float jumpSpeed = 2f; // ! 跳跃初速度
    private bool direction;
    void Start()
    {
        coolDownTimeStart = Time.time;
        // rushTimeStart = Time.time;
        
        jumpTimeStart = Time.time;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        rb.freezeRotation = true;
        if (gameObject.tag == "positive")
        {
            targetTag = "negative";
        }
        else if (gameObject.tag == "negative")
        {
            targetTag = "positive";
        }
        else
        {
            Debug.Log("Wrong tag");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (gameObject.GetComponent<CreatureInformation>().Get_Live() == false)
        // {
        //     Destroy(gameObject);
        // }
        SearchTarget();
        if (targetObject == null){return;}
        residualPositionX = (targetObject.transform.position - gameObject.transform.position)[0];
        // SearchTarget();
        
        TrackNAttack();
        //Debug.Log(Random.Range(0, 100));
        //rb.velocity = new Vector2(rushDirectionX, rb.velocity.y);
        if (coolDownStatus&& Time.time>coolDownTimeStart + coolDownTime){
            rushStatus = true;
            rushTimeStart = Time.time;
            switch((int)gameObject.GetComponent<ElementInformation>().GetElementType()){
                case (int)ElementInformation.ElementIndex.e_fire:
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color (0.8f,0.1f,0.1f,1f);
                    break;
                case (int)ElementInformation.ElementIndex.e_water:
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color (0.1f,0.1f,0.8f,1f);
                    break;
                case (int)ElementInformation.ElementIndex.e_wood:
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color (0.1f,0.8f,0.1f,1f);
                    break;
                default:
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color (0.4f,0.4f,0.4f,1f);
                    break; 
            }
        }
        if (rushStatus){
            coolDownStatus = false;
            Debug.LogWarning(Time.time < rushTimeStart+rushTime);
            if (Time.time >= rushTimeStart+rushTime){
                rushStatus = false;
                coolDownStatus = true;
                coolDownTimeStart = Time.time;
                
            }
            if (Time.time < rushTimeStart+rushTime){
                rushDirectionX = (residualPositionX > 0 ? 1 : -1) * speed * 1.5f;
            }

        }
        if (coolDownStatus){
            rushDirectionX = (residualPositionX > 0 ? 1 : -1) * speed / 3f;
        }
        if (!coolDownStatus && !rushStatus){
            rushDirectionX = (residualPositionX > 0 ? 1 : -1) * speed;
        }

        if (Time.time >= jumpTimeStart + 1f)
        {
            if (Random.Range(0, 10) >= 9)
            {
                Jump();
                Debug.LogWarning("jump!");

            }
            jumpTimeStart = Time.time;
        }
        // Debug.LogWarning(coolDownTimeStart);

        // ChangeDirection();
        //TODO random movement
        rb.velocity = new Vector2(rushDirectionX,rb.velocity.y);

    }

    private void SearchTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);

        float minDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (Collider2D hitCollider in hitColliders)
        {

            if (hitCollider.gameObject.tag == targetTag)
            {
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

        // if (closestObject == null)
        // {
        //     //TODO avoid edge
        //     rb.velocity = new Vector2(Random.Range(-1,2),0) * speed;
        // }
        targetObject = closestObject;
    }

    private float coolDownTime = 3f;
    private float coolDownTimeStart;
    private float rushTime = 0.4f;
    private float rushTimeStart;
    private bool rushStatus = false;
    private bool coolDownStatus = false;
    private float rushDirectionX;
    private float residualPositionX;
    private void TrackNAttack()
    {
        //TODO random movement
        if (targetObject == null)
        {
            //rb.velocity = new Vector2(Random.Range(-1, 2), 0) * speed;
            return;
        }
        //! targetObject != null

        // float residualPositionX = (targetObject.transform.position - gameObject.transform.position)[0];
        //TODO track
        // if (residualPositionX > 0 ? true : false)
        // {
        //     rb.velocity = new Vector2((residualPositionX > 0 ? 1 : -1) * speed, rb.velocity.y);
        // }
        // else
        // {
        //     rb.velocity = new Vector2(-speed, rb.velocity.y);
        // }
        //TODO attack
        if (Mathf.Abs(residualPositionX) > 10f)
        {
            coolDownStatus = false;
            coolDownTimeStart = Time.time;
            rb.velocity = new Vector2((residualPositionX > 0 ? 1 : -1) * speed, rb.velocity.y);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color (1f,1f,1f,1f);
        }
        if (Mathf.Abs(residualPositionX) <= 10f && !rushStatus)
        {
            coolDownStatus = true;
            //Debug.LogWarning("蓄力");
            rb.velocity = new Vector2((residualPositionX > 0 ? 1 : -1) * speed / 4f, rb.velocity.y);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1-(Time.time - coolDownTimeStart) / coolDownTime, 1-(Time.time - coolDownTimeStart) / coolDownTime, 1-(Time.time - coolDownTimeStart) / coolDownTime, 1f);
            //Debug.LogWarning(this.gameObject.GetComponent<SpriteRenderer>().color);
            // if (coolDownStatus){}
            
        }
        // if (Time.time < rushTimeStart+rushTime)
        // {
        //         // rushTimeStart = Time.time;//TODO rush
        //         rushDirectionX = (residualPositionX > 0 ? 1 : -1) * speed * 3f;
        // }
        // if (Time.time >= rushTimeStart+rushTime)
        // {
        //         coolDownTimeStart = Time.time;//TODO rush
        //         rushDirectionX = (residualPositionX > 0 ? 1 : -1) * speed;
        //         this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        // }
    }

    private float jumpTimeStart;
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpSpeed);
    }

}

