using bananplayss;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable {
	[SerializeField] private PlayerInventory playerInventory;
	[SerializeField] private AudioSource keyDropSFX;

	private void Start() {
		GetComponent<Collider>().enabled = false;
	}

	public void Interact() {
		PlayerStats.Interact();
		playerInventory.AddKeyToInventory();
		DisplayMessageUI.Instance.DisplayMessage("Key","Kulcs");
		gameObject.SetActive(false);
	}

	public bool UsesIndicator() {
		return false;
	}

	public void PlayKeydropSFX() {
		keyDropSFX.Play();
		GetComponent<Collider>().enabled = true;
	}
}
