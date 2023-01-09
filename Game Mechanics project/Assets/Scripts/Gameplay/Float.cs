// Credit to Donovan Keith
// Taken from [his webpage](http://www.donovankeith.com/2016/05/making-objects-float-up-down-in-unity/)
// [MIT License](https://opensource.org/licenses/MIT)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    // Sinus modifiers
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float degreesPerSecond = 15.0f;
    [SerializeField] private bool canRotate = false;

    // Position Storage Variables
    Vector2 posOffset = new Vector2();
    Vector2 tempPos = new Vector2();

    // Use this for initialization
    void Start()
    {
        // Store the starting position of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-axis
        if (canRotate)
        {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        }

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
