using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameInput input;
    [SerializeField] float moveSpeed = 7F;
    [SerializeField] float rotateSpeed = 5F;
    private bool isWalking;

    // Update is called once per frame
    void Update()
    {
        var direction = input.GetDirection();
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, direction, rotateSpeed * Time.deltaTime);
        isWalking = !direction.Equals(Vector3.zero);
    }


    public bool IsWalking() => isWalking;
}
