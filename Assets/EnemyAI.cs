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
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
        DeathLogic();
    }

    public void FixedUpdate()
    {
        MoveLogic();
    }

    public void MoveLogic()
    {
        if (canMove)
        {
            agent.SetDestination(player.transform.position);
        }
        else
            agent.velocity = Vector3.zero;
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