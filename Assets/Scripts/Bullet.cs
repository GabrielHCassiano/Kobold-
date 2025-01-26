using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 direct;

    // Start is called before the first frame update
    void Start()
    {
        direct = target.forward;
    }

    // Update is called once per frame
    void Update()
    {
        StartAttack();
    }


    public void StartAttack()
    {
        transform.Translate(direct * 5 * Time.deltaTime);
    }

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
}
