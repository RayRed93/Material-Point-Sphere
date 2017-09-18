using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace BallSimulation
//{
    public class MaterialPointPhysics
    {
        public Vector3 Velocity { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Acceleration { get; set; }
        public float Mass { get; set; }

        private Vector3 PreviousPosition;
        private Vector3 NextPosition;
        private Vector3 NextVelocity;
        public Vector3 Force; 
        
        private int step = 0;
       
        public MaterialPointPhysics(Vector3 startPosition, List<Vector3> springConnections, float mass)
        {
            this.Position = startPosition;
            this.Mass = mass;
        }

        private void MoveEuler(float stepTime)
        {
            NextVelocity = Velocity + Acceleration * stepTime;
            NextPosition = Position + NextVelocity * stepTime;
        }

        private void MoveVerlet(float stepTime)
        {
            if (step == 0)
            {
                MoveEuler(stepTime);
            }
            else 
            {
                NextPosition = Position * 2f - PreviousPosition + Acceleration * (float)Math.Pow(stepTime, 2);
                NextVelocity = (NextPosition - PreviousPosition) / (2f * stepTime); 
            }
            //nastepnePolozenie = 2.0 * polozenie - poprzedniePolozenie + przyspieszenie * SQR(krokCzasowy);
            //nastepnaPredkosc = (nastepnePolozenie - poprzedniePolozenie) / (2 * krokCzasowy);
        }
        public void AddForce(Vector3 force)
        {
        this.Force = force;
        Acceleration = Force / Mass;
        }
        public void Move(float stepTime)
        {
            //Acceleration = force / Mass;
        
            MoveEuler(stepTime);

            PreviousPosition = Position;
            Position = NextPosition;
            Velocity = NextVelocity;
            //Acceleration *= 0.3f; //friction
            Velocity *= 0.97f;
            step++;
        }

        public Vector3 springJointForce(Vector3[] springConnections, float ballanceDist)
        {
            Vector3 force = Vector3.zero;
            //float ballanceDistance = 1f;
            float k = 5f;


            for (int i = 0; i < springConnections.Length; i++)
            {
                float distance = Vector3.Distance(Position, springConnections[i]);
                if (distance < ballanceDist)
                {
                    return Vector3.zero;
                }
            force += -k * (distance - ballanceDist) * (Vector3.Normalize(Position - springConnections[i]));
            
            }
            
            return force * 1.2f;
        }
    }
//}
