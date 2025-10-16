using UnityEngine;
using UnityEditor;

public class Player : Entity
{
    // Components
    private PlayerAudio audio;
    private PlayerInput input;
    private PlayerMovement movement;

    private Vector2 inputDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        audio = GetComponent<PlayerAudio>();
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        inputDirection = input.inputDirection;
        movement.Move(inputDirection, Time.deltaTime);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision triggered by " + collision.gameObject.name);
            audio.PlayCollisionSound();
        }
    }
}
