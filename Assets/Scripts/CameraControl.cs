using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject thirdPersonTarget;
    [SerializeField] private GameObject firstPersonTarget;
    [SerializeField] private GameObject firstPersonCrossHair;
    [SerializeField] private GameObject thirdPersonCrossHair;
    [SerializeField] private float cameraSpeed = 10f;

    private bool _firstPerson = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _firstPerson = !_firstPerson;
            firstPersonCrossHair.SetActive(_firstPerson);
            thirdPersonCrossHair.SetActive(!_firstPerson);

        }

        transform.position = _firstPerson
            ? Vector3.Lerp(transform.position, firstPersonTarget.transform.position, cameraSpeed * Time.deltaTime)
            : Vector3.Lerp(transform.position, thirdPersonTarget.transform.position, cameraSpeed * Time.deltaTime);

        if (!_firstPerson)
        {
            transform.LookAt(player.transform);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, firstPersonTarget.transform.rotation,
                cameraSpeed * Time.deltaTime);
        }
    }
}
