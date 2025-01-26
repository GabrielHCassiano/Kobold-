using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    [SerializeField] private Animator animatorMain;
    [SerializeField] private Animator animatorEffects;
    [SerializeField] private Animator animatorCopy;

    //public LayerMask whatIsGround, whatIsPlayer;

    private float health = 50;

    private bool canMove = true;

    private bool inHurt = false;

    //public float attackInterval;
    //bool hasAttacked;

    //states
    //public float sightRange, attackRange;
    //public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        //agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
        DeathLogic();
        AnimationsLogic();
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

    public void AnimationsLogic()
    {
        animatorMain.SetFloat("Horizontal", agent.velocity.x);
        animatorMain.SetFloat("Vertical", agent.velocity.y);

        animatorEffects.SetBool("InHurt", inHurt);

        animatorCopy.SetFloat("Horizontal", agent.velocity.x);
        animatorCopy.SetFloat("Vertical", agent.velocity.y);
    }

    public void ResetInHurt()
    {
        inHurt = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            inHurt = true;
            health -= 15;
            other.gameObject.SetActive(false);
        }
    }
}
