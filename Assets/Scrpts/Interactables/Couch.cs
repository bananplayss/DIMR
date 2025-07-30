using UnityEngine;

namespace bananplayss {
	public class Couch : MonoBehaviour, IInteractable {

		public void Interact() {
			PlayerStats.Interact();
			CinematicManager.Instance.StartSitCutscenePublic();
		}

		public bool UsesIndicator() {
			return true;
		}
	}

}
