using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameInput input;
    [SerializeField] float moveSpeed = 7F;
    [SerializeField] float rotateSpeed = 10F;

    float moveDistance => moveSpeed * Time.deltaTime;

    public IInteracPlayer LastInteracPlayer = null;
    public bool IsWalking = false;

    private void Awake()
    {
        input.OnInteraction += Input_OnInteraction;
    }

    private void Input_OnInteraction(object sender, EventArgs e)
    {

    }


    // Update is called once per frame
    void Update()
    {
        var direction = input.GetDirection();
        HandleMovement(direction);
        HandleInteraction(direction);
    }

    private void HandleInteraction(Vector3 direction)
    {
        if (!direction.Equals(Vector3.zero))
        {
            var coliider = GetComponent<BoxCollider>();
            if (Physics.Raycast(transform.position, direction, out var hitInfo, maxDistance: coliider.size.x))
            {
                LastInteracPlayer = hitInfo.transform.GetComponent<IInteracPlayer>();
            }
            else
            {
                LastInteracPlayer?.Interaction(null);
                LastInteracPlayer = null;
            }
        }
        LastInteracPlayer?.Interaction(this);
    }

    private void HandleMovement(Vector3 direction)
    {
        var coliider = GetComponent<BoxCollider>();

        //walking
        IsWalking = !direction.Equals(Vector3.zero);
        if (IsWalking) transform.forward = Vector3.Slerp(transform.forward, direction, rotateSpeed * Time.deltaTime);

        //move
        var playerRadius = coliider.size.x / 2;
        var playerHeight = transform.position + Vector3.up * coliider.size.x;
        var directions = new[] {
            direction,
            new Vector3(direction.x, 0, 0).normalized,
            new Vector3(0, 0, direction.z).normalized,
        };
        foreach (var item in directions)
        {
            if (IsWalking && !Physics.CapsuleCast(transform.position, playerHeight, playerRadius, item, moveDistance))
            {
                transform.position += item * moveDistance;
                break;
            }
        }
    }

}
