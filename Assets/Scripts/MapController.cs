using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapController : MonoBehaviour
{
    public float sensitivity;

    Vector3 m_EulerAngleVelocity;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        m_EulerAngleVelocity = new Vector3(movementVector.y * sensitivity, 0, -movementVector.x * sensitivity);
       
    }

    private void FixedUpdate()
    {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        Quaternion totalrotation = rb.rotation * deltaRotation;

        rb.MoveRotation(totalrotation);
    }
}