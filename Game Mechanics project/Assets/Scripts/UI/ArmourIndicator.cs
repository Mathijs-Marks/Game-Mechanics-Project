using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourIndicator : MonoBehaviour
{
    [SerializeField] private GameObject armourIndicator;

    private void Update()
    {
        if (GameManager.Instance.HasArmour)
        {
            armourIndicator.SetActive(true);
        }
        else
        {
            armourIndicator.SetActive(false);
        }
    }
}
