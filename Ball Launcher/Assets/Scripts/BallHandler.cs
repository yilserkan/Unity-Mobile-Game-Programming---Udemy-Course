using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] float detachDelay = 0.5f;
    [SerializeField] float respawnDelay = 1f;

    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSprintJoint;

    private Camera mainCamera;
    private bool isDragging;

    void Start() 
    {
        mainCamera = Camera.main;

        SpawnNewBall();
    }

    private void OnEnable() 
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable() {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if(currentBallRigidbody == null) { return; }

        if(Touch.activeTouches.Count == 0)
        {
            if(isDragging)
            {
                LaunchBall();
            }

            isDragging = false;

            return;
        }

        currentBallRigidbody.isKinematic = true;

        isDragging = true;

        Vector2 touchPosition = new Vector2();

        foreach(Touch touch in Touch.activeTouches)
        {
            touchPosition += touch.screenPosition;
        }

        touchPosition /= Touch.activeTouches.Count;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidbody.position = worldPosition;
    }

    void SpawnNewBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.transform.position, Quaternion.identity);

        currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSprintJoint = ballInstance.GetComponent<SpringJoint2D>();

        currentBallSprintJoint.connectedBody = pivot;
    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;

        Invoke(nameof(DetachBall), detachDelay);

    }

    private void DetachBall()
    {
        currentBallSprintJoint.enabled = false;
        currentBallSprintJoint = null;

        Invoke(nameof(SpawnNewBall), respawnDelay);
    }
}
