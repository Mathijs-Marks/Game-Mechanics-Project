using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYAxis : MonoBehaviour
{
    [SerializeField] private float speed = .5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 180, 0 * speed * Time.deltaTime);
    }
}
