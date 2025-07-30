using UnityEngine;

namespace bananplayss {
	public class InteractDotUI : MonoBehaviour {
		[SerializeField] private GameObject dot;

		private PlayerInteract playerInteract;

		private void Start() {
			playerInteract = FindObjectOfType(typeof(PlayerInteract)) as PlayerInteract;
			playerInteract.OnInteractHover += PlayerInteract_OnInteractHover;
			playerInteract.OnInteractUnhover += PlayerInteract_OnInteractUnhover;
		}

		private void PlayerInteract_OnInteractUnhover() {
			Hide();
		}

		private void PlayerInteract_OnInteractHover() {
			Show();
		}

		public void Show() {
			dot.SetActive(true);
		}

		public void Hide() {
			dot.SetActive(false);
		}
	}

}
