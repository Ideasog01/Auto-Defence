using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceManager : MonoBehaviour
{
    public static bool joinScreenActive;

    public static int playerCount;

    private MenuManager _menuManager;

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = new PlayerInput();
        _menuManager = this.GetComponent<MenuManager>();

        InputSystem.onDeviceChange += (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("New device added: " + device);
                    break;

                case InputDeviceChange.Removed:
                    Debug.Log("Device removed: " + device);
                    break;
            }
        };

        //_playerInput.Menu.Join.performed += ctx => PlayerJoin();
    }

    private void PlayerJoin()
    {
        if(playerCount < 4)
        {
            if(playerCount == 0)
            {
                _menuManager.DisplayPlayer(0, "Waiting");
            }

            playerCount += 1;
        }
    }

    

}
