using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class SignInteractable : AInteractable {

    public GameObject signDisplay;
    public String textToDisplay;
    private float interactStartTime;

    protected override IEnumerator Interaction() {

        signDisplay.SetActive(true);
        
        signDisplay.GetComponentInChildren<Text>().text = textToDisplay;
        interactStartTime = Time.time;
        yield return new WaitUntil(() => (Input.GetKeyDown(interactKey) || !playerInRange)
                                                   && Time.time > interactStartTime);
        
        signDisplay.SetActive(false);
        
    }
}