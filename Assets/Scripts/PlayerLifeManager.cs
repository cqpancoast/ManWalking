using UnityEngine;

// Manages death, respawning, and checkpoints for player
public class PlayerLifeManager : MonoBehaviour {

    public float deathHeight = -200;
    public float respawnTime = 3; //...3 of what?
    public Vector3 respawnPosition;

    public GameManager gameManager;
    public GameObject player;

    private GameObject[] crystalPickups;
    
    // Checks what may become several conditions to check if player is dead
    private bool IsDead() {

        bool deadFromHeight = player.transform.position.y < deathHeight;

        return deadFromHeight || false; //false could be one of the other values

    }

    private void Respawn() {
        
        player.SetActive(false);
        // have this wait for a time proably
        
        // Add the crystals back in, subtract from player score
        gameManager.AddCrystalsToScore(-gameManager.currentCrystals);
        int numOfCrystals = crystalPickups.Length;
        if (numOfCrystals > 0) {
            for (int i = 0; i < numOfCrystals; i++) {
                crystalPickups[i].SetActive(true);
            }
        }

        player.transform.SetPositionAndRotation(respawnPosition, Quaternion.identity);
        player.SetActive(true);

    }
    
    private void Start() {
        
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        respawnPosition = player.transform.position;
        crystalPickups = GameObject.FindGameObjectsWithTag("Crystal");
        
    }
    
    private void Update() {

        if (IsDead()) {
            Respawn();
        }
        
    }
    
}