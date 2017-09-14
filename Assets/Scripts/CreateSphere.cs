using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateSphere : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject materialPoint;
    [SerializeField]
    private float radius;
	void Start ()
    {
        //var sphericalPoints = SphericalPointsDistribution.GetCartesianCoordinates(radius, 10);
        var sphericalPoints = SphericalPointsDistribution.FibonacciSphere(199, 6);
        GameObject Ball = new GameObject("Ball");
		int id = 0;
		foreach (var point in sphericalPoints)
        {
            GameObject newMaterialPoint = (GameObject)Instantiate(materialPoint, point, Quaternion.identity);
            newMaterialPoint.transform.parent = Ball.transform;
			newMaterialPoint.name = String.Format("Mat_Point:{0}", id++);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	

    private double DegreeToRadian(double angle)
    {
        return Math.PI * angle / 180.0;
    }

}
