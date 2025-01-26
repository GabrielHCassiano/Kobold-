using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackCount;
    [SerializeField] private float knockbackTime;
    [SerializeField] private bool isKnock;

    [SerializeField] private Vector3 difference;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        knockbackForce = 5;
        knockbackCount = -0.1f;
        knockbackTime = 0.3f;
        rb = GetComponent<Rigidbody>();
    }

    public float KnockbackCountLogic
    {
        get { return knockbackCount; }
        set { knockbackCount = value; }
    }

    public void KnockLogic()
    {
        if (isKnock == true)
        {
            rb.velocity = difference;
        }
        knockbackCount -= Time.deltaTime;
    }

    public void Knocking(Collider collision)
    {
        knockbackCount = knockbackTime;

        if (collision != null)
        {
            difference = rb.transform.position - collision.transform.position;
            difference = difference.normalized * knockbackForce;
            isKnock = true;
        }
        else
        {
            isKnock = false;
        }

    }
}
