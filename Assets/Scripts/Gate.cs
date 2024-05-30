using UnityEngine;

public class Gate : IInteractable
{
    [SerializeField] private float initHP;

    private float currentHP;

    private void Awake()
    {
        currentHP = initHP;
    }


}
