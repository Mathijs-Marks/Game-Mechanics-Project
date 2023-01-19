using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveColumn : MonoBehaviour
{
    [SerializeField] private Lever lever;

    private void OnEnable()
    {
        lever.leverSwitched.AddListener(deleteObject);
    }

    private void OnDisable()
    {
        lever.leverSwitched.RemoveListener(deleteObject);
    }

    private void deleteObject()
    {
        Destroy(gameObject);
    }
}
