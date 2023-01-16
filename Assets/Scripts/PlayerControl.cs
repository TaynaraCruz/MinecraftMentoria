using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float playerJumpSpeed = 5f;
    [SerializeField] private float playerJumpTime = 0.5f;
    [SerializeField] private float playerRotationSpeed = 50f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityIntensity = 0.5f;
    [SerializeField] private GameObject firstPersonView;
    [SerializeField] private CharacterController character;
    [SerializeField] private GameObject inventoryUI;
    private bool _inventoryUIToggle = false;

    private float _jumpTimeCounter;
    private float _gravity = 0f;
    private float _playerVerticalAngleView = 0f;

    private void Update()
    {
        MovePlayerView();
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventoryUIToggle = !_inventoryUIToggle;
            inventoryUI.SetActive(_inventoryUIToggle);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        /*TransformDirection --> transforma coordenada de local para global
          InverseTransformDirection transforma coordenada de global para local
        */
        var globalTransform = transform.TransformDirection(new Vector3(
            Input.GetAxis("Horizontal") * playerSpeed * Time.fixedDeltaTime,
            _gravity,
            Input.GetAxis("Vertical") * playerSpeed * Time.fixedDeltaTime
        ));
        
        if(_jumpTimeCounter > 0)
        {
            _jumpTimeCounter -= Time.fixedTime;
            _gravity = jumpTime * jumpForce;
        }

        if (character.isGrounded && _jumpTimeCounter <= 0)
        {
            _jumpTimeCounter = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                globalTransform.y += playerJumpSpeed * Time.fixedDeltaTime;
                _jumpTimeCounter = jumpTime;
            }
        }
        
        character.Move(globalTransform);
        _gravity = Mathf.Lerp(_gravity, Physics.gravity.y, Time.fixedDeltaTime * gravityIntensity);
        
    }

    private void MovePlayerView()
    {
        //horizontal rotation
        transform.Rotate(0, Input.GetAxis("Mouse X") * playerRotationSpeed, 0);
        
        //vertical rotation
        _playerVerticalAngleView += (-Input.GetAxis("Mouse Y") * Time.deltaTime * 180);
        
        var rotation = firstPersonView.transform.rotation;
        
        firstPersonView.transform.rotation = Quaternion.Euler(Mathf.Clamp(_playerVerticalAngleView, -75, 75), 
            rotation.eulerAngles.y, rotation.eulerAngles.z);
    }
}
