using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Vector2 inputDirection, inputMouseDirection;
    [SerializeField] private bool inputAutoAttack, inputDash, inputMouseSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnAutoAttack(InputAction.CallbackContext context)
    {
        inputAutoAttack = context.action.triggered;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        inputDash = context.action.triggered;
    }

    public void OnMouseDirection(InputAction.CallbackContext context)
    {
        inputMouseDirection = context.ReadValue<Vector2>();
    }

    public void OnMouseSelect(InputAction.CallbackContext context)
    {
        inputMouseSelect = context.action.triggered;
    }

    public Vector2 InputDirection
    {
        get { return inputDirection; }
        set { inputDirection = value; }
    }

    public bool InputAutoAttack
    {
        get { return inputAutoAttack; }
        set { inputAutoAttack = value; }
    }

    public bool InputDash
    {
        get { return inputDash; }
        set { inputDash = value; }
    }

    public Vector2 InputMouseDirection
    {
        get { return inputMouseDirection; }
        set { inputMouseDirection = value; }
    }

    public bool InputMouseSelect
    {
        get { return inputMouseSelect; }
        set { inputMouseSelect = value; }
    }

}
