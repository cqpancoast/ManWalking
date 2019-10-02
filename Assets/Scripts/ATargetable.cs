using System;
using System.Collections;
using UnityEngine;

public abstract class ATargetable : MonoBehaviour {

    public CameraController cameraController;
    public PlayerController playerController;
    
    protected bool freezePlayer;

    public float secondsToWait;
    protected bool lerpToPosn;
    protected Vector3 lerpOffsetPosn;
    protected Vector3 lerpOffsetRot;
    protected float lerpSpeed;
    

    protected void Start() {
        cameraController = FindObjectOfType<CameraController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // THERE IS NO UPDATE FUNCTION: Create method to activate Target() in child classes
    
    // There's a choice for whether to implement/override this or do something else in CC class
    public virtual bool CanUnTarget() {
        return false;
    }

    // Camera is gong to have to shift either way, be it in rotation or in position...
    // How to get rotation happening in CC class to stop when condition in this class is met???
    protected IEnumerator Target() {
        if (freezePlayer) {
            playerController.playerFrozen = true;
        }
        yield return cameraController.Target(this);
        playerController.playerFrozen = false;
    }

    // This is the only straightforward method in this entire class smh
    public virtual void TargetAction() {}
    
}