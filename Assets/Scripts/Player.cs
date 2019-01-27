using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, Entity
{

    [SerializeField] private float Health = 100;
    [SerializeField] private float Hydration = 100;
    [SerializeField] private float Hunger = 100;
    [SerializeField] private float Cooldown = 1;
    [SerializeField] private float TickDamage = 0.5f;
    [SerializeField] private float HealthTick = 5;
    [SerializeField] private Shader grayscaleShader;

    private float currCooldown;
    private IDrinkable drinkRef;
    private IEatable foodRef;

    public GameObject WeaponMount;
    public GameObject Weapon;
    public Image HealthVisual;
    public Image HydrationVisual;
    public Image HungerVisual;
    public Text InteractionText;
    
    // Start is called before the first frame update
    void Start()
    {
        HealthVisual = GameObject.Find("GUI/HealthBar/Health").GetComponent<Image>();
        HydrationVisual = GameObject.Find("GUI/HydrationBar/Hydration").GetComponent<Image>();
        HungerVisual = GameObject.Find("GUI/HungerBar/Hunger").GetComponent<Image>();
        InteractionText = GameObject.Find("GUI/Interaction").GetComponent<Text>();

        InteractionText.text = "";
        
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
                ModifyHealth(-TickDamage);
            }
            else if (Hydration <= 10 || Hunger <= 10)
            {
                ModifyHealth(-TickDamage / 2);
            }
            else if (Hydration >= 100 && Hunger >= 100)
            {
                ModifyHealth(HealthTick);
            }

            if (Random.Range(0, 100) >= 50)
            {
                ModifyHydration(-1);
            }

            if (Random.Range(0, 100) >= 50)
            {
                ModifyHunger(-1);
            }
            currCooldown = Cooldown;
        }

        if (Input.GetButton("Fire1"))
        {
            try
            {
                Weapon.GetComponent<IWeapon>().Attack();
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }

        HealthVisual.fillAmount = Mathf.Lerp(HealthVisual.fillAmount, Health/100, Time.deltaTime);
        HydrationVisual.fillAmount = Mathf.Lerp(HydrationVisual.fillAmount, Hydration/100, Time.deltaTime);
        HungerVisual.fillAmount = Mathf.Lerp(HungerVisual.fillAmount, Hunger/100, Time.deltaTime);
    }

    private void ModifyHealth(float amt)
    {
        Health += amt;
        if (Health > 100)
        {
            Health = 100;
        }

        if (Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
            //die("You fell asleep... Try again in a bit");
        }
    }
    
    private void ModifyHydration(float amt)
    {
        Hydration += amt;
        if (Hydration > 100)
        {
            Hydration = 100;
        }

        if (Hydration <= 0)
        {
            Hydration = 0;
        }
    }
    
    private void ModifyHunger(float amt)
    {
        Hunger += amt;
        if (Hunger > 100)
        {
            Hunger = 100;
        }

        if (Hunger <= 0)
        {
            Hunger = 0;
        }
    }

    public void Damage(float hit)
    {
        ModifyHealth(-hit);
    }

    public float GetHealth()
    {
        return Health;
    }

    private void die(string endText)
    {
        InteractionText.text = endText;
        InteractionText.gameObject.SetActive(true);
        this.enabled = false;
        gameObject.GetComponent<FirstPersonController>().enabled = false;
        Invoke("loadTitle", 5);
    }

    public void FallOffLedge()
    {
        die("Flat Earthers: 1. Everyone else: 0");
    }

    private void loadTitle()
    {
        SceneManager.LoadScene("Title");
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
                ModifyHydration(drinkRef.Drink());
            }
        }
        if (other.CompareTag("Feeder"))
        {
            if (foodRef == null) return;
            if (Input.GetButton("Interact"))
            {
                ModifyHunger(foodRef.Eat());
                foodRef = null;
                
            }
        }

        var hint = other.GetComponent<IHintable>();
        if (Input.GetButton("Interact"))
        {
            var success = hint?.CollectHint();
            if ( success != null && (bool) success)
            {
                InteractionText.gameObject.SetActive(false);
            } //Tell the player they messed up?
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
