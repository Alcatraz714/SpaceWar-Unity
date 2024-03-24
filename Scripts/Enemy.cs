using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject EnemyExplosion;
    [SerializeField] GameObject EnemyHit;
    
    [SerializeField] int ScoreForHit = 25;
    [SerializeField] int Hitpoints = 6;

    ScoreBoard Scoreboard;
    GameObject ParentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        Scoreboard = FindObjectOfType<ScoreBoard>();
        ParentGameObject = GameObject.FindWithTag("SpawnAtRuntime");

        // add rigid body for colliders to work as one unit
        Rigidbody Rb = gameObject.AddComponent<Rigidbody>();
        // disable gravity for the RB
        Rb.useGravity = false;
    }

    // Destroy on collision with particles - Lasers
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        // kill if 1
        if (Hitpoints < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        //Play Particles
        GameObject vfx = Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
        // Club particles into a group using parent class assign
        vfx.transform.parent = ParentGameObject.transform;// changed from transform to gameobject
        Destroy(gameObject);
    }

    private void ProcessHit()
    {
        // Hit particles
        GameObject vfx = Instantiate(EnemyHit, transform.position, Quaternion.identity);
        vfx.transform.parent = ParentGameObject.transform;// changed from transform to gameobject
        // update hp for enemy
        Hitpoints--;
        // Update score for board
        Scoreboard.UpdateScore(ScoreForHit);
    }
}
