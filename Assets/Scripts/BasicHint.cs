using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHint : MonoBehaviour, IHintable, IInteractable
{
    private SpriteRenderer hint;

    private void Awake()
    {
        hint = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CollectHint()
    {
        if (HintManager.Instance.CompleteHint(gameObject))
        {
            DisableHint();
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public void DisableHint()
    {
        hint.enabled = false;
    }

    public void EnableHint()
    {
        hint.enabled = true;
    }

    public string GetInteractionText()
    {
        return "Press F to pick up the cup";
    }
}
