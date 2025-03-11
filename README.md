## Demo Image 🖼️
![AR_Project](https://github.com/Asbaq/AR_Project/assets/sample-image.png)

---

# AR_Project Documentation 📄

## Overview 🌍
This document provides an explanation of the **AR_Project**, which is an **AR-based object spawning and manipulation** script using **Unity** and **AR Foundation**. This project allows users to spawn an object in **Augmented Reality (AR)** using a simple touch and scale it using pinch gestures.

---

🔗 Video Trailer

https://youtube.com/shorts/7I_f1HF11kk?feature=share

---

## Features 🚀
- **Spawn an Object in AR** 🎯
- **Scale the Object Using Pinch Gesture** 📏
- **Real-time AR Raycasting for Surface Detection** 🛠️

---

## Dependencies 📦
- **Unity 2020 or Later**
- **AR Foundation Package**
- **ARCore (Android) or ARKit (iOS)**
- **XR ARSubsystems**

---

## Code Breakdown 🧩

### 1️⃣ Variables Declaration 📌
```csharp
public GameObject spwan_prefab;  // The object to be spawned
GameObject spawned_object;       // Stores the spawned object
bool object_spawned;             // Checks if the object is spawned
ARRaycastManager arrays;         // AR Raycast manager instance
Vector2 First_touch, Second_touch; // Stores touch positions
float distance_current, distance_previous; // Stores pinch distances
bool First_pinch = false;        // Flag for detecting first pinch
List<ARRaycastHit> hits = new List<ARRaycastHit>(); // List for storing AR raycast results
```

### 2️⃣ Start Method 🚦
```csharp
void Start()
{
    object_spawned = false;  // Initialize object as not spawned
    arrays = GetComponent<ARRaycastManager>(); // Get AR Raycast Manager component
}
```

### 3️⃣ Object Spawning 🛠️
```csharp
if(Input.touchCount > 0 && !object_spawned)
{
    if(arrays.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
    {
        var hitpose = hits[0].pose;
        spawned_object = Instantiate(spwan_prefab, hitpose.position, hitpose.rotation);
        object_spawned = true;
    }
}
```
📌 **Explanation:**
- If the user taps on a detected **plane**, a **3D object** is spawned at that position.
- Uses **ARRaycastManager** to detect **real-world surfaces**.

### 4️⃣ Object Scaling (Pinch to Zoom) 🤏
```csharp
if(Input.touchCount > 1 && object_spawned)
{
    First_touch = Input.GetTouch(0).position;
    Second_touch = Input.GetTouch(1).position;
    distance_current = Second_touch.magnitude - First_touch.magnitude;

    if(First_pinch)
    {
        distance_previous = distance_current;
        First_pinch = false;
    }
    
    if(distance_current != distance_previous)
    {
        Vector3 scale_value = spawned_object.transform.localScale * (distance_current / distance_previous);
        spawned_object.transform.localScale = scale_value;
        distance_previous = distance_current;
    }
}
else
{
    First_pinch = true;
}
```
📌 **Explanation:**
- Uses **two-finger pinch gesture** to scale the spawned object.
- Computes the **difference** between touch distances to scale the object.

---

## How It Works? ⚙️
1️⃣ **User taps** on a detected surface to **spawn an object**. 📌
2️⃣ The object **appears** at the detected position. 🎭
3️⃣ **User pinches** with two fingers to **scale the object**. 🔍

---

## Conclusion 🏁
This **AR_Project** provides a basic yet effective **AR-based object spawning and scaling** system using **Unity AR Foundation**. It can be **extended further** to include **rotation, movement, and other AR interactions**! 🎮🚀
