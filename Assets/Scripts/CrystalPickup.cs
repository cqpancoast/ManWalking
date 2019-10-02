using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickup : MonoBehaviour {

	public int crystalValue = 1;
	public GameObject crystalDissolve;

	public void Start() {
		FindObjectOfType<GameManager>().CalculateCrystalScore();
	}
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			FindObjectOfType<GameManager>().AddCrystalsToScore(crystalValue);
			gameObject.SetActive(false);
			Instantiate(crystalDissolve, transform.position, transform.rotation);
		}
	}
}
