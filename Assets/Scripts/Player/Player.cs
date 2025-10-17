using UnityEngine;

public class Player : Entity
{
    // Components
    private PlayerAudio sfx;
    private PlayerInput input;
    private Movement movement;

    // Singletons
    private GameManager gm;

    private Vector2 inputDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        sfx = GetComponent<PlayerAudio>();
        input = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();
    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        inputDirection = input.inputDirection;
        movement.Move(inputDirection);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Exit"))
        {
            Debug.Log("Collision triggered by " + collision.gameObject.name);
            sfx.PlayCollisionSound();

            if (collision.gameObject.CompareTag("Enemy"))
                gm.RestartLevel();
            else if (collision.gameObject.CompareTag("Exit"))
                gm.Win();
        }
    }
}
