using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private float health, maxHealth = 3f;
    private Rigidbody2D rb;

    Transform target;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y) * speed;
    }
}
