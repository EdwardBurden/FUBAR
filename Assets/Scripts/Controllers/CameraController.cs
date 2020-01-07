using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float fixedDeltaTime;
    public static CameraController instance;
    public bool Follow;
    public Camera MainCamera;
    public float NormalSpeed;
    public float SprintSpeed;

    private float MovementSpeed;
    public float MovementTime;

    public float RotationStrength;
    public Vector3 ZoomStrength;

    private Vector3 TargetPosition;
    private Quaternion TargetRotation;
    private Vector3 TargetZoom;

    private Vector3 DragStartPosition;
    private Vector3 DragCurrentPosition;

    private Vector3 RotateStartPosition;
    private Vector3 RotateCurrentPosition;

    private void Start()
    {
        instance = this;
        TargetPosition = transform.position;
        TargetRotation = transform.rotation;
        TargetZoom = MainCamera.transform.localPosition;
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Follow && SelectionController.instance.IsSelectionFollowable())
        {
            transform.position = SelectionController.instance.Selected[0].transform.position;
        }
        else
        {
            HandleMouseInput();
            HandleKeyInput();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Follow = false;
        }
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            Follow = true;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            TargetZoom += Input.mouseScrollDelta.y * ZoomStrength;
        }

        /*if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                DragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(1))
        {

            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                DragCurrentPosition = ray.GetPoint(entry);
                TargetPosition = transform.position + DragStartPosition - DragCurrentPosition;
            }
        }*/

        if (Input.GetMouseButtonDown(2))
        {
            RotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            RotateCurrentPosition = Input.mousePosition;
            Vector3 differnece = RotateStartPosition - RotateCurrentPosition;
            RotateStartPosition = RotateCurrentPosition;
            TargetRotation *= Quaternion.Euler(Vector3.up * (-differnece.x / 5.0f));
        }
    }

    private void HandleKeyInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            MovementSpeed = SprintSpeed;
        else
            MovementSpeed = NormalSpeed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            TargetPosition += (transform.forward * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            TargetPosition -= (transform.forward * MovementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            TargetPosition += (transform.right * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            TargetPosition -= (transform.right * MovementSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            TargetRotation *= Quaternion.Euler(Vector3.up * RotationStrength);
        }
        if (Input.GetKey(KeyCode.E))
        {
            TargetRotation *= Quaternion.Euler(Vector3.up * -RotationStrength);
        }

        if (Input.GetKey(KeyCode.R))
        {
            TargetZoom += ZoomStrength;
        }
        if (Input.GetKey(KeyCode.F))
        {
            TargetZoom -= ZoomStrength;
        }
        if (Time.timeScale == 0.2f)
            Time.timeScale = 1.0f;
        // Adjust fixed delta time according to timescale
        // The fixed delta time will now be 0.02 frames per real-time second

        if (Input.GetKey(KeyCode.L))
        {

            Time.timeScale = 0.2f;

        }
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;

        transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * MovementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, Time.deltaTime * MovementTime);
        MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, TargetZoom, Time.deltaTime * MovementTime);
    }
}
