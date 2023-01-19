using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FocalPoint : MonoBehaviour
{
    public GameObject player;
    public Camera mainCam;
    public CinemachineVirtualCamera virtualCam;
    public float minXRot;
    public float maxXRot;
    public float xCamSpeed;
    public float yCamSpeed;
    public float zoomSpeed;
    public float zoomMin;
    public float zoomMax;
    public float smoothness;
    public float clickDelay;

    private float lastClick;
    private float desiredZoom;
    private Quaternion desiredRotation;
    private bool doubleClicked;
    private Vector3 prevPos;
    private CinemachineTransposer offset;

    private void Start()
    {
        offset = virtualCam.GetCinemachineComponent<CinemachineTransposer>();
        mainCam = Camera.main;
        transform.eulerAngles = new Vector3(45, 0, 0);
        desiredZoom = -30;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;


        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time - lastClick <= clickDelay)
            {
                doubleClicked = true;
                desiredRotation = Quaternion.Euler(45, 0, 0);
                desiredZoom = -30;
            }
            lastClick = Time.time;
        }
        else if (Input.GetMouseButton(1) && (!doubleClicked && !(Input.GetMouseButton(0) || Input.GetMouseButton(2))))
        {
            Vector3 direction = prevPos - mainCam.ScreenToViewportPoint(Input.mousePosition);

            float xDir = direction.x * 180;
            float yDir = direction.y * 180;
            if (yDir > 0.9f) { yDir = 0.9f; }
            if (yDir < -0.9f) { yDir = -0.9f; }

            float rotX = transform.eulerAngles.x + yDir * xCamSpeed;

            if (rotX > maxXRot) { rotX = maxXRot; }
            if (rotX < minXRot) { rotX = minXRot; }
            desiredRotation = Quaternion.Euler(rotX, transform.eulerAngles.y - xDir * yCamSpeed, 0);
        }
        if (Input.GetMouseButtonUp(1))
        {
            doubleClicked = false;
        }
        prevPos = mainCam.ScreenToViewportPoint(Input.mousePosition);

        float scroll = Input.mouseScrollDelta.y;

        float actualZoom = offset.m_FollowOffset.z;

        if (scroll != 0)
        {
            if (scroll > 0 && desiredZoom <= -zoomMax)
            {
                desiredZoom += zoomSpeed;
            }
            if (scroll < 0 && desiredZoom >= -zoomMin)
            {
                desiredZoom -= zoomSpeed;
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothness);

        offset.m_FollowOffset = Vector3.Lerp
            (
                new Vector3(0, 0, actualZoom),
                new Vector3(0, 0, desiredZoom),
                smoothness
            );
    }

}