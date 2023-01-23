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

    // Character receives a key
    public static UnityAction<int> keysReceived;

    // Character loses a key
    public static UnityAction<int> keysLost;

    // Character is in rollTooltipCollider
    public static UnityAction<GameObject, int> rollTooltip;

    // Character loses a life
    public static UnityAction<GameObject> characterLosesLife;

    // Character chooses level
    public static UnityAction<string> chooseLevel;
}
