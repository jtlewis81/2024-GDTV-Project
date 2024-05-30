using UnityEngine;

public class Building : IInteractable
{
    [SerializeField] private Transform menuPanel;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerActions>() != null)
        {
            menuPanel.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerActions>() != null)
        {
            menuPanel.gameObject.SetActive(false);
        }


    }
}
