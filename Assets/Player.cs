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
    [SerializeField] private float Cooldown = 1;
    [SerializeField] private float TickDamage = 0.5f;
    [SerializeField] private float HealthTick = 5;

    private float currCooldown;
    private IDrinkable drinkRef;
    private IEatable foodRef;
    
    public Image HealthVisual;
    public Image HydrationVisual;
    public Image HungerVisual;
    public Text InteractionText;
    
    // Start is called before the first frame update
    void Start()
    {
        currCooldown = Cooldown;
        HealthVisual.fillAmount = Health/100;
        HydrationVisual.fillAmount = Hydration/100;
        HungerVisual.fillAmount = Hunger/100;
        InteractionText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currCooldown -= Time.deltaTime;
        if ((currCooldown <= 0))
        {
            if (Hydration <= 0 || Hunger <= 0)
            {
                Health -= TickDamage;
            }
            else if (Hydration <= 10 || Hunger <= 10)
            {
                Health -= TickDamage / 2;
            }
            else if (Hydration >= 100 && Hunger >= 100) {}

            Health += HealthTick;

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
            currCooldown = Cooldown;
        }

        HealthVisual.fillAmount = Mathf.Lerp(HealthVisual.fillAmount, Health/100, Time.deltaTime);
        HydrationVisual.fillAmount = Mathf.Lerp(HydrationVisual.fillAmount, Hydration/100, Time.deltaTime);
        HungerVisual.fillAmount = Mathf.Lerp(HungerVisual.fillAmount, Hunger/100, Time.deltaTime);
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
        var interact = other.GetComponent<IInteractable>();
        if (interact != null)
        {
            InteractionText.gameObject.SetActive(true);
            InteractionText.text = interact.GetInteractionText();
        }
        if (other.CompareTag("Hydrator"))
        {
            if (drinkRef == null)
            {
                drinkRef = other.GetComponent<IDrinkable>();
            }
        }
        if (other.CompareTag("Feeder"))
        {
            if (foodRef == null)
            {
                foodRef = other.GetComponent<IEatable>();
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
            }
        }
        if (other.CompareTag("Feeder"))
        {
            if (foodRef == null) return;
            if (Input.GetButton("Interact"))
            {
                Hunger += foodRef.Eat();
                if (Hunger > 100)
                {
                    Hunger = 100;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hydrator"))
        {
                drinkRef = null;
        }
        if (other.CompareTag("Feeder"))
        {
            foodRef = null;
        }

        if (other.GetComponents<IInteractable>().Length > 0)
        {
            InteractionText.gameObject.SetActive(false);
        }
    }
}
