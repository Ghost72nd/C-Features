﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Cue : MonoBehaviour
    {
        public Ball targetBall; //Target ball selected (which is generally the Cue ball)
        public float minPower = 0f; //The min power which maps to the distance
        public float maxPower = 20f; //The max power which maps to the distance
        public float maxDistance = 5f; //The maximum distance in units the Cue can be dragged back

        private float hitPower; //The final calculated hit power to fire the ball
        private Vector3 aimDirection; //The aim direction the ball should fire
        private Vector3 prevMousePos; //The mouse position obtained when left-clicking
        private Ray mouseRay; //The ray of the mouse

        // Use this for initialization
        void Start()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(targetBall.transform.position, targetBall.transform.position + aimDirection * hitPower);
        }

        //Rotates the cue to wherever the mouse is pointing
        void Aim()
        {
            //Calculate mouseRay before performing Raycast
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Raycast hit container for the hit information
            RaycastHit hit;
            //perform the Raycast
            if (Physics.Raycast(mouseRay, out hit))
            {
                //Obtain direction from the cue's position to the Raycast's hit point
                Vector3 dir = transform.position - hit.point;
                //Convert direction to angle in degrees
                float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                //Rotate towards that angle
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                //Position cue to the ball's position
                transform.position = targetBall.transform.position;
            }    
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            Aim();
            gameObject.SetActive(true);
        }
        
        void Drag()
        {
            //Store target ball's position in smaller variable
            Vector3 targetPos = targetBall.transform.position;
            //Get mouse position in world units
            Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Calculate distance from previous mouse position to the current mouse position
            float distance = Vector3.Distance(prevMousePos, currMousePos);
            //Clamp the distance between 0 - maxDistance
            distance = Mathf.Clamp(distance, 0, maxDistance);
            //Calculate a percentage for the distance
            float distpercentage = distance / maxDistance;
            //Use percentage of distance to map to the minPower - max-Power values
            hitPower = Mathf.Lerp(minPower, maxPower, distpercentage);
            //Position the cue back using distance
            transform.position = targetPos - transform.forward * distance;
            //Get direction to the target ball
            aimDirection = (targetPos - transform.position).normalized;
        }

        void Fire()
        {
            //Hit the ball with direction and power
            targetBall.Hit(aimDirection, hitPower);
            //Deactivate the cue when done
            Deactivate();
        }

        // Update is called once per frame
        void Update()
        {
            // Check if left mouse button is pressed
            if(Input.GetMouseButtonDown(0))
            {
                //store the click position as the "prevMousePos"
                prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            // Check if left mouse button is pressed
            if( Input.GetMouseButton(0))
            {
                // Perform drag mechanic
                Drag();
            }
            else
            {
                // Perform aim mechanic
                Aim();
            }

            // Check if the left mouse button is up
            if (Input.GetMouseButtonUp(0))
            {
                //Hit the ball
                Fire();
            }
        }
    }
}
