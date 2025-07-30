using System;
using UnityEngine;

namespace bananplayss {
	public class ActivityIndicatorUI : MonoBehaviour {
		public static ActivityIndicatorUI Instance { get; private set; }

		public event Action OnRefillIndicators;

		[SerializeField] private GameObject[] indicators;
		[SerializeField] private PlayerSleep playerSleep;

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			playerSleep.OnWakeup += PlayerSleep_OnWakeup;
			ActivityIndicatorManager.Instance.OnUseIndicator += ActivityIndicatorManager_OnUseIndicator;
		}

		private void ActivityIndicatorManager_OnUseIndicator(int activeIndicators) {
			indicators[indicators.Length - activeIndicators].SetActive(false);
		}

		private void PlayerSleep_OnWakeup() {
			RefillIndicators();
		}

		public void HideIndicators() {
			foreach (var indicator in indicators) { indicator.SetActive(false); }
		}

		public void RefillIndicators() {
			foreach (var indicator in indicators) { indicator.SetActive(true); }
			OnRefillIndicators?.Invoke();
		}
	}

}
