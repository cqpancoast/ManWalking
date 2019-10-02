using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class AInteractable : MonoBehaviour {

    protected String interactKey = "e";
    private bool isPlayerInteracting;
    protected bool canStartInteraction;
    protected bool playerInRange;
    public Image pressEImage;

    // Starts the interaction coroutine given certain conditions
    protected void OnTriggerStay(Collider other) {

        canStartInteraction = playerInRange
                              && Input.GetKeyDown(interactKey)
                              && !isPlayerInteracting;
        
        if (canStartInteraction) {

            pressEImage.GetComponent<Toggle>().isOn = false;
            
            isPlayerInteracting = true;
            StartCoroutine(Interaction());
            isPlayerInteracting = false;

        }
    }

    // To be replaced in the child class
    protected virtual IEnumerator Interaction() {
        yield return new WaitUntil(() => true);
    }

    protected void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player")) {
            playerInRange = true;
            pressEImage.GetComponent<Toggle>().isOn = true;
        }
    }

    protected void OnTriggerExit(Collider other) {

        if (other.CompareTag("Player")) {
            playerInRange = false;
            pressEImage.GetComponent<Toggle>().isOn = false;
        }
    }
}