using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : InteractableObject
{
    // TODO: Do something with a linked object to the lever
    // (e.g. open/close doors, extend bridge, activate moving platform)

    public UnityEvent leverSwitched;

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        leverSwitched.Invoke();
    }
}
