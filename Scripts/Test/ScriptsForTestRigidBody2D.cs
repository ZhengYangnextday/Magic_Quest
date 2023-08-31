using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsForTestRigidBody2D : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 direction;
    Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        float result = CalculateAngle(Vector2.right, rb.velocity);
        result = rb.velocity.y > 0.0f?result:360f - result;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, result);
    }

    float CalculateAngle(Vector2 a, Vector2 b){
        float sum = a.magnitude * b.magnitude;
        float cross = a[0] * b[0] + a[1] * b[1];
        return 360 * (Mathf.Acos(cross/sum)/ (2 * Mathf.PI)); 
    }
}
