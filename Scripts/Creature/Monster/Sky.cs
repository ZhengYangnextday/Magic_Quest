using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetTag;
    private float searchRadius = 10f;
    public GameObject targetObject;
    private Rigidbody2D rb;
    public GameObject test_hero;
    private float speed = 3f;
    private bool direction;
    public float jumpTimeStart;
    void Start()
    {
        // coolDownTimeStart = Time.time;
        // rushTimeStart = Time.time;
        jumpTimeStart = Time.time;

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 0;
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

        rb.gravityScale = 0;
        // if (gameObject.GetComponent<CreatureInformation>().Get_Live() == false)
        // {
        //     Destroy(gameObject);
        // }
        GameObject formerTarget = targetObject;
        SearchTarget();

        if (targetObject == null)
        {
            //Debug.Log("Target: null");
            // rb.velocity = new Vector2(Random.Range(-1, 2), 0) * speed;

        }
        if (targetObject != null)
        {
            Vector2 residualPosition = targetObject.transform.position - gameObject.transform.position;
            
            if (residualPosition.x < 3f){
                rb.velocity = Vector2.Lerp(rb.velocity, residualPosition.normalized * speed, 0.08f);
            }
            else {
                rb.velocity = new Vector2((residualPosition.x>0?1:-1)*speed,5f);

                if (residualPosition.y >= 3f||targetObject.transform.position.y>14f){
                    rb.velocity = new Vector2((residualPosition.x > 0? 1 : -1) * speed, -1f);
                }
                else{
                    rb.velocity = new Vector2((residualPosition.x > 0? 1 : -1) * speed, 1f);
                }
            }
            
            // if (formerTarget == null){
            //     rb.velocity = new Vector2(0f,1f) * speed;
            // }
            // Debug.Log("Target: " + targetObject.name);
            // // Vector2 residualPosition = targetObject.transform.position - gameObject.transform.position;
            // //TODO track
            // rb.velocity = Vector2.Lerp(rb.velocity, residualPosition.normalized * speed, 0.05f);
        }

        //Debug.Log(Random.Range(0, 100));

        if (Time.time >= jumpTimeStart + 1f)
        {
            if (Random.Range(0, 10) >= 9)
            {
                rb.velocity = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized * speed;
                Debug.LogWarning("jump!");

            }
            jumpTimeStart = Time.time;
        }
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


        
        //! targetObject != null



    }

    // private float coolDownTime = 3f;
    // private float coolDownTimeStart;
    // private float rushTime = 0.5f;
    // private float rushTimeStart;
    // private Vector2 rushDirection;
    // private void Track()
    // {
    //     //TODO random movement
    //     if (targetObject == null)
    //     {
    //         rb.velocity = new Vector2(Random.Range(-1, 2), 0) * speed;
    //         return;
    //     }
    //     //! targetObject != null

    //     Vector2 residualPosition = targetObject.transform.position - gameObject.transform.position;
    //     //TODO track
    //     rb.velocity = Vector2.Lerp(rb.velocity, residualPosition.normalized * speed, 0.0005f);
    // }

}
