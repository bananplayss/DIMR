using UnityEngine;

namespace bananplayss {
	public class Slippers : MonoBehaviour, IInteractable {


		public void Interact() {
			PlayerStats.Interact();
			DisplayMessageUI.Instance.DisplayMessage("Slippers","Papucs");
			gameObject.SetActive(false);
		}

		public bool UsesIndicator() {
			return false;
		}
	}

}
