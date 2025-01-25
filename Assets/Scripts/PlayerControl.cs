using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private InputSystem inputSystem;

    private GameObject enemies;

    [SerializeField] private GameObject model;
    [SerializeField] private GameObject spawnBullet;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletCurrent;

    private int idBullet = 0;

    private RaycastHit hit;
    
    [SerializeField] private float speed;

    private Vector2 mouseRotation;

    private bool canMove = true;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inputSystem = GetComponentInChildren<InputSystem>();
        enemies = GameObject.FindWithTag("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        DashLogic();
        AnimatorLogic();
        //AutoAttackLogic();
    }

    private void FixedUpdate()
    {
        //MoveLogic();
    }

    private void LateUpdate()
    {
        MoveChar();
    }

    public void MoveLogic()
    {
        if (canMove)
        {
            rb.velocity = new Vector3(inputSystem.InputDirection.x * speed, 0, inputSystem.InputDirection.y * speed);
        }
    }

    public void MoveChar()
    {
        print(inputSystem.InputMouseDirection);
        if (inputSystem.InputMouseDirection != Vector2.zero)
        {
            mouseRotation.x += inputSystem.InputMouseDirection.x;
            mouseRotation.y += inputSystem.InputMouseDirection.y;

            model.transform.rotation = Quaternion.Euler(mouseRotation.y, mouseRotation.x, 0);
        }
    }

    public void DashLogic()
    {

    }

    public void AnimatorLogic()
    {

    }

    public void AutoAttackLogic()
    {
        //obter distancia entre jogador e inimigo
        float minDistance = 5;
        Transform target = null;
        foreach (Transform enemy in enemies.transform)
        {
            float enemyDistance = Vector3.Distance(enemy.position, transform.position);
            Vector3 distance = enemy.position - transform.position;

            print("Dis" + distance);

            float angle = Vector3.Angle(distance, transform.forward);

            Ray ray = new Ray(transform.position, distance);
            bool rayHit = Physics.Raycast(ray, out hit, minDistance);

            if (angle < 45 / 2 && rayHit == true & hit.collider != null)
            {
             //if (enemyDistance < minDistance && enemy.gameObject.activeSelf)
            //{
                minDistance = enemyDistance;
                target = enemy;
            }
        }
        if (target)
        {
            //logica de ataque
            if (canAttack)
            {
                print("Attack");
                StartCoroutine(AttackCooldown(target));
            }
            Debug.DrawLine(transform.position, target.position, Color.green);
        }

    }

    public IEnumerator AttackCooldown(Transform target)
    {
        canAttack = false;
        /*for (int i = 0; i < bullet.Length; i++)
        {
            if (idBullet != i && bullet[idBullet].activeSelf)
                idBullet = i;
        }*/
        //bullet[idBullet].transform.parent = null;
        //bullet[idBullet].SetActive(true);
        bulletCurrent = Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity);
        bulletCurrent.GetComponent<Bullet>().Target = target;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}
