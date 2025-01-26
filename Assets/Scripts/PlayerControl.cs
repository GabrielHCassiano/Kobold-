using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Bubble.BubbleType[] inventory = new Bubble.BubbleType[3];
    private int currentIndex = 0;

    private Rigidbody rb;
    [SerializeField] private Animator animatorMain;
    [SerializeField] private Animator animatorEffects;
    [SerializeField] private Animator animatorCopy;

    private InputSystem inputSystem;
    private Knockback knockback;

    private GameObject enemies;

    [SerializeField] private GameObject model;
    [SerializeField] private GameObject spawnBullet;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletCurrent;

    private int idBullet = 0;

    private RaycastHit hit;

    [Header("Status")]

    private int lifeMax = 200;
    [SerializeField] private int lifeCurrent;

    private int manaMax = 100;
    [SerializeField] private int manaCurrent;

    [SerializeField] private float speed;

    private bool canMove = true;
    private bool canAttack = true;

    private bool inHurt = false;

    private Vector3 lookPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponentInChildren<InputSystem>();
        knockback = GetComponent<Knockback>();
        enemies = GameObject.FindWithTag("Enemies");

        lifeCurrent = lifeMax;
        manaCurrent = manaMax;
    }

    // Update is called once per frame
    void Update()
    {
        MoveChar();
        AnimatorLogic();
        AutoAttackLogic();
        Death();

        if (currentIndex == 3)
        {
            Combine();
        }
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    public void MoveLogic()
    {
        if (knockback.KnockbackCountLogic < 0)
        {
            if (canMove)
            {
                rb.velocity = new Vector3(inputSystem.InputDirection.x * speed, 0, inputSystem.InputDirection.y * speed);
            }
        }
        else
        {
            knockback.KnockLogic();
        }

    }

    public void MoveChar()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputSystem.InputMouseDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);

    }

    public void AnimatorLogic()
    {
        animatorMain.SetFloat("Horizontal", rb.velocity.x);
        animatorMain.SetFloat("Vertical", rb.velocity.y);

        animatorEffects.SetBool("InHurt", inHurt);

        animatorCopy.SetFloat("Horizontal", rb.velocity.x);
        animatorCopy.SetFloat("Vertical", rb.velocity.y);
    }

    public void ResetInHurt()
    {
        inHurt = false;
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


            float angle = Vector3.Angle(distance, transform.forward);

            Ray ray = new Ray(transform.position, distance);
            bool rayHit = Physics.Raycast(ray, out hit, 5);

            if (angle < 45 / 2 && rayHit == true && enemyDistance < minDistance && enemy.gameObject.activeSelf && hit.collider != null)
            {
                print(enemy.position);
                minDistance = enemyDistance;
                target = enemy;
                Debug.DrawRay(transform.position, distance, Color.red);
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
            //Debug.DrawLine(transform.position, target.position, Color.green);
        }

    }

    public void Death()
    {
        if (lifeCurrent <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble"))
        {
            Bubble bubble = other.GetComponent<Bubble>();
            if(bubble != null)
            {
                CollectBubble(bubble.bubbleType);

            }
        }

        if (other.CompareTag("EnemyHit"))
        {
            inHurt = true;
            lifeCurrent -= 10;
            knockback.Knocking(other);
        }
    }

    private void CollectBubble(Bubble.BubbleType bubbleType)
    {
       
        if(currentIndex < inventory.Length)
        {
            inventory[currentIndex] = bubbleType; 
            currentIndex++;
        }
        else
        {
            
            inventory[0] = bubbleType;
            currentIndex = 1;
        }
        DebugInventoryLog();
       

    }

    private void DebugInventoryLog()
    {
        string inventoryContent = "";
        for (int i = 0; i < inventory.Length; i++)
        {
            inventoryContent += inventory[i].ToString() + " | ";
        }
        Debug.Log(inventoryContent);
    }


//scripts e logica de magias abaixo

// Funções de cada elemento ou combinação
    void Fire() {
        Debug.Log("Fire!");
    }

    void Air() {
        Debug.Log("Air!");
    }

    void Earth() {
        Debug.Log("Earth!");
    }

    void Water() {
        Debug.Log("Water!");
    }

    void Electric() {
        Debug.Log("Eletricidade!");
    }

    void Lava() {
        Debug.Log("Lava!");
    }

    void Steam() {
        Debug.Log("Vapor!");
    }

    void Dust() {
        Debug.Log("Poeira!");
    }

    void WaterSpout() {
        Debug.Log("Tufão!");
    }

    void Mud() {
        Debug.Log("Lama!");
    }

    void EarthQuake() {
        Debug.Log("Terremoto!");
    }

    void Storm() {
        Debug.Log("Tempestade!");
    }

    void Obsidian() {
        Debug.Log("Obsidiana!");
    }

    void Plant() {
        Debug.Log("Planta!");
    }

    // Método para determinar qual função chamar baseado na combinação
    
    public void Combine()
    {
        // Group the inventory by BubbleType and count occurrences
        var elementCount = inventory.GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());

        // Check for valid combinations
        if (elementCount.ContainsKey(Bubble.BubbleType.Fire) ||
            elementCount.ContainsKey(Bubble.BubbleType.Air) ||
            elementCount.ContainsKey(Bubble.BubbleType.Earth) ||
            elementCount.ContainsKey(Bubble.BubbleType.Water))
        {
            if (elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 3)
            {
                Fire();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 3)
            {
                Air();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Earth) == 3)
            {
                Earth();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Water) == 3)
            {
                Water();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 2 || elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 2 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 1 )
            {
                Electric();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Earth) == 2)
            {
                Lava();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Water) == 2)
            {
                Steam();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Earth) == 2)
            {
                Dust();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Water) == 2)
            {
                WaterSpout();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Earth) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Water) == 2)
            {
                Mud();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Earth) == 1)
            {
                EarthQuake();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Water) == 1)
            {
                Storm();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Fire) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Earth) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Water) == 1)
            {
                Obsidian();
            }
            else if (elementCount.GetValueOrDefault(Bubble.BubbleType.Air) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Earth) == 1 &&
                    elementCount.GetValueOrDefault(Bubble.BubbleType.Water) == 1)
            {
                Plant();
            }
            else
            {
                Debug.Log("Combinação inválida.");
            }
        }
        else
        {
            Debug.Log("Inventário vazio ou elementos incompatíveis.");
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
        bulletCurrent.GetComponent<Bullet>().Target = spawnBullet.transform;
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
}
