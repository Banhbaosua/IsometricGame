using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector3 _input;
    public Vector3 INput => _input;
    private void Update()
    {
        GetInput();
    }
    void GetInput()
    {
        _input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
}
