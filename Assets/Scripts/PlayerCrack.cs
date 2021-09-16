using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCrack : MonoBehaviour {
    [Header("Physics settings")]
    [SerializeField]
    private float crackThreshold = 20.0f;

    [Header("Scene settings")]
    [SerializeField]
    private int sceneBuildIndex = 0;
    private float initialTimeScale;
    private float initialFixedDeltaTime;

    [Header("UI settings")]
    [SerializeField]
    private CanvasGroup deathScreen;

    private Rigidbody m_rigidbody;

    private bool cracked;

    private void Awake() {
        initialTimeScale = Time.timeScale;
        initialFixedDeltaTime = Time.fixedDeltaTime;

        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        Vector3 relativeVelocity = collision.relativeVelocity;
        float collisionMagnitude = (new Vector2(relativeVelocity.x, relativeVelocity.z)).magnitude;

        if(collisionMagnitude < crackThreshold) {
            return;
        }

        if(cracked) {
            return;
        }

        cracked = true;
    }

    private void Update() {
        if(!cracked) {
            return;
        }

        float lerpSpeed = Mathf.Min(4f * Time.deltaTime, 1f);

        Time.timeScale = Mathf.Lerp(Time.timeScale, 1f / 20f, lerpSpeed);
        Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, initialFixedDeltaTime / 20f, lerpSpeed);

        float currentVelocity = 0.75f;
        deathScreen.alpha = Mathf.SmoothDamp(deathScreen.alpha, 1f, ref currentVelocity, 40f * Time.deltaTime);
    }

    public void Restart() {
        cracked = false;
        Time.timeScale = initialTimeScale;
        Time.fixedDeltaTime = initialFixedDeltaTime;

        SceneManager.LoadScene(sceneBuildIndex);
    }
}