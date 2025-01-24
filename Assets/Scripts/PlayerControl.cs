using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private InputSystem inputSystem;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inputSystem = GetComponentInChildren<InputSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        DashLogic();
        AnimatorLogic();
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
}
