using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private float Y = 1800.0f;
    // Start is called before the first frame update
    void Start()
    {
        var dist = Y - transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.localPosition.y + dist, transform.position.z);
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
}
