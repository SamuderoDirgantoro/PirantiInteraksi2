using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{
    [HideInInspector] public EnemyIdle idleState;
    [HideInInspector] public EnemyPatrol patrolState;
    [HideInInspector] public EnemyChase chaseState;
    [HideInInspector] public EnemyJumping jumpingState;

    [Header("Components")]
    public Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;

    [Header("Movement Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float jumpForce = 8f;

    [Header("Detection Settings")]
    public float chaseRange = 5f;
    public float attackRange = 1.2f;

    [Header("Target")]
    public Transform player;
    
    private void Awake()
    {
        idleState = new EnemyIdle(this);
        patrolState = new EnemyPatrol(this);
        chaseState = new EnemyChase(this);
        jumpingState = new EnemyJumping(this);
        // Mencari objek dengan nama "Player"
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.transform;

        // Memeriksa apakah objek berhasil ditemukan
        if (playerObject != null)
        {
            Debug.Log("Objek 'Player' ditemukan!");
        }
        else
        {
            Debug.Log("Objek 'Player' tidak ditemukan.");
        }
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
