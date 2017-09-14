using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public class SphericalPointsDistribution
{
    //https://en.wikipedia.org/wiki/Spherical_coordinate_system
    public static List<Vector3> GetCartesianCoordinates(float radius, int angleStep)
    {
        List<Vector3> points = new List<Vector3>();
        for (int azimuth = 0; azimuth < 360; azimuth += angleStep)
        {
            for (int inclination = 0; inclination < 180; inclination += angleStep)
            {
                double x = radius * Math.Sin(DegreeToRadian(inclination)) * Math.Cos(DegreeToRadian(azimuth));
                double y = radius * Math.Sin(DegreeToRadian(inclination)) * Math.Sin(DegreeToRadian(azimuth));
                double z = radius * Math.Cos(DegreeToRadian(inclination));

                points.Add(new Vector3((float)x, (float)y, (float)z));              
            }
        }
        return points;
    }

    public static List<Vector3> FibonacciSphere(int samples, int scale)
    {
        List<Vector3> points = new List<Vector3>();
        double offset = 2f / samples;
        double increment = Math.PI * (3f - Math.Sqrt(5f));
        for (int i = 0; i < samples; i++)
        {
            double y = ((i * offset) - 1) + (offset / 2);
            double r = Math.Sqrt(1 - Math.Pow(y, 2));
            double phi = ((i + 1) % samples) * increment;

            double x = Math.Cos(phi) * r;
            double z = Math.Sin(phi) * r;

            points.Add(new Vector3((float)x, (float)y, (float)z) * scale);

        }



        return points;
    }

    public static double DegreeToRadian(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}

