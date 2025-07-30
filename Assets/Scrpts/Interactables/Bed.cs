using UnityEngine;

namespace bananplayss.Interactables {
	public class Bed : MonoBehaviour, IInteractable {

		public void Interact() {
			PlayerStats.Interact();
			CinematicManager.Instance.StartSleepCutscenePublic();
		}

		public bool UsesIndicator() {
			return false;
		}
	}
}


