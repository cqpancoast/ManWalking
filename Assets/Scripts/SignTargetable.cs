using UnityEngine;

public class SignTargetable : ATargetable {

    private bool canUnTarget;
    
    public override bool CanUnTarget() {
        return canUnTarget;
    }

    
    private void OnTriggerEnter(Collider other) {
        secondsToWait = 1;
        if (other.CompareTag("Player")) {
            StartCoroutine(Target());
        }
        canUnTarget = false;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            canUnTarget = true;
        }
    }
}