using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public static ObjectSelector Instance { get; private set; }

    [field:SerializeField] public Transform SelectedObject { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (InputManager.Instance.GetLeftMouseButtonDown())
        {
            Transform selected = MouseWorld.GetObject();
            if(selected != null)
            {
                SelectedObject = selected;
            }
        }
    }

}
