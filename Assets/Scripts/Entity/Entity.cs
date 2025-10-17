using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb {get; private set;}

    //[SerializeField] private float stateTimer;
    //[SerializeField] private float stateCooldown;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        //stateTimer -= Time.deltaTime;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
