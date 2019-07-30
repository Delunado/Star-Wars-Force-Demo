using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot;

    [SerializeField] private bool invert;

    [SerializeField] private bool canUnlock = true;

    [SerializeField] private float sensivity = 5f;

    [SerializeField] private int smoothSteps = 10;

    [SerializeField] private float smoothWeight = 0.4f;

    [SerializeField] private float rollAngle = 10f;

    [SerializeField] private float rollSpeed = 3f;

    [SerializeField] private Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    private Vector2 lookAngles;

    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;

    private int lastLookFrame;

    void Start()
    {
        //Blocks the cursor in the middle
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LockAndUnlockCursor();

        //We can only look around if the cursor is locked.
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }

    private void LookAround()
    {
        //Axis Y and X are interchanged because rotation works contrary to normal. If you rote on X you look down/up and if you rote on Y you look sideways.     
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.VERTICAL_CAMERA), Input.GetAxis(MouseAxis.HORIZONTAL_CAMERA));

        lookAngles.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.HORIZONTAL_CAMERA) * rollAngle, Time.deltaTime * rollSpeed);

        //lookRoot only controls Y axis, looking down and up. The playerRoot turn on itself, allowing the camera to look sideways.
        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);

    }

}
