using UnityEngine;
using System.Collections;


public class UIDrag : MonoBehaviour
{
    private float offsetX;
    private float offsetY;

    private Vector2 mousePos;

    public void BeginDrag()
    {
        mousePos = InputManager.Instance.GeneralInputs.MousePosition;

        offsetX = transform.position.x - mousePos.x;
        offsetY = transform.position.y - mousePos.y;
    }

    public void OnDrag()
    {
        mousePos = InputManager.Instance.GeneralInputs.MousePosition;
        transform.position = new Vector3(offsetX + mousePos.x, offsetY + mousePos.y, transform.position.z);
    }
}