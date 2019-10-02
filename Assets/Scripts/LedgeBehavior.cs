using UnityEngine;

// Activates the pC.Hang() when the player enters the collider
public class LedgeBehavior : MonoBehaviour {
    
    public PlayerController playerController;

    private void Start() {
        
        playerController = FindObjectOfType<PlayerController>();

    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            
            playerController.ledgeGrabbed = gameObject;
            playerController.hangXPosition = transform.InverseTransformPoint(playerController.transform.position).x;
            playerController.ledgeRotation = transform.eulerAngles.y;
            
            StartCoroutine(playerController.Hang());
            
        }
    }
    
}