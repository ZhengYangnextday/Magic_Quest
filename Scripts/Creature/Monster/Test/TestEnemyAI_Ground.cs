using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAI_Ground : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetTag;
    public float searchRadius = 10f;
    public GameObject targetObject;
    private Rigidbody2D rb;
    public GameObject test_hero;
    public float speed = 10f;
    private bool direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        if(gameObject.tag == "positive"){
            targetTag = "negative";
        }else if(gameObject.tag == "negative"){
            targetTag = "positive";
        }else{
            Debug.Log("Wrong tag");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SearchTarget();
        ChangeDirection();
        if(gameObject.GetComponent<CreatureInformation>().Get_Live() == false){
            Destroy(gameObject);
        }
    }

    private void SearchTarget(){
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

        if (closestObject != null)
        {
            //Debug.Log("The closest object is " + closestObject.name + " at a distance of " + minDistance);
        }
        targetObject = closestObject;
    }

    private void ChangeDirection(){
        if(targetObject != null){
            if((targetObject.transform.position - gameObject.transform.position)[0] > 0? true : false){
                rb.velocity = new Vector2(1,0) * speed;
            }else{
                rb.velocity = new Vector2(-1,0) * speed;
            }
        }else{
            rb.velocity = Vector2.zero;
        }
    }
}
