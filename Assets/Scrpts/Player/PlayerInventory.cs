using UnityEngine;

namespace bananplayss {
	public class PlayerInventory : MonoBehaviour {
		private Animator animator;
		private bool hasKey = false;
		private bool hasPlank = false;

		public enum ItemEnum {
			Guitar,
			Book,
			HairDryer,
			Plank,
		}

		[SerializeField] private InventoryItem[] items;
		private InventoryItem currentItem;

		public void EquipItem(ItemEnum item) {
			if (item == ItemEnum.Plank) {
				hasPlank = true;
			}
			if (currentItem == null) {
				currentItem = items[(int)item];
				currentItem.gameObject.SetActive(true);
			}
		}

		public bool HasPlank() {
			return hasPlank;
		}

		public bool IsGuitarInHand() {
			return currentItem = items[(int)ItemEnum.Guitar];
		}
		public void AddKeyToInventory() {
			hasKey = true;
		}

		public bool HasKey() {
			return hasKey;
		}

		public void UnequipItem() {
			if (currentItem == null) return;
			hasPlank = false;
			currentItem.gameObject.SetActive(false);
			currentItem = null;
		}

		public bool CanBePickedUp() {
			return currentItem == null;
		}

		private void Update() {
			if (currentItem != null && Input.GetMouseButtonDown(1)) {
				currentItem.UseItem();
			}
		}

		public void SendDeath() {
			GetComponent<PlayerMovement>().Die();
		}
	}

}
