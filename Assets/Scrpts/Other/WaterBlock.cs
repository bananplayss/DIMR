
using System;
using UnityEngine;

namespace bananplayss {
	public class WaterBlock : MonoBehaviour {
		public event Action OnTriggerHead;

		[SerializeField] private Mixer mixer;
		[SerializeField] private GameObject waterBlockGO;

		private void Start() {
			mixer.OnHardShake += Mixer_OnHardShake;
		}

		private void Mixer_OnHardShake() {
			waterBlockGO.SetActive(true);
			LeanTween.moveY(gameObject, 2.2f, 12);
		}

		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Head")) {
				mixer.SetRainFalse();
				OnTriggerHead?.Invoke();
			}
			if (other.TryGetComponent<Mixer>(out Mixer _mixer)) {
				mixer.DisableWater();
			}
			if (other.TryGetComponent<WashingMachine>(out WashingMachine _washingMachine)) {
				_washingMachine.PlayIdle();
			}
		}
	}

}
