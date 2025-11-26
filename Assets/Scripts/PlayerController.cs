using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public InputActionReference moveInput, actionInput;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = new Vector2(moveSpeed, 0f);
         rb.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;



        if(rb.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); 
        }
        else if(rb.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }

        if(actionInput.action.WasPressedThisFrame())
        {
            useTool();
        }


        animator.SetFloat("speed", rb.linearVelocity.magnitude);
    }

    void useTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        block.PloughSoil();
    }

}
