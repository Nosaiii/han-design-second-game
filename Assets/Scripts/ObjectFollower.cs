using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ObjectFollower : MonoBehaviour
{
    [Header("Object settings")]
    [SerializeField]
    private Transform followee;
    public Transform Followee
    {
        get
        {
            return followee;
        }
        set
        {
            followee = value;
        }
    }

    [Header("Movement settings")]
    [SerializeField]
    private float smoothSpeed = 5f;

    private Vector3 cameraOffset;

    private Vector3 goalPosition;

    private void Awake()
    {
        if (Followee == null)
        {
            return;
        }

        cameraOffset = Followee.position - transform.position;
    }

    private void FixedUpdate()
    {
        if (Followee == null)
        {
            return;
        }

        goalPosition = Followee.transform.position - cameraOffset;
        transform.position = Vector3.Lerp(transform.position, goalPosition, smoothSpeed * Time.fixedDeltaTime);
    }
}