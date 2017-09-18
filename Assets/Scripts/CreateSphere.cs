using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class CreateSphere : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject materialPoint;
    [SerializeField]
    private int radius;
    private List<GameObject> materialPoints;
    void Start()
    {
        materialPoints = new List<GameObject>();


        //var sphericalPoints = SphericalPointsDistribution.GetCartesianCoordinates(radius, 10);
        var sphericalPoints = SphericalPointsDistribution.FibonacciSphere(199, radius);
        GameObject materialPointSphere = new GameObject("Sphere");
        int id = 0;
        foreach (var point in sphericalPoints)
        {
            GameObject newMaterialPoint = (GameObject)Instantiate(materialPoint, point, Quaternion.identity);
            newMaterialPoint.transform.parent = materialPointSphere.transform;
            newMaterialPoint.name = String.Format("Mat_Point:{0}", id++);
            materialPoints.Add(newMaterialPoint);
        }
       
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
	}

	

    private double DegreeToRadian(double angle)
    {
        return Math.PI * angle / 180.0;
    }

}
