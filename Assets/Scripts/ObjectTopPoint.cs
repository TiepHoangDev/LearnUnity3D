using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTopPoint : MonoBehaviour, IInteracPlayer
{
    [SerializeField] private TableObjectTopPointSO objectToShow;
    [SerializeField] Transform positionToShow;

    public bool Interaction(Player player)
    {
        if (objectToShow == null) return false;

        var objectToShowTransform = Instantiate(objectToShow.objectToShow, positionToShow);
        objectToShowTransform.transform.localPosition = Vector3.zero;
        return true;
    }
}
