using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour, IDrinkable, IInteractable
{
    [SerializeField] private float Hydration = 100;

    public float Drink()
    {
        return Hydration;
    }

    public string GetInteractionText()
    {
        return "Press F to drink water";
    }
}
