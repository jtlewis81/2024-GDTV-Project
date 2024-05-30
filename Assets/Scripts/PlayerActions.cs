using UnityEngine;
using UnityEngine.AI;

public class PlayerActions : MonoBehaviour
{
    //[Header("Settings")]
    //[SerializeField] private float moveSpeed = 6f;

    [Header("Read Only")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Transform targetObject;

    public Transform TargetObject => targetObject;

    private NavMeshAgent navMeshAgent;
    
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (InputManager.Instance.GetRightMouseButtonDown()) // handle right click movement
        {
            Transform currentTargetObject = targetObject; // cache previous target object in case targeting different one
            
            Transform newTargetObject = MouseWorld.GetObject(); // see if target is object

            // target is not an object
            if (newTargetObject == null)
            {
                if(currentTargetObject != null) // had previous target object that needs cleared
                {
                    targetObject = null; // remove previously targeted object
                }

                targetPosition = MouseWorld.GetPosition(); // get target position since not targeting an object
                navMeshAgent.destination = targetPosition; // move to targeted position

            }
            // target is a new object (don't change target if same target is clicked on)
            else if (targetObject != newTargetObject)
            {
                targetObject = newTargetObject; // set new target object
                targetPosition = targetObject.position; // set target position to targeted object's position (prevents running around objects to interact)
                navMeshAgent.destination = targetPosition; // move to target object
            }
            
        }

    }

}
