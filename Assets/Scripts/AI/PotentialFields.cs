using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace AI
{
    /*
     * This is a reactive path planning for an intelligent agent that wants to reach a target avoiding obstacles.
     * It does not calculate the best path to the target.
     */
    public class PotentialFields : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] Rigidbody rdb;
        
        // Gain constants
        [SerializeField] private float attractionGain = 0.1f;
        [SerializeField] private float repulsiveGain = 0.1f;
        
        
        [SerializeField] private float distance = 3;
        [SerializeField] private int angle = 30;
        [SerializeField] private int numberOfRays = 10;
        
        private float stoppedTime = 0;
        private float resolvedTime;
        
        private void FixedUpdate()
        {
            MoveToTarget();
        }
        private void MoveToTarget()
        {
            AttractionForce();
            RepulsiveForce();
        }

        private void AttractionForce()
        {
            var targetDistanceVector = target.transform.position - transform.position;
            var attractionForce = Vector3.forward * attractionGain;
            
            Debug.Log(Vector3.Dot(transform.right, targetDistanceVector.normalized));
            var direction = math.clamp(Vector3.Dot(transform.right, targetDistanceVector.normalized), -0.5f, 0.5f);

            if (stoppedTime > 3)
            {
                ReverseForce();
            }
            else
            {
                rdb.AddRelativeTorque(Vector3.up * direction, ForceMode.VelocityChange);
                rdb.AddRelativeForce(attractionForce, ForceMode.VelocityChange);
            }
            
            if (rdb.velocity.magnitude < 0.1f)
            {
                stoppedTime += Time.fixedDeltaTime;
            }

            
        }

        private void ReverseForce()
        {
            
                rdb.AddRelativeForce(Vector3.forward * -attractionGain, ForceMode.VelocityChange);
                rdb.AddTorque(Vector3.up * -attractionGain, ForceMode.VelocityChange);
                resolvedTime += Time.fixedDeltaTime;
                // Reset stopped and resolved time after 3 seconds
                if (resolvedTime > 3)
                {
                    stoppedTime = 0;
                    resolvedTime = 0;
                }
        }

        private void RepulsiveForce()
        {
            var radius = angle / 2;
            var rayIncrement = angle / numberOfRays;
            for (int i = -radius; i < radius; i+=rayIncrement)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(i, 0, 50)),
                        out var hit, distance))
                {
                    if (i <= 0)
                    {
                        rdb.AddRelativeTorque(Vector3.up * repulsiveGain, ForceMode.VelocityChange);
                    }
                    else {
                        rdb.AddRelativeTorque(Vector3.up * -repulsiveGain, ForceMode.VelocityChange);
                    }
                    Debug.DrawLine(transform.position, hit.point);
                }
            }
        }

    }
}
