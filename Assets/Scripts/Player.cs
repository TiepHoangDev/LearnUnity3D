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
    private bool isWalking;

    // Update is called once per frame
    void Update()
    {
        var direction = input.GetDirection();
        var coliider = GetComponent<BoxCollider>();

        //walking
        isWalking = !direction.Equals(Vector3.zero);
        transform.forward = Vector3.Slerp(transform.forward, direction, rotateSpeed * Time.deltaTime);

        //move
        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = coliider.size.x / 2;
        var playerHeight = transform.position + Vector3.up * coliider.size.x;
        var directions = new[] {
            direction,
            new Vector3(direction.x, 0, 0).normalized,
            new Vector3(0, 0, direction.z).normalized,
        };
        foreach (var item in directions)
        {
            if (isWalking && !Physics.CapsuleCast(transform.position, playerHeight, playerRadius, item, moveDistance))
            {
                transform.position += item * moveDistance;
                break;
            }
        }

    }


    public bool IsWalking() => isWalking;
}
