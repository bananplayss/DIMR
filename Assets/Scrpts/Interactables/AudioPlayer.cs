using UnityEngine;

namespace bananplayss {
	public class AudioPlayer : MonoBehaviour, IInteractable {
		private bool isPlaying = false;
		[SerializeField] private new ParticleSystem particleSystem;
		[SerializeField] private AudioSource audioplayerSFX;

		private void Start() {
			particleSystem.Stop();
		}

		public void Interact() {
			PlayerStats.Interact();
			isPlaying = !isPlaying;	
			audioplayerSFX.enabled = isPlaying;
			if (isPlaying) {
				particleSystem.Play();
			} else {
				particleSystem.Stop();
			}
		}

		public void StopSFX() {
			audioplayerSFX.enabled = false;
		}

		public bool UsesIndicator() {
			return true;
		}

		public bool IsPlaying() {
			return isPlaying;
		}
	}
}
