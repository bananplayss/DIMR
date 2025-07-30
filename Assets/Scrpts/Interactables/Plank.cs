using bananplayss;
using UnityEngine;

public class Plank : MonoBehaviour, IInteractable {
	[SerializeField] private PlayerInventory inventory;

	public void Interact() {
		if (inventory.CanBePickedUp()) {
			inventory.EquipItem(PlayerInventory.ItemEnum.Plank);
			gameObject.SetActive(false);
		}
	}

	public bool UsesIndicator() {
		return false;
	}
}
