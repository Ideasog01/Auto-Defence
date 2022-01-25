using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    private GameObject cursor;

    private PlayerController _playerController;

    private void Awake()
    {
        cursor = GameObject.Find("Cursor");
        _playerController = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();

        
    }

    private void Update()
    {
        //_playerController.RotatePlayer(cursor.transform.position);
    }

    public void MoveCursor(Vector2 mousePosition)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mousePosition.x > 0 && cursor.transform.position.x < screenWidth)
        {
            cursor.transform.position += new Vector3(mousePosition.x, 0) * 100;
        }
        else if (mousePosition.x < 0 && cursor.transform.position.x > 0)
        {
            cursor.transform.position += new Vector3(mousePosition.x, 0) * 100;
        }

        if (mousePosition.y > 0 && cursor.transform.position.y < screenHeight)
        {
            cursor.transform.position += new Vector3(0, mousePosition.y) * 100;
        }
        else if (mousePosition.y < 0 && cursor.transform.position.y > 0)
        {
            cursor.transform.position += new Vector3(0, mousePosition.y) * 100;
        }
    }
   
}
