using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSymbol : MonoBehaviour
{
    [SerializeField] private float Y = 200;

    [SerializeField] private float xScale = 5;

    [SerializeField] private float zScale = 5;
    // Start is called before the first frame update
    void Start()
    {
        var currGlobalY = transform.position.y;
        var offset = Y - currGlobalY;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + offset, transform.localPosition.z);
        
        transform.localScale = new Vector3(xScale, 0, zScale);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
