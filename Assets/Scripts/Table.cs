using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour, IInteracPlayer
{
    [SerializeField] GameObject activeWhenInteraction = null;

    public void Interaction(Player player)
    {
        var isInterecEnter = player != null;
        if (isInterecEnter && !activeWhenInteraction.IsUnityNull()) activeWhenInteraction.gameObject.SetActive(true);
        if (!isInterecEnter && activeWhenInteraction.IsUnityNull()) this.gameObject.SetActive(false);
    }
}
