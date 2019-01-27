using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
}
