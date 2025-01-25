using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartAttack();
    }


    public void StartAttack()
    {
        print(targetPos);
        targetPos = new Vector3(target.position.x, 0, target.position.z);
        transform.Translate(targetPos.normalized * 1 * Time.deltaTime);
    }

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
}
