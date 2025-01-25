using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Bubble.BubbleType[] inventory = new Bubble.BubbleType[3];
    private int currentIndex = 0;


    private Rigidbody rb;
    private Animator animator;
    private InputSystem inputSystem;

    private GameObject enemies;

    [SerializeField] private float speed;

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
        AutoAttackLogic();
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    public void MoveLogic()
    {
        rb.velocity = new Vector3(inputSystem.InputDirection.x * speed, 0 , inputSystem.InputDirection.y * speed);
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
                if (enemyDistance < minDistance && enemy.gameObject.activeSelf)
                {
                    minDistance = enemyDistance;
                    target = enemy;
                }
        }
        if (target)
        {
            //logica de ataque
            print("teste");
            Debug.DrawLine(transform.position, target.position, Color.green);
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
    }

    private void CollectBubble(Bubble.BubbleType bubbleType)
    {
        Debug.Log(currentIndex);
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

}
