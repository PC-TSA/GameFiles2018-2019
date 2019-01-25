using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeReplacer : MonoBehaviour
{
    public GameObject theTree; //Will replace all target trees with this tree
    public GameObject targetObj;
    public GameObject terrainObj;
    public GameObject parentObj;

    // Use this for initialization
    void Start()
    {
        // Grab the terrain's data
        TerrainData terrain = terrainObj.GetComponent<Terrain>().terrainData;
        int count = 0;
        // For every tree on the terrain
        foreach (TreeInstance tree in terrain.treeInstances)
        {
            TreeInstance tree2 = tree;
            float temp = tree.rotation;
            tree2.rotation = Mathf.Deg2Rad * Random.Range(0, 180);
            terrain.SetTreeInstance(count, tree2);
            Debug.Log("Index: " + count + " Old Rot: " + temp + " New rot: " + tree.rotation + " Intended Rot: " + tree2.rotation);
            count++;

            //If the tree is the targetObj type of tree
            if(targetObj != null && terrain.treePrototypes[tree.prototypeIndex].prefab == targetObj)
            {
                //Add propper tree in it's place
                Vector3 worldTreePos = Vector3.Scale(tree.position, terrain.size) + Terrain.activeTerrain.transform.position;
                Instantiate(theTree, worldTreePos, Quaternion.identity, parentObj.transform); // Create a prefab tree on its pos
            }
        }
    }
}
