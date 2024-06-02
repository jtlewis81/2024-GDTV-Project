using UnityEngine;

public class SingleTargetProjectile : Projectile
{
    private void Start()
    {
        rb.velocity = transform.forward * force;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.GetComponent<Health>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
