using UnityEngine;

namespace bananplayss {
	public class Painting : MonoBehaviour, IInteractable {
		Animator animator;
		[SerializeField] Animator keyAnimator;
		[SerializeField] private AudioSource shakeSFX;
		[SerializeField] private AudioSource fallSFX;
		public bool smallPainting = false;
		private bool played = false;
		private const string MOVE_PAINTING = "MovePainting";

		private int shookTimes = 0;
		private int shookTimesReq = 2;
		private void Start() {
			animator = GetComponent<Animator>();
		}

		public void Interact() {
			PlayerStats.Interact();
			shookTimes++;
			if (shookTimes >= shookTimesReq && !played) {
				played = true;
				if (smallPainting) {
					keyAnimator.Play("DropKey");
				} else {
					animator.Play("DropPainting");
				}
				
			}

			if (!played) {
				animator.Play(MOVE_PAINTING);
				shakeSFX.Play();
			}
		}

		public void PlayFallSFX() {
			fallSFX.Play();
		}

		public bool UsesIndicator() {
			if (played) return false;
			return true;
		}

		public float GetShookTimes() {
			return shookTimes;
		}
	}

}
