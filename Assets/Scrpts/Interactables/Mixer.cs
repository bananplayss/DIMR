
using System;
using UnityEngine;

namespace bananplayss {
	public class Mixer : MonoBehaviour, IInteractable {
		public event Action OnHardShake;

		private Animator animator;
		[SerializeField] private GameObject[] rainLevels;
		private int interactCounter = -1;
		private int interactNeededForMildShake = 3;
		private int interactNeededForHardShake = 5;

		private const string MIXER_MILD_SHAKE = "MixerMildShake";
		private const string MIXER_HARD_SHAKE = "MixerHardShake";

		[SerializeField] private AudioSource showerSFX;
		[SerializeField] private GameObject water;

		private void Start() {
			animator = GetComponent<Animator>();
		}

		public void Interact() {
			if (interactCounter + 1 == interactNeededForHardShake) return;
			if (!showerSFX.isPlaying) {
				showerSFX.Play();
			} else {
				showerSFX.pitch += .1f;
			}
			PlayerStats.Interact();
			interactCounter++;
			rainLevels[interactCounter].SetActive(true);
			if (interactCounter + 1 == interactNeededForHardShake) {
				animator.CrossFade(MIXER_HARD_SHAKE, .5f);
				OnHardShake?.Invoke();
			} else if (interactCounter + 1 == interactNeededForMildShake) {
				animator.CrossFade(MIXER_MILD_SHAKE, .5f);
			}
		}

		public void DisableWater() {
			water.SetActive(false);
			showerSFX.Stop();
		}

		public bool UsesIndicator() {
			return false;
		}

		public void SetRainFalse() {
			for (int i = 0; i < rainLevels.Length; i++) {
				rainLevels[i].SetActive(false);
				animator.CrossFade(MIXER_MILD_SHAKE, .5f);
				animator.speed = .5f;
			}
		}

	}

}
