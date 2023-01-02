using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Based on AdamCYounis's original parallax script: https://www.youtube.com/watch?v=tMXgLBwtsvI
// Created using Chris' Tutorials video: https://youtu.be/bhR4d2KgNO4

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform followTarget;

    // Starting position for the parallax gameObject.
    private Vector2 startingPosition;

    // Start Z value for the parallax gameObject.
    private float startingZValue;

    // Distance the camera has moved from the starting position of the parallax object.
    private Vector2 camMoveSinceStart => (Vector2)camera.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // If object is in front of the target, use near clip plane. If behind target, use far clip plane.
    private float clippingPlane => (camera.transform.position.z + (zDistanceFromTarget > 0 ? camera.farClipPlane : camera.nearClipPlane));

    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPosition= transform.position;
        startingZValue = transform.position.z;
    }

    void Update()
    {
        // When the target moves, move the parallax object for the same distance * a multiplier.
        Vector2 newPosition = startingPosition + (camMoveSinceStart * parallaxFactor);

        // The X/Y position changes based on target travel speed * the parallax factor, but Z stays consistent.
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZValue);
    }

}
