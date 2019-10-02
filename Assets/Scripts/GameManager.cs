using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int currentCrystals;
	public int totalCrystals;
	public Text crystalText;

	public int currentKeys;
	public Text keyText;
	
	public Vector3 spawnPoint;

	private void Start() {
		
		// Set player to spawn
		GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint;
		
		// Start crystal business
		currentCrystals = 0;
		crystalText = FindObjectOfType<Canvas>().transform.Find("CrystalText").GetComponent<Text>();
		CalculateCrystalScore();
		
		// Start key business
		currentKeys = 0;
		keyText = FindObjectOfType<Canvas>().transform.Find("KeyText").GetComponent<Text>();
		DisplayKeyText();
		
	}

	// This is obviously a repeat. Create class APickup?
	public void DisplayKeyText() {
		if (currentKeys == 0) {
			keyText.text = "";
		} else {
			keyText.text = "Keys: " + currentKeys;
		}
	}

	public void ChangeKeyValue(int keyChangeValue) {
		currentKeys += keyChangeValue;
		DisplayKeyText();
	}
	
	public void CalculateCrystalScore() {
		totalCrystals = GameObject.FindGameObjectsWithTag("Crystal").Length;
		DisplayCrystalText();
	}
	
	private void DisplayCrystalText() {
		if (totalCrystals == 0) {
			crystalText.text = "";
		} else {
			crystalText.text = "Crystals: " + currentCrystals + "/" + totalCrystals;
		}
		
	}

	public void AddCrystalsToScore(int valueToAdd) {
		currentCrystals += valueToAdd;
		DisplayCrystalText();
	}
}
