using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MaterialPointController : MonoBehaviour {

    // Use this for initialization
	[SerializeField]
	private GameObject lineRendererPrefab;

    public List<GameObject> closestObjects;
	private List<Transform[]> lineConnections; //for render
	private List<LineRenderer> lineRender;
    public MaterialPointPhysics physics;
    public float mass = 0.1f;


    Vector3 addForce;
    void Start ()
    {
		closestObjects = FindNeighbors(4);
        List<Vector3> ballanceSpringsPos = closestObjects.Select(pos => pos.transform.position).ToList();
        physics = new MaterialPointPhysics(this.transform.position, ballanceSpringsPos, mass);
        foreach (var closePoint in closestObjects)
        {
            GameObject duplicate = closePoint.GetComponent<MaterialPointController>().closestObjects.Find(p => p.name == this.name);
            if (duplicate != null)
            {
                closePoint.GetComponent<MaterialPointController>().closestObjects.Remove(duplicate);
                var duplicateL2 = this.closestObjects.Find(p => p.name == duplicate.name);
                if (duplicateL2 != null)
                {
                    this.closestObjects.Remove(duplicateL2);
                }
            }
        }

        lineRender = new List<LineRenderer> ();
		GameObject linesParent = new GameObject ("joint node:" + this.name);
		foreach (var line in lineConnections)
		{
			LineRenderer newLine = Instantiate (lineRendererPrefab).GetComponent<LineRenderer>();
			newLine.transform.parent = linesParent.transform;
			lineRender.Add(newLine);
		}

    }
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < lineConnections.Count(); i++) 
		{
			lineRender [i].SetPositions (new Vector3[] { lineConnections [i] [0].position, lineConnections [i] [1].position });
		}

        Vector3 force = physics.springJointForce(this.closestObjects.Select(x => x.transform.position).ToArray(), 1.4f);

        physics.AddForce(force);
        
        physics.Move(Time.deltaTime);
      
        this.transform.position = physics.Position;
        Debug.DrawLine(this.transform.position, this.transform.position + force, Color.red);


         if (this.name == "Mat_Point:0")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                addForce = Vector3.up * 14f;
            }
            else
            {
                addForce = Vector3.zero;
            }
           
        }
    }

	public List<GameObject> FindNeighbors(int neighborsCount)
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
		var closestPoints = orderedPoints.Skip(Math.Max (0, orderedPoints.Count() - neighborsCount));
	

		List<Vector3> connections = new List<Vector3>();
		lineConnections = new List<Transform[]>();

		foreach (var item in closestPoints) 
		{
    
			connections.Add(item.Key.transform.position);
			lineConnections.Add (new Transform[] { this.transform, item.Key.transform }); 
            
            
		}
    
	
		if(this.name == "Mat_Point:0")
		{

            foreach (var item in closestPoints)
        	{
				
				//if (item.Key.GetComponent<) {
					
				//}
				Debug.Log("node" + this.name + " " + item.Key.name + " " + item.Value.ToString());
				//item.Key.gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
        	}
		}

		closestObjects = closestPoints.Select ((x) => x.Key).ToList();	
		return closestObjects;
    }
}
