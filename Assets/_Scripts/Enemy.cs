using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 4f;
    //[SerializeField] private float playerTargetingRadius = 5f;
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private float attackSpeed = 1f; // time in seconds to count down between attacks
    [SerializeField] private int attackPower = 10;

    [Header("Read Only")]
    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform player;
    [SerializeField] private Transform gate;
    [SerializeField] private bool isAttacking = false;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float attackTimer = 0f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        attackTimer = attackSpeed;
        player = FindFirstObjectByType<PlayerActions>().transform;
        gate = FindFirstObjectByType<Gate>().transform;
        targetObject = gate; // default target is Gate
        navMeshAgent.destination = targetObject.position;
    }
    
    private void Update()
    {
        if(gate == null)
        {
            targetObject = null;
            animator.SetBool("gameOver", true);
            navMeshAgent.destination = transform.position;
            return;
        }
        else if (Vector3.Distance(transform.position, targetObject.position) <= attackDistance) // arrived at target => set state
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            navMeshAgent.destination = transform.position;
        }

        if (isAttacking) // attack over time
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                attackTimer = attackSpeed;
                HandleAttacking();
            }
        }

        /*
        if (!isAttacking)
        {
            HandleTargeting();

            if (targetObject != null && Vector3.Distance(transform.position, targetObject.position) <= attackDistance) // arrived at target => set state
            {
                isAttacking = true;
                navMeshAgent.destination = transform.position;
            }
        }

        if (isAttacking) // attack over time
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                attackTimer = attackSpeed;
                HandleAttacking();
            }
            if (targetObject == null) // target destroyed/moved => get new target
            {
                isAttacking = false;
                HandleTargeting();
            }
        }
        */
    }

    
    /*
    private void HandleTargeting ()
    {
        if (targetObject == gate && Vector3.Distance(transform.position, player.position) < playerTargetingRadius)
        {
            targetObject = player;
            navMeshAgent.destination = targetObject.position;
        }
        else if (targetObject == player && Vector3.Distance(transform.position, player.position) > playerTargetingRadius)
        {
            targetObject = gate;
            navMeshAgent.destination = targetObject.position;
        }

        // add logic to attack towers?
    }
    */
    private void HandleAttacking() // fires off every time the timer gets to 0
    {
        if (targetObject != null && Vector3.Distance(transform.position, targetObject.position) <= attackDistance) // double check that target exists and is in range
        {
            targetObject.GetComponent<Health>().TakeDamage(attackPower);
        }
        else // if target is gone/moved, stop attacking
        {
            isAttacking = false;
            animator.SetBool("isAttacking", false);
        }
    }
}
