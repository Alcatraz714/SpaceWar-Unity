using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Particle system var and others
    [Header("Reload Time and Particle Systems")]
    [SerializeField] float ReloadDelay = 1f;
    [SerializeField] ParticleSystem ExplosionParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Collision Handler script - Redacted

    // Trigger actions on collision
    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    // Restart Level upon Ship Collision
    void StartCrashSequence()
    {
        //Play Particles
        ExplosionParticles.Play();
        //Disable Mesh Renderer
        GetComponent<MeshRenderer>().enabled = false;
        //Disable Collider
        GetComponent<BoxCollider>().enabled = false;
        //Disale Player Control
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", ReloadDelay);
    }

    // Reload Level function
    void ReloadLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }
}
