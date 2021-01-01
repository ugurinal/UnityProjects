using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerJump playerJump = PlayerJump.instance;
        if (playerJump != null)
        {
            playerJump.SetPower(true);
        }
        else
        {
            Debug.LogError("Player Jump instance is null!");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerJump playerJump = PlayerJump.instance;
        if (playerJump != null)
        {
            playerJump.SetPower(false);
        }
        else
        {
            Debug.LogError("Player Jump instance is null!");
        }
    }
}