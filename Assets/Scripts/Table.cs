using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour, IInteracPlayer
{
    [SerializeField] GameObject activeWhenInteraction = null;

    public bool Interaction(Player player)
    {
        var isInterecEnter = player != null;
        if (activeWhenInteraction.IsUnityNull()) return false;
        activeWhenInteraction.gameObject.SetActive(isInterecEnter);
        return true;
    }
}
