using UnityEngine;

public class AOEProjectile : Projectile
{
    [SerializeField] float radius = 3f;
    [SerializeField] LayerMask mask;

    private void Start()
    {
        rb.velocity = transform.forward * force;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, mask);
            foreach (Collider hit in hits)
            {
                if(hit.GetComponent<Enemy>() != null)
                {
                    hit.GetComponent<Health>().TakeDamage(Damage);
                }
            }
        }
        Destroy(gameObject);
    }

}
