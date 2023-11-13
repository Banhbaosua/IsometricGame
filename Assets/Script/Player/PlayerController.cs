using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rgbd;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _turnSpeed = 360f;
    private Vector3 _input;

    void Update()
    {
        GetInput();
        Look();

    }
    void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        _input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        if (_input != Vector3.zero) 
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);

            var relative = (_transform.position + skewedInput) - _transform.position;
            var rota = Quaternion.LookRotation(relative, Vector3.up);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation,rota, _turnSpeed*Time.deltaTime);
            Move();
        }

    }
    void Move()
    {
        _rgbd.MovePosition(_transform.position + _input.ToIso() * _input.normalized.magnitude * _speed * Time.deltaTime);
    }
}


public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
