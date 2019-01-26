using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, Entity
{

    [SerializeField] private float Health = 100;
    [SerializeField] private float Hydration = 100;
    [SerializeField] private float Hunger = 100;
    [SerializeField] private float Cooldown = 30;
    [SerializeField] private float TickDamage = 0.5f;

    private float currCooldown;
    private IDrinkable drinkRef;
    
    public Text HealthText;
    public Text HydrationText;
    public Text HungerText;
    
    // Start is called before the first frame update
    void Start()
    {
        currCooldown = Cooldown;
        HealthText.text = Health.ToString();
        HydrationText.text = Hydration.ToString();
        HungerText.text = Hunger.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currCooldown -= Time.deltaTime;
        if (!(currCooldown <= 0)) return;
        if (Hydration <= 0 || Hunger <= 0)
        {
            Health -= TickDamage;
        }
        else if (Hydration <= 10 || Hunger <= 10)
        {
            Health -= TickDamage / 2;
        }

        if (Random.Range(0, 100) >= 50)
        {
            Hydration--;
            if (Hydration < 0)
            {
                Hydration = 0;
            }
        }

        if (Random.Range(0, 100) >= 50)
        {
            Hunger--;
            if (Hunger < 0)
            {
                Hunger = 0;
            }
        }

        HealthText.text = Health.ToString();
        HydrationText.text = Hydration.ToString();
        HungerText.text = Hunger.ToString();
        currCooldown = Cooldown;
    }

    public void Damage(float hit)
    {
        Health -= hit;
        if (Health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hydrator"))
        {
            if (drinkRef == null)
            {
                drinkRef = other.GetComponent<IDrinkable>();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hydrator"))
        {
            if (drinkRef == null) return;
            if (Input.GetButton("Interact"))
            {
                Hydration += drinkRef.Drink();
                if (Hydration > 100)
                {
                    Hydration = 100;
                }
                HydrationText.text = Hydration.ToString();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hydrator"))
        {
                drinkRef = null;
        }
    }
}
