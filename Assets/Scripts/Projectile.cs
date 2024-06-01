using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float force = 20f;

    private Rigidbody rb;
    public int Damage { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = transform.forward * force;
    }
}
