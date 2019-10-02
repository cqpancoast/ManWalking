using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour {

    public GameObject keyDissolve;
    public float bobHeight;
    public float bobSpeed;
    

    private void Update() {
        
        if (PauseMenu.gamePaused)
            return;
        
        // Causes it to bob in midair (I'm too lazy to animate)
        transform.position += new Vector3(0f, bobHeight * Mathf.Sin(bobSpeed * Time.time), 0f);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<GameManager>().ChangeKeyValue(1);
            gameObject.SetActive(false); // SetActive() or Destroy()? Is this a rogue or roguevania?
            //Instantiate(keyDissolve, transform.position, transform.rotation);
        }
    }
}
