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

        // For every tree on the terrain
        foreach (TreeInstance tree in terrain.treeInstances)
        {
            //TreeInstance tree2 = new TreeInstance();
            //tree2.rotation = 10f;

            //If the tree is the targetObj type of tree
            if(terrain.treePrototypes[tree.prototypeIndex].prefab == targetObj)
            {
                //Add propper tree in it's place
                Vector3 worldTreePos = Vector3.Scale(tree.position, terrain.size) + Terrain.activeTerrain.transform.position;
                Instantiate(theTree, worldTreePos, Quaternion.identity, parentObj.transform); // Create a prefab tree on its pos
            }
        }
    }
}
