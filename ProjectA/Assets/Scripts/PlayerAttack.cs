using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public Rigidbody2D rb; 
    public PlayerInputActions playerControls;

    private InputAction fire;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    
    private void OnEnable()
    {

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    void Update()
    {
        
        
    }

    private void Fire(InputAction.CallbackContext context)
    {
        anim.SetTrigger("attack");
    }

    
}
