using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.Player;

public class PowerUP : MonoBehaviour
{
    [SerializeField] private List<GameObject> projectiles = null;
    private Rigidbody2D rb2d = null;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0f, -1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (tag == "+1")
            {
                other.GetComponent<PlayerController>().IncrementShotCounter(1);
            }
            else if (tag == "+2")
            {
                other.GetComponent<PlayerController>().IncrementShotCounter(2);
            }
            else if (tag == "PowerUPProjectiles")
            {
                other.GetComponent<PlayerController>().ChangeProjectile(projectiles[0]);
            }
            else if (tag == "PowerUPShield")
            {
                other.GetComponent<PlayerController>().ShieldOn(true);
            }
        }
        Destroy(gameObject);
    }
}