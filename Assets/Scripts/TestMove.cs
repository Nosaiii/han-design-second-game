using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestMove : MonoBehaviour {
    [SerializeField]
    private float movementSpeed = 5.0f;

    private Rigidbody m_rigidbody;

    private void Awake() {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        m_rigidbody.AddForce(transform.forward * Time.fixedDeltaTime * movementSpeed);
    }
}