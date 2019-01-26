using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, IWeapon
{
    [SerializeField] private float Damage = 100;
    private Animator anim;
    private Collider col;

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    public void Attack()
    {
        col.enabled = true;
        anim.SetTrigger("Attack");
        
    }
    
    public void ActivateCollider(int active)
    {
        if (active == 0)
            col.enabled = false;
        else
            col.enabled = true;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Enemy"))
        {
            var entity = other.GetComponent<Entity>();
            entity?.Damage(Damage);
        }
    }
}
