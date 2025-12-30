using UnityEngine;
using UnityEngine.InputSystem;

public class ShopActivator : MonoBehaviour
{
    private bool canOpen;


    // Update is called once per frame
    void Update()
    {
        if (canOpen == true)
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
            {
                if(UIController.instance.theShop.gameObject.activeSelf == false )
                {
                    UIController.instance.theShop.OpenClose();

                    AudioManager.instance.PlaySFX(0);
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canOpen = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canOpen = false;
        }
    }

}
