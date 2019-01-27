using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour, IHintable
{
    private SpriteRenderer hint;
    void Awake()
    {
        hint = GetComponentInChildren<SpriteRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Win");
            //Debug.Log("You Win!");
        }
    }

    public bool CollectHint()
    {
        return true;
    }

    public void DisableHint()
    {
        hint.enabled = false;
    }

    public void EnableHint()
    {
        hint.enabled = true;
    }


}
