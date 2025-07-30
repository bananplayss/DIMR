using System;
using UnityEngine;

namespace bananplayss {
	public class ActivityIndicatorManager : MonoBehaviour {
		public event Action<int> OnUseIndicator;

		public static ActivityIndicatorManager Instance { get; private set; }

		private int activeIndicators;
		public int maxIndicators;

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			activeIndicators = maxIndicators;
			ActivityIndicatorUI.Instance.OnRefillIndicators += ActivityIndicatorUI_OnRefillIndicators;
		}
		public void UseIndicator() {
			OnUseIndicator?.Invoke(activeIndicators);
			activeIndicators--;
		}

		public bool HasAllIndicators() {
			return activeIndicators == maxIndicators;
		}

		public bool CanUseIndicator() {
			return activeIndicators > 0;
		}

		private void ActivityIndicatorUI_OnRefillIndicators() {
			activeIndicators = maxIndicators;
		}
	}

}
