using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : MonoBehaviour
{
    public string targetTag;
    public float searchRadius = 10f;
    public GameObject targetObject;
    private Rigidbody2D rb;
    public GameObject test_hero;
    public float speed = 10f;
    private bool direction;

    private float timeStamp;
    private float timeGap = 1f;
    private Vector2 size;
    void Start()
    {
        timeStamp = Time.time;

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 4;
        //rb.sharedMaterial.friction = 0.5f;
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
        size = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        SearchTarget();
        // ChangeDirection();
        // if (gameObject.GetComponent<CreatureInformation>().Get_Live() == false)
        // {
        //     Destroy(gameObject);
        // }
        if (Time.time - timeStamp< timeGap){
            return;
        }
        if (Time.time - timeStamp>= timeGap){
            timeStamp = Time.time;
            //TODO attack with noise per second
            Vector2 residualPosition;
            if(targetObject){
                residualPosition = targetObject.transform.position - gameObject.transform.position;
            }else{
                residualPosition = Vector2.zero;
            }
            int type;
            int rank;
            if(targetObject){
                transform.localScale = new Vector2((residualPosition.x > 0?-1:1) * size.x,size.y);
                float x = Random.Range(-1,1);
                float y = Random.Range(-1,1);
                
                Vector2 attackDirection = (residualPosition.normalized + new Vector2(x,y) * 0.1f).normalized;
                type = Random.Range(0,3) + 1;
                rank = Random.Range(2,5);
                //Debug.Log(type);
                MagicCreate.CreateAttackMagic(rank, type, attackDirection, gameObject.transform.position, tag = gameObject.tag);
                //! create magic at attackDirection
                //TODO randomly heal
            }
            type = Random.Range(0,3) + 1;
            rank = Random.Range(0,3);
            float heal = Random.Range(0,100);
            if (heal>=99){
                MagicCreate.CreateHealMagic(rank, type, gameObject);
                //! self-heal D
            }
            //TODO randomly defend
            type = Random.Range(0,3) + 1;
            rank = Random.Range(0,3);
            float defend = Random.Range(0,100);
            if (heal>=99){
                MagicCreate.CreateDefendMagic(rank, type, gameObject);
                //! defend D
                //! random element
                //! int type = Random.Range(0,3);
            }
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

        targetObject = closestObject;
    }
}
