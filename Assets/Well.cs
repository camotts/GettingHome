using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour, IDrinkable
{
    [SerializeField] private float Hydration = 100;

    public float Drink()
    {
        return Hydration;
    }
}
