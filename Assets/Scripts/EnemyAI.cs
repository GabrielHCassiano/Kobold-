using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    //public LayerMask whatIsGround, whatIsPlayer;

    private float health = 5;

    private bool canMove = true;

    //public float attackInterval;
    //bool hasAttacked;

    //states
    //public float sightRange, attackRange;
    //public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
<<<<<<< HEAD:Assets/Scripts/EnemyAI.cs
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
=======
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
>>>>>>> main:Assets/EnemyAI.cs

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
<<<<<<< HEAD:Assets/Scripts/EnemyAI.cs
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
=======
        DeathLogic();
>>>>>>> main:Assets/EnemyAI.cs
    }

    public void FixedUpdate()
    {
<<<<<<< HEAD:Assets/Scripts/EnemyAI.cs
        health -= damage;

        if(health <= 0) Invoke(nameof(DestroyEnemy), .5f);
        
=======
        MoveLogic();
>>>>>>> main:Assets/EnemyAI.cs
    }

    public void MoveLogic()
    {
<<<<<<< HEAD:Assets/Scripts/EnemyAI.cs
        Destroy(gameObject);
=======
        if (canMove)
        {
            agent.SetDestination(player.transform.position);
        }
        else
            agent.velocity = Vector3.zero;
>>>>>>> main:Assets/EnemyAI.cs
    }

    public void DeathLogic()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= 1;
            other.gameObject.SetActive(false);
        }
    }
}