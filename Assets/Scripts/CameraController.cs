using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public float initCameraDistance;
    public float minCameraDistance;
    public float maxCameraDistance;
    public float rotationSpeed;
    public float zoomSpeed;
    public float maxZoomSpeed;
    public float relativePFH;

    public Transform playerTransform;
    public Transform target;
    
    private Vector3 rotation;
    private Vector3 offset;
    private float scrolling;
    private float playerFocusHeight;
    private Vector3 cameraLookPosition;
    private RaycastHit hitInfo;
    private bool isAgainstWall = false;
    private bool isTargeting = false;
    
        
    private void Start() {

        playerFocusHeight = target.GetComponent<CharacterController>().height * relativePFH;
        offset = new Vector3(0f, 0f, -Mathf.Abs(initCameraDistance));
        rotation = new Vector3(0f, 0f, 0f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        target = playerTransform;

    }

    // Maybe update to make the camera motion cleaner
    private static Vector3 ValidCameraRotation(Vector3 rotation) {

        // Make adjustable in Unity?
        const float buffer = 5f;
        const float lowerRotationLimit = -90 + buffer;
        const float upperRotationLimit = 90 - buffer;
        
        if (rotation.x < lowerRotationLimit) {
            return new Vector3(lowerRotationLimit, rotation.y, rotation.z);
        } else if (rotation.x > upperRotationLimit) {
            return new Vector3(upperRotationLimit, rotation.y, rotation.z);
        } else {
            return rotation;
            
        }
    }
    
    // Constrain offset magnitude
    private Vector3 ValidCameraOffset() {
        
        // Constrain max and min distance
        if (offset.magnitude < minCameraDistance) {
            offset = offset.normalized * minCameraDistance;
        } else if (offset.magnitude > maxCameraDistance) {
            offset = offset.normalized * maxCameraDistance;
        }
        
        /*
        // Prevent clipping through walls
        if (Physics.Linecast(transform.position, cameraLookPosition, out hitInfo)) {
            isAgainstWall = true;
            offset = offset.normalized * (hitInfo.point - cameraLookPosition).magnitude;
        };
        */

        return offset;
    }

    // Shifts the camera to look at some object depending on params from the targetable
    public IEnumerator Target(ATargetable targetable) {
        
        Vector3 originalRotation = rotation;
        Vector3 originalOffset = offset;
        
        isTargeting = true;
        // move focus to thing
        target = targetable.transform;
        yield return new WaitForSeconds(targetable.secondsToWait);
        targetable.TargetAction();
        yield return new WaitUntil(targetable.CanUnTarget);
        Debug.Log("No longer targeting");
        // move focus back to player
        target = playerTransform;
        isTargeting = false;
        
    }
    
    private void LateUpdate() {

        // Don't camera when paused
        if (PauseMenu.gamePaused || isTargeting)
            return;
     
        // Find cameraLookPosition
        cameraLookPosition = target.transform.position + (target.CompareTag("Player") ? 
                                                          new Vector3(0f, playerFocusHeight, 0f) : 
                                                          Vector3.zero);
        
        // Scrolling
        offset *= 1 + zoomSpeed * Mathf.Clamp(Input.mouseScrollDelta.y, -maxZoomSpeed, maxZoomSpeed);
        
        // Take in and process input from mouse
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;
        Vector3 frameRotation = new Vector3(-vertical, horizontal, 0f);
        
        // Constrain offset position and apply
        offset = ValidCameraOffset();
        rotation = ValidCameraRotation(rotation + frameRotation);
        transform.position = target.position + Quaternion.Euler(rotation) * offset;
            
        // Look a little above target's origin if it's the player
        transform.LookAt(cameraLookPosition);
    }
       
}