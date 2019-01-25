using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TerrainTreeReplacer :  EditorWindow {

    public GameObject fakeGrass1;
    public GameObject fakeGrass2;
    public GameObject grass1; //Used to replace the fake grass on the terrain
    public GameObject grass1Parent;
    public GameObject grass2;
    public GameObject grass2Parent;

	[MenuItem("Tools/Terrain Tree Replacer")]

	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(TerrainTreeReplacer));
	}

	Terrain terrain;

	int treeDivisions = 5;

	float scaleMin = .9f;
	float scaleMax = 1.5f;

	float heightFudge = 0f;

	void OnGUI()
	{
		// The actual window code goes here
		GUILayout.Label("Replace Trees with Prefabs", EditorStyles.boldLabel);

		terrain = EditorGUILayout.ObjectField("Terrain:", terrain, typeof(Terrain), true) as Terrain;

		GUILayout.Label("Tree groups " + treeDivisions);
		treeDivisions = (int)GUILayout.HorizontalSlider(treeDivisions, 1, 10);
		GUILayout.Space(10f);

		GUILayout.Label("Tree small scale " + scaleMin);
		scaleMin = GUILayout.HorizontalSlider(scaleMin, .5f, 1f);
		GUILayout.Label("Tree large scale " + scaleMax);
		scaleMax = GUILayout.HorizontalSlider(scaleMax, 1f, 3f);
		GUILayout.Space(10f);

		GUILayout.Label("Height fudge " + heightFudge);
		heightFudge = GUILayout.HorizontalSlider(heightFudge, -2f, 2f);
		GUILayout.Space(10f);

		if (GUILayout.Button("Replace trees!"))
        {
			Convert();
		}
	}

	



	public void Convert()
	{
		if (terrain == null)
			return;

		GameObject treeParent = new GameObject("Trees");

		List<List<Transform>> treegroups = new List<List<Transform>>();

		for(int i = 0; i < treeDivisions; i++)
		{
			treegroups.Add(new List<Transform>());
			for (int j = 0; j < treeDivisions; j++)
			{
				GameObject treeGroup = new GameObject("TreeGroup_" + i + "_" + j);
				treeGroup.transform.parent = treeParent.transform;
				treegroups[i].Add(treeGroup.transform);
            }
		}

		TerrainData data = terrain.terrainData;
		float width = data.size.x;
		float height = data.size.z;
		float y = data.size.y;

		float xDiv = data.size.x / (float)treeDivisions;
		float zDiv = data.size.z / (float)treeDivisions;

		foreach (TreeInstance tree in data.treeInstances)
		{
            //If the tree is grass1
            if (data.treePrototypes[tree.prototypeIndex].prefab == fakeGrass1)
            {
                //Add propper tree in it's place
                Vector3 worldTreePos = Vector3.Scale(tree.position, data.size) + Terrain.activeTerrain.transform.position;
                Instantiate(grass1, worldTreePos, Quaternion.identity, grass1Parent.transform); // Create a prefab tree on its pos
            }

            Vector3 position = new Vector3(tree.position.x * width, tree.position.y * y, tree.position.z * height);

			int xGroup = (int)(position.x / xDiv);
			int zGroup = (int)(position.z / zDiv);

			position += terrain.transform.position;

			float scale = Random.Range(scaleMin, scaleMax);
			position.y += heightFudge;

			GameObject newTree = Instantiate(data.treePrototypes [tree.prototypeIndex].prefab, position, Quaternion.Euler (Random.Range (0f,360f) * Vector3.up) ) as GameObject;
			newTree.transform.localScale = scale * Vector3.one;

			newTree.transform.SetParent(treegroups[xGroup][zGroup]);
		}

		//terrain.drawTreesAndFoliage = false;
	}
}
