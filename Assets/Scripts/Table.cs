using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour, IPlayerSelected
{
    [SerializeField] GameObject activeWhenInteraction = null;
    [SerializeField] ObjectTopPoint interacPlayer = null;

    public bool Interaction(Player player)
    {
        return interacPlayer?.Interaction(player) ?? false;
    }

    public bool Selected(Player player)
    {
        var isInterecEnter = player != null;
        if (activeWhenInteraction.IsUnityNull()) return false;
        activeWhenInteraction.gameObject.SetActive(isInterecEnter);
        return true;
    }
}
