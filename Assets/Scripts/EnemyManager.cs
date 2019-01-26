using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyPrefabsUser;
    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();

    [SerializeField] private int MaxEnemies = 15;

    [SerializeField] private float DetectionDist = 150;

    [SerializeField] private float MaxAliveDist = 1000;

    private GameObject player;

    private Terrain terrain;
    private Transform enemyHolder;

    private List<GameObject> EnemyPrefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var pref in EnemyPrefabsUser)
        {
            if (pref.GetComponent<Entity>() == null)
            {
                Debug.LogException(new Exception(pref.name + " is not an Entity"));
                continue;
            }
            if (pref.GetComponent<IEnemy>() == null)
            {
                Debug.LogException(new Exception(pref.name + " is not an Enemy"));
                continue;
            }
            EnemyPrefabs.Add(pref);
        }
        player = GameObject.FindWithTag("Player");
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        enemyHolder = new GameObject("Enemies").transform;
        if (EnemyPrefabs.Count > 0)
        {
            for (var i = 0; i < MaxEnemies; i++)
            {
                generateEnemy();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var toRemove = new List<GameObject>();
        foreach (var enemy in Enemies)
        {
            if (Vector3.Distance(enemy.transform.position, player.transform.position) <= DetectionDist)
            {
                enemy.GetComponent<IEnemy>().SetTarget(player);
            }

            if (Vector3.Distance(enemy.transform.position, player.transform.position) > MaxAliveDist)
            {
                toRemove.Add(enemy);
            }
        }

        foreach (var del in toRemove)
        {
            Enemies.Remove(del);
            Destroy(del);
            generateEnemy();
        }
    }

    void generateEnemy()
    {
        var inst = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)];
        var theta = Random.Range(0, 2 * (float)Math.PI);
        var dist = Math.Sqrt(Random.Range(0.0f, 1.0f)) * MaxAliveDist;
        if (dist < MaxAliveDist / 4) dist = MaxAliveDist / 4;
        var xPos = (float)(dist * Math.Cos(theta) + player.transform.position.x);
        var zPos = (float)(dist * Math.Sin(theta) + player.transform.position.z);
        var yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos));
        var obj = Instantiate(inst, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        if (obj != null)
        {
            Enemies.Add(obj);
            obj.transform.SetParent(enemyHolder);
        }
    }
}
