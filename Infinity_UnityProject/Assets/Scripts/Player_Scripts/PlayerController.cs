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

    private Vector2 _direction;

    private PlayerInput _playerInput;

    private Camera _playerCam;

    private GameObject _focusObject;

    private PlayerInterface _playerInterface;

    private Vector2 mouseRotation;
    private Vector2 rightStickRotation;

    private CursorController cursorHandler;

    private SpawnManager _spawnManager;

    private void Awake()
    {
        _playerController = this.GetComponent<CharacterController>();
        _playerInterface = this.GetComponent<PlayerInterface>();
        _playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cursorHandler = GameObject.Find("GamepadCursor").GetComponent<CursorController>();
        _spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();

        _focusObject = this.transform.GetChild(2).gameObject;

        _playerInput = new PlayerInput();
        _playerInput.Gameplay.Movement.performed += ctx => _direction = ctx.ReadValue<Vector2>();
        _playerInput.Gameplay.PrimaryFire.performed += ctx => PrimaryFire();
        _playerInput.Gameplay.Reload.performed += ctx => Reload();

        _playerInput.Gameplay.MouseX.performed += ctx => mouseRotation = ctx.ReadValue<Vector2>();
        _playerInput.Gameplay.RightStick.performed += ctx => rightStickRotation = ctx.ReadValue<Vector2>();

        _playerInput.Gameplay.Interact.performed += ctx => Interact();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerHealth = playerMaxHealth;
    }

    private void Update()
    {
        if(_spawnManager.gameMode.gameInProgress)
        {
            MovePlayer();
            cursorHandler.MoveCursor(mouseRotation);
            RotatePlayer();
        }
    }

    public void DamagePlayer(int amount)
    {
        playerHealth -= amount;
        _playerInterface.DisplayPlayerHealth(playerMaxHealth, playerHealth);

        if(playerHealth <= 0)
        {
            _spawnManager.StopGame();
            _playerInterface.DisplayConditionMessage(1);
        }
    }

    public void RotatePlayer()
    {
        this.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * mouseRotation.x * 50);
        this.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rightStickRotation.x * 150);
    }

    private void Reload()
    {
        if(currentBlaster != null)
        {
            currentBlaster.ReloadBlaster();
        }
    }

    private void Interact()
    {
        if(nearBlaster != null)
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

    private void PrimaryFire()
    {
        if(currentBlaster != null)
        {
            currentBlaster.PrimaryFire();
        }

        Cursor.visible = false;
    }

    private void MovePlayer()
    {
        Vector3 movementDirection = new Vector3(_direction.x, 0, _direction.y);

        if (movementDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnVelocity, turnSpeed);

            //this.transform.rotation = Quaternion.Euler(0, angle, 0);
            _playerController.transform.position += movementDirection * playerSpeed * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        if(_playerInput != null)
        {
            _playerInput.Gameplay.Enable();
        }
    }

    private void OnDisable()
    {
        if (_playerInput != null)
        {
            _playerInput.Gameplay.Disable();
        }
    }
}
