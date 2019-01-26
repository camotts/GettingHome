using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBasket : MonoBehaviour, IInteractable, IEatable
{
    [SerializeField] private float Feed = 10;

    public string GetInteractionText()
    {
        return "Press F to Eat";
    }

    public float Eat()
    {
        Destroy(gameObject);
        return Feed;
    }
}
