using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any object that needs to be accessed globally is documented here.
/// Note that all references are static, as we only want 1 instance of that reference.
/// </summary>

public static class GlobalReferenceManager
{
    public static PlayerController PlayerScript;
    public static CheckSurfaces CheckSurfacesScript;
    public static ItemCollector ItemCollectorScript;
    public static NPCInteraction NPCInteractionScript;
    public static DamageController DamageControllerScript;
}
