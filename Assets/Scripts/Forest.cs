using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Forest : MonoBehaviour
{

    public GameObject[] trees;
    public float lowRange = 200.0f;
    public float midRange = 400.0f;
    public float lowChance = 80.0f;
    public float midChance = 40.0f;
    public float highChance = 1.0f;
    public int numTrees = 100000;

    //public GameObject tree_2;
    private Transform forestHolder;

    private Terrain terrain;
    private Vector3 terrainArea;

    void Awake()
    {
        bool makeTree;
        forestHolder = new GameObject("Forest").transform;

        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        terrainArea = terrain.terrainData.size;
        for(int i = 0; i < numTrees; i++)
        {
            float tmpMax = terrainArea.x;
            float xPos = Random.Range(0, tmpMax);
            tmpMax = terrainArea.z;
            float zPos = Random.Range(0, tmpMax);
            float yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos));

            makeTree = false;
            if (yPos < lowRange)
            {
                if (Random.Range(0, 100) <= lowChance) makeTree = true;
            }
            else if (yPos > lowRange && yPos < midRange)
            {
                if (Random.Range(0, 100) <= midChance) makeTree = true;
            }
            else if (yPos > midRange)
            {
                if (Random.Range(0, 100) <= highChance) makeTree = true;
            }

            if (makeTree)
            {
                Vector3 vec = new Vector3(xPos, yPos - 0.01f, zPos);

                GameObject toInstanciate = trees[Random.Range(0, trees.Length)];
                GameObject instance = Instantiate(toInstanciate, vec, Quaternion.Euler(0, Random.Range(0, 360), 0));
                //instance.transform.rotation = Random.rotation;
                instance.transform.SetParent(forestHolder);
            }

        }

    }
}
