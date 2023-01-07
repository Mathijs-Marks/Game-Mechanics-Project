using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    // Character damaged and amount damaged
    public static UnityAction<GameObject, int> characterDamaged;

    // Character healed and amount healed
    public static UnityAction<GameObject, int> characterHealed;
}
