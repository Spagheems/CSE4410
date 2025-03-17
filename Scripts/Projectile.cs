using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 3.0f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after a few seconds
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ReactiveTarget target = collision.gameObject.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                target.ReactToHit();
            }

            Destroy(gameObject); // Destroy projectile on hit
        }
    }
}
