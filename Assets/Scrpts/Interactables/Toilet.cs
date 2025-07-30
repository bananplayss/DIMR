using UnityEngine;

namespace bananplayss {
	public class Toilet : MonoBehaviour, IInteractable {
		private bool pulledOutPlunger = false;
		private bool pullingOutPlunger = false;
		private Animator animator;


		[SerializeField] private AudioSource coverDropSFX;
		[SerializeField] private AudioSource toiletFlushSFX;
		private const string TOILET_COVER_FALL = "ToiletCoverFall";

		private void Start() {
			animator = GetComponent<Animator>();
		}

		public void Interact() {
			if (toiletFlushSFX.isPlaying) return;
			PlayerStats.Interact();
			if (!pulledOutPlunger) {
				PullOutPlunger();
				return;
			}
			InteractWithToilet();
		}

		public void CoverDropSFX() {
			coverDropSFX.Play();
		}

		private void InteractWithToilet() {
			toiletFlushSFX.Play();
		}

		private void PullOutPlunger() {
			if (pullingOutPlunger) return;
			pullingOutPlunger = true;
			animator.Play(TOILET_COVER_FALL);
			DisplayMessageUI.Instance.DisplayMessage("I'll keep this","Ezt megtartom");

			Invoke(nameof(DelayPulledOutPlunger), 3f);
		}

		private void DelayPulledOutPlunger() {
			pulledOutPlunger = true;
		}

		public bool UsesIndicator() {
			return false;
		}
	}

}
