using System.Collections;
using UnityEngine;

public class DoorInteractable : AInteractable {

    public GameManager gameManager;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    protected override IEnumerator Interaction() {

        if (gameManager.currentKeys > 0) {
            gameManager.ChangeKeyValue(-1);
            gameObject.SetActive(false);
        }
        
        yield return null;
    }
}