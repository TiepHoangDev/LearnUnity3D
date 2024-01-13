using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameInput input;
    [SerializeField] float moveSpeed = 7F;
    [SerializeField] float rotateSpeed = 10F;

    float moveDistance => moveSpeed * Time.deltaTime;

    public IPlayerSelected LastInteracPlayer = null;
    public bool IsWalking = false;

    private void Awake()
    {
        if (input != null) input.OnInteraction += Input_OnInteraction;
    }

    private void Input_OnInteraction(object sender, EventArgs e)
    {
        LastInteracPlayer?.Interaction(this);
    }


    // Update is called once per frame
    void Update()
    {
        var direction = input?.GetDirection() ?? Vector3.zero;
        var raycastHit = HandleMovement(direction);
        HandleInteraction(raycastHit);
    }

    private void HandleInteraction(RaycastHit? raycastHit)
    {
        if (IsWalking)
        {
            if (raycastHit == null)
            {
                LastInteracPlayer?.Selected(null);
                LastInteracPlayer = null;
                return;
            }

            var interacPlayer = raycastHit?.transform.GetComponent<IPlayerSelected>();
            if (interacPlayer?.Selected(this) ?? false)
            {
                if (interacPlayer != LastInteracPlayer)
                {
                    LastInteracPlayer?.Selected(null);
                    LastInteracPlayer = interacPlayer;
                }
            }
        }
    }

    private RaycastHit? HandleMovement(Vector3 direction)
    {
        var coliider = GetComponent<BoxCollider>();

        //walking
        IsWalking = !direction.Equals(Vector3.zero);
        if (IsWalking) transform.forward = Vector3.Slerp(transform.forward, direction, rotateSpeed * Time.deltaTime);

        //move
        var playerRadius = coliider.size.x / 2;
        var playerHeight = transform.position + Vector3.up * coliider.size.x;
        var directions = new List<Vector3>  {
            direction,
            new Vector3(direction.x, 0, 0).normalized,
            new Vector3(0, 0, direction.z).normalized,
        };
        directions = directions.Where(q => q != Vector3.zero).ToList();

        if (!IsWalking) return null;

        RaycastHit raycastHit = default;
        foreach (var item in directions)
        {
            if (!Physics.CapsuleCast(transform.position, playerHeight, playerRadius, item, out raycastHit, moveDistance))
            {
                transform.position += item * moveDistance;
                return null;
            }
        }
        return raycastHit;
    }

}
