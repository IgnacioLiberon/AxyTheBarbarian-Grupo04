using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    // Speed of the player movement
    public float moveSpeed = 5f;
    private Vector2 inputDirection = Vector2.zero;

    private Rigidbody2D rb;
    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    private void Update()
    {
        ProcessInput();
        UpdateState(Time.deltaTime);
    }

    private void ProcessInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;

        inputDirection = new Vector2(x, y).normalized;
    }

    private void UpdateState(float deltaTime)
    {
        Vector2 movement = inputDirection * moveSpeed * deltaTime;
        MoveTo(movement);
    }

    private void MoveTo(Vector2 movement)
    {
        transform.Translate(movement);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity = Vector2.zero;
            EditorApplication.Beep();
        }
    }
}
