using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float cooldown = 2f;
    private float timer;

    public bool hasTriggered;

    private void Awake()
    {
        SetTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        hasTriggered = (timer < 0);
    }

    public void SetTimer()
    {
        timer = cooldown;
        hasTriggered = false;
    }
}
