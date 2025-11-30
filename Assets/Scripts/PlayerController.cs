using UnityEditor.ShaderGraph.Internal;
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

    public float toolWaitTime = .5f;
    private float toolWaitCounter;

    public Transform toolIndicator;
    public float toolRange = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);
    }

    // Update is called once per frame
    void Update()
    {
        if (toolWaitCounter > 0)
        {
            toolWaitCounter -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            //moving logic
            rb.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;
            if (rb.linearVelocity.x < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.linearVelocity.x > 0f)
            {
                transform.localScale = Vector3.one;
            }

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

        //grabs the mouse cord and the tool indictor icon follows it
        toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);

        //check the range of tool indictor icon and limits it to tool range pos
        if(Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
        {
            Vector2 direction = toolIndicator.position - transform.position;
            direction = direction.normalized * toolRange;
            toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f);
        }

        toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x) + .5f,
            Mathf.FloorToInt(toolIndicator.position.y) +.5f,
            0f);
    }

    void useTool()
    {
        GrowBlock block = null;

        //block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        block = GridController.instance.GetBlock(toolIndicator.position.x -.5f, toolIndicator.position.y -.5f);

        toolWaitCounter = toolWaitTime;

        if (block != null)
        {
            switch (currentTool)
            {
                case ToolType.plought:

                    block.PloughSoil();

                    animator.SetTrigger("usePlough");

                    break;
                case ToolType.wateringCan:

                    block.WaterSoil();

                    animator.SetTrigger("useWateringCan");

                    break;
                case ToolType.seeds:

                    block.PlantCrop();

                    break;
                case ToolType.basket:

                    block.HarvestCrop();

                    break;
            }

        }

    }


}
