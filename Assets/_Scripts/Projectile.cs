using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float force = 20f;

    protected Rigidbody rb;
    public int Damage { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
