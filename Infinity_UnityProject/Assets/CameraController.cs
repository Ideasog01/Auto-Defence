using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 targetOffset;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    private Vector3 _velocity;

    private void LateUpdate()
    {
        Vector3 destination = target.position + targetOffset;
        Vector3 smoothedDestination = Vector3.Lerp(this.transform.position, destination, Time.deltaTime * smoothSpeed);

        this.transform.position = smoothedDestination;
    }
}
