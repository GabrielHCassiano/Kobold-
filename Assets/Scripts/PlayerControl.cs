using System.Collections;
using System.Linq;
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



}
