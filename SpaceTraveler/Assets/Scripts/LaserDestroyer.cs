using UnityEngine;

public class LaserDestroyer : MonoBehaviour
{
    // if projectile cross the bounds of screen
    // this function will destroy them
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}