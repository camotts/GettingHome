using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSymbol : MonoBehaviour
{
    [SerializeField] private float Y = 1700;
    [SerializeField] private float xScale = 5;
    [SerializeField] private float yScale = 5;
    [SerializeField] private float zScale = 5;
    // Start is called before the first frame update
    void Start()
    {
        var dist = Y - transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.localPosition.y + dist, transform.position.z);
        
        transform.localScale = new Vector3(xScale, yScale, zScale);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation.Set(0,PlayerManager.player.transform.rotation.eulerAngles.y,0,0);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, PlayerManager.player.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}
