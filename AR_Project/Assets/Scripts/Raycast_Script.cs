using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class Raycast_Script : MonoBehaviour
{   
    // Declaring and Initializing GameObject
    public GameObject spwan_prefab;
    GameObject spawned_object;
    bool object_spawned;
    ARRaycastManager arrays;
    Vector2 First_touch;
    Vector2 Second_touch;
    float distance_current;
    float distance_previous;
    bool First_pinch = false;
    List <ARRaycastHit> hits = new List<ARRaycastHit>();


    // Start is called before the first frame update
    void Start()
    {
        // Initializing object_spawned and arrays
        object_spawned = false;
        arrays = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the cube is not visible on the screen  
        if(Input.touchCount > 0 && !object_spawned)
        {
            // To visible the cube by touching
            if(arrays.Raycast(Input.GetTouch(0).position,hits,TrackableType.PlaneWithinPolygon))
            {   
                var hitpose = hits[0].pose;
                // Instantiating the Cube/Object
                spawned_object = Instantiate(spwan_prefab,hitpose.position,hitpose.rotation);
                object_spawned = true;
            }
        }
                
        // If cube is visible screen 
        if(Input.touchCount>1 && object_spawned)
        {
            First_touch = Input.GetTouch(0).position;
            Second_touch = Input.GetTouch(1).position;
            // distance between the the fingers or thumb
            distance_current = Second_touch.magnitude-First_touch.magnitude;

            // for initializing distance to distance_previous variable
            if(First_pinch)
            {
                distance_previous = distance_current;
                First_pinch = false;
            }
            
            // If the distance is not equal to prevoius distance
            if(distance_current != distance_previous)
            {
                // for transforming the Cube/Object
                Vector3 scale_value = spawned_object.transform.localScale * (distance_current/distance_previous);
                spawned_object.transform.localScale = scale_value;
                distance_previous = distance_current;
            }
        }
        else
        {
            First_pinch = true;
        }
        
    }

}

