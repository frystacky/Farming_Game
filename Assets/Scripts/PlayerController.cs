using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public InputActionReference moveInput, actionInput;

    public Animator animator;

    public enum ToolType
    {
        plought,
        wateringCan,
        seeds,
        basket
    }

    public ToolType currentTool;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);
    }

    // Update is called once per frame
    void Update()
    {
        //moving logic
        rb.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;
        if(rb.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); 
        }
        else if(rb.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }

        ///tools
        bool hasSwitchTool = false;

        if(Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;

            if((int)currentTool >= 4)
            {
                currentTool = ToolType.plought;
            }

            hasSwitchTool = true;
        }

        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = ToolType.plought;
            hasSwitchTool = true;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = ToolType.wateringCan;
            hasSwitchTool = true;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = ToolType.seeds;
            hasSwitchTool = true;
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentTool = ToolType.basket;
            hasSwitchTool = true;
        }

        if (!hasSwitchTool) 
        {
            UIController.instance.SwitchTool((int)currentTool);
        }

        if (actionInput.action.WasPressedThisFrame())
        {
            useTool();
        }


        animator.SetFloat("speed", rb.linearVelocity.magnitude);
    }

    void useTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        if (block != null)
        {
            switch (currentTool)
            {
                case ToolType.plought:

                    block.PloughSoil();

                    break;
                case ToolType.wateringCan:


                    break;
                case ToolType.seeds:


                    break;
                case ToolType.basket:


                    break;
            }

        }

    }


}
