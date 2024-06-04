using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
[RequireComponent(typeof(CharacterData))]
public class Player : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _turnSpeed = 360f;
    [SerializeField] private CharacterController _characterController;
   
    //Getter
    public CharacterController CharController => _characterController;
    public float TurnSpeed => _turnSpeed;
    public float MoveSpeed => _moveSpeed;
    //

    [Header("Animation Smooth")]
    [Range(0f, 1f)]
    [SerializeField] private float speedDampTime = 0.05f;

    private Vector3 _velocity;
    private PlayerInput _playerInput;
    public PlayerInput PlayerInput => _playerInput;

    private InputAction _moveInput;
    public InputAction MoveInput => _moveInput;
    public float AnimSpeedDampTime => speedDampTime;
    private Vector3 _input;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponentInChildren<CharacterController>();
        _moveInput = _playerInput.actions["Move"];

    }
}
public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    public static Vector3 VelocityCal(this bool isGrounded)
    {
        var velocity = new Vector3(0, 0, 0);
        if (isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity += Physics.gravity * Time.deltaTime;
        }
        return velocity;
    }
}
