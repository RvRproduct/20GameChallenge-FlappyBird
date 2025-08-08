using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float forceAmount = 500f;
    [SerializeField] Sprite idle;
    [SerializeField] Sprite move;
    InputActions inputActions;
    SpriteRenderer spriteRenderer;
    Collider2D playerCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputActions();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        inputActions.PlayerControls.Enable();
        inputActions.PlayerControls.Force.started += ForcePlayerUp;
        inputActions.PlayerControls.Force.canceled += ReturnToIdle;
    }

    private void Start()
    {
        if (rb != null && GameManager.Instance.IsGameStarted())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void ReturnToIdle(InputAction.CallbackContext context)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = idle;
        } 
    }

    void ForcePlayerUp(InputAction.CallbackContext context)
    {
        if (rb != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = move;
            rb.AddForce(Vector2.up * forceAmount, ForceMode2D.Force);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "UnAlive")
        {
            inputActions.PlayerControls.Force.started -= ForcePlayerUp;
            inputActions.PlayerControls.Force.canceled -= ReturnToIdle;
            GameManager.Instance.GameOverScreen();
            SoundManager.Instance.PlayBonk();
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }
        }
    }
}
