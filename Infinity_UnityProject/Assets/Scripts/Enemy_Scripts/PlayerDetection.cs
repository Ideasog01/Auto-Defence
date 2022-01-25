using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    private bool isEnemyDetector;

    [SerializeField]
    private bool isBlasterPickupDetector;

    private BaseEnemyController _enemyController;

    private BlasterPickup _blasterPickup;

    private PlayerController _playerController;

    private void Awake()
    {
        if(isEnemyDetector)
        {
            _enemyController = this.transform.parent.GetComponent<BaseEnemyController>();
        }
        
        if(isBlasterPickupDetector)
        {
            _blasterPickup = this.transform.parent.GetComponent<BlasterPickup>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(isEnemyDetector)
            {
                _enemyController.AssignPlayer(other.gameObject);
            }
            
            if(isBlasterPickupDetector)
            {
                other.gameObject.GetComponent<PlayerController>().nearBlaster = this.transform.parent.GetComponent<BlasterPickup>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(isEnemyDetector)
            {
                GameObject player = _enemyController.GetPlayer();

                if (player != null)
                {
                    if (other.gameObject == player)
                    {
                        _enemyController.AssignPlayer(null);
                    }
                }
            }

            if(isBlasterPickupDetector)
            {
                other.gameObject.GetComponent<PlayerController>().nearBlaster = null;
            }
        }
    }
}
