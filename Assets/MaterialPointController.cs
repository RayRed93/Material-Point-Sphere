using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MaterialPointController : MonoBehaviour {

    // Use this for initialization
    private List<GameObject> closestObjects;

    void Start ()
    {
        FindClosestEnemy();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<GameObject> FindClosestEnemy()
    {
        GameObject[] materialPoints;
        Dictionary<GameObject, float> pointsDictionary = new Dictionary<GameObject, float>();
        closestObjects = new List<GameObject>();
        materialPoints = GameObject.FindGameObjectsWithTag("MaterialPoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject materialPoint in materialPoints)
        {
            Vector3 diff = materialPoint.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            pointsDictionary.Add(materialPoint, curDistance);
        }

        var xxx = pointsDictionary.OrderBy(x => x.Value);
        var ptt = xxx.Skip(Math.Max(0, xxx.Count() - 4));
        foreach (var item in ptt)
        {
            Debug.Log(item.ToString());
        }
        return closestObjects;
    }
}
