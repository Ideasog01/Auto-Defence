using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotation;

    [SerializeField]
    private float rotationSpeed;

    void Update()
    {
        this.transform.eulerAngles += rotation * Time.deltaTime * rotationSpeed;
    }
}
