using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public BlasterPickup nearBlaster;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Blaster currentBlaster;

    [SerializeField]
    private float playerSpeed = 10;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private GameObject[] blasterPrefabs;

    [SerializeField]
    private int playerMaxHealth;

    private int playerHealth;

    private CharacterController _playerController;

    private float _turnVelocity;

    [SerializeField]
    private Camera _playerCam;

    private GameObject _focusObject;

    [SerializeField]
    private PlayerInterface _playerInterface;

    private SpawnManager _spawnManager;

    private Vector3 movementInput;

    private float rotationInputX;

    private GameManager _gameManager;

    private bool _isDead;

    private void Awake()
    {
        _playerController = this.GetComponent<CharacterController>();
        _spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _gameManager.PlayerJoined(this.transform.parent.gameObject);

        _focusObject = this.transform.GetChild(2).gameObject;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerHealth = playerMaxHealth;
    }

    public void DamagePlayer(int amount)
    {
        if(!_isDead)
        {
            playerHealth -= amount;
            _playerInterface.DisplayPlayerHealth(playerMaxHealth, playerHealth);

            if (playerHealth <= 0)
            {
                _gameManager.PlayerDefeated();

                this.gameObject.tag = "Untagged";

                _isDead = true;
            } 
        }
    }

    public void RevivePlayer()
    {
        playerHealth = playerMaxHealth;
        this.gameObject.tag = "Player";
        _isDead = false;
        _playerInterface.DisplayPlayerHealth(playerMaxHealth, playerHealth);
        _gameManager.PlayerRevived();
    }

    public void RotatePlayerMouse(InputAction.CallbackContext context)
    {
        rotationInputX = context.ReadValue<Vector2>().x;
    }

    public void RotatePlayerGamePad(InputAction.CallbackContext context)
    {
        rotationInputX = context.ReadValue<Vector2>().x * 10;
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if(currentBlaster != null && context.performed && playerHealth > 0)
        {
            currentBlaster.ReloadBlaster();
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(nearBlaster != null && context.performed && playerHealth > 0)
        {
            BlasterType priorBlaster = currentBlaster.blasterType;

            //Equip Blaster Pickup
            currentBlaster.ChangeBlaster(nearBlaster.blasterType);

            //Replace Blaster Pickup with Prior Blaster Type
            nearBlaster.ChangeBlaster(priorBlaster);

            nearBlaster.transform.position = currentBlaster.transform.position;
            nearBlaster.transform.rotation = currentBlaster.transform.rotation;

            nearBlaster.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2, 2), 4, Random.Range(-2, 2)) * 2, ForceMode.Impulse);

            nearBlaster = null;
        }
    }

    public void PrimaryFire(InputAction.CallbackContext context)
    {
        if(currentBlaster != null && context.performed && playerHealth > 0)
        {
            currentBlaster.PrimaryFire();
        }

        Cursor.visible = false;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        movementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);

        //float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnVelocity, turnSpeed);

        //this.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void Update()
    {
        if(_spawnManager.gameMode.gameInProgress && playerHealth > 0)
        {
            _playerController.Move(movementInput * playerSpeed * Time.deltaTime);
            this.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotationInputX * 50);
        }
        
    }
}
