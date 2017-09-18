using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public class Spring : MonoBehaviour
{
    public GameObject node1, node2;
    public float stifness; //k
   
  
    public float restDistance; //d
    public float damping; //b
    Vector3 force = Vector3.zero;


    private void Start()
    {
        
    }
    //    F = -k(|x|-d)(x/|x|) - bv
    private void Update()
    {
        float distance = Vector3.Distance(node1.transform.position, node2.transform.position);
        if (distance < restDistance) return;

        Vector3 force1 = -stifness * (distance - restDistance) * (Vector3.Normalize(node2.transform.position - node1.transform.position) / distance) - damping * (node1.GetComponent<MaterialPointController>().physics.Velocity - node2.GetComponent<MaterialPointController>().physics.Velocity);
        Vector3 force2 = -stifness * (distance - restDistance) * (Vector3.Normalize(node1.transform.position - node2.transform.position) / distance) - damping * (node2.GetComponent<MaterialPointController>().physics.Velocity - node1.GetComponent<MaterialPointController>().physics.Velocity);

        node1.GetComponent<MaterialPointController>().physics.Acceleration += force1 / 1f; //force or acc?
        node2.GetComponent<MaterialPointController>().physics.Acceleration += force2 / 1f;


        //node1.GetComponent<MaterialPointController>().physics.Velocity += node1.GetComponent<MaterialPointController>().physics.Acceleration;
        //node1.GetComponent<MaterialPointController>().physics.Position += node1.GetComponent<MaterialPointController>().physics.Velocity;
        //node1.GetComponent<MaterialPointController>().physics.Velocity *= 0.97f;
        //node1.GetComponent<MaterialPointController>().physics.Acceleration *= 0.3f;
        //node1.transform.position = node1.GetComponent<MaterialPointController>().physics.Position;

        //node2.GetComponent<MaterialPointController>().physics.Velocity += node2.GetComponent<MaterialPointController>().physics.Acceleration;
        //node2.GetComponent<MaterialPointController>().physics.Position += node2.GetComponent<MaterialPointController>().physics.Velocity;
        //node2.GetComponent<MaterialPointController>().physics.Velocity *= 0.97f;
        //node2.GetComponent<MaterialPointController>().physics.Acceleration *= 0.3f;
        //node2.transform.position = node2.GetComponent<MaterialPointController>().physics.Position;

        //move


        this.GetComponent<LineRenderer>().SetPositions(new Vector3[] { node1.transform.position, node2.transform.position });
    }

   

}

