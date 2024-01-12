using Assets.Scripts;
using System;
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
        if (activeWhenInteraction.IsUnityNull())
        {
            this.gameObject.SetActive(!isInterecEnter);
        }
        else
        {
            //table parent
            activeWhenInteraction.gameObject.SetActive(isInterecEnter);
        }
    }
}
