using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CenterCamera : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform XROrigin;
    [SerializeField] private Transform target;

    [SerializeField] private InputActionReference centerButton;

    // Update is called once per frame
    void Update()
    {
        if (centerButton.action.WasPressedThisFrame())
        {
            Vector3 offset = head.position - XROrigin.position;
            offset.y = 0;
            XROrigin.position = target.position - offset;

            Vector3 targetForward = target.forward;
            targetForward.y = 0;
            Vector3 cameraForward = head.forward;
            cameraForward.y = 0;

            float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);

            XROrigin.RotateAround(head.position, Vector3.up, angle);
        }
    }
}
