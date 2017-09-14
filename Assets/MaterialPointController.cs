using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MaterialPointController : MonoBehaviour {

    // Use this for initialization
	[SerializeField]
	private GameObject lineRendererPrefab;

    private List<GameObject> closestObjects;
	private List<Transform[]> lineConnections;
	private List<LineRenderer> lineRender;
    void Start ()
    {
		closestObjects = new List<GameObject> ();
		closestObjects = FindNeighbors();
		lineRender = new List<LineRenderer> ();
		GameObject linesParent = new GameObject ("lines");
		foreach (var line in lineConnections)
		{
			LineRenderer newLine = Instantiate (lineRendererPrefab).GetComponent<LineRenderer>(); 
			newLine.transform.parent = linesParent.transform;
		

			lineRender.Add (newLine);
		}

    }
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < lineConnections.Count(); i++) 
		{
			lineRender [i].SetPositions (new Vector3[] { lineConnections [i] [0].position, lineConnections [i] [1].position });
		}
	}

	public List<GameObject> FindNeighbors()
    {
        GameObject[] materialPoints;
        Dictionary<GameObject, float> pointsDictionary = new Dictionary<GameObject, float>();
      
		materialPoints = GameObject.FindGameObjectsWithTag ("MaterialPoint");
        foreach (GameObject materialPoint in materialPoints)
        {
			Vector3 diff = materialPoint.transform.position - this.transform.position;
            float curDistance = diff.sqrMagnitude; //hmm
            pointsDictionary.Add(materialPoint, curDistance);
        }

		var orderedPoints = pointsDictionary.OrderByDescending (x => x.Value).ToDictionary (x => x.Key);
		orderedPoints.Remove(orderedPoints.Keys.Last());
		var closestPoints = orderedPoints.Skip (Math.Max (0, orderedPoints.Count () - 4));
	

		List<Vector3> connections = new List<Vector3>();
		lineConnections = new List<Transform[]> ();

		foreach (var item in closestPoints) 
		{
			
			//if(item.Key.GetComponent<MaterialPointController>().closestObjects.Contains(this.gameObject))
			{
				connections.Add (item.Key.transform.position);
				lineConnections.Add (new Transform[] { this.transform, item.Key.transform });

			//item.Key.GetComponent<LineRenderer> ().SetPositions();
			}
		}

	


		if(this.name == "Mat_Point:0")
		{
			foreach (var item in closestPoints)
        	{
				
				//if (item.Key.GetComponent<) {
					
				//}
				Debug.Log("node" + this.name + " " + item.Key.name + " " + item.Value.ToString());
				item.Key.gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
        	}
		}

		closestObjects = closestPoints.Select ((x) => x.Key).ToList();	
		return closestObjects;
    }
}
