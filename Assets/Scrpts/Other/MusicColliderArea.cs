using System;
using UnityEngine;

namespace bananplayss {
	public class MusicColliderArea : MonoBehaviour {
		public event Action OnEnterMusicArea;
		public event Action OnLeaveMusicArea;
		public static MusicColliderArea Instance { get; private set; }

		private void Awake() {
			Instance = this;
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject.CompareTag("Player")) {
				OnEnterMusicArea?.Invoke();
			}
		}

		private void OnTriggerExit(Collider other) {
			if (other.gameObject.CompareTag("Player")) {
				OnLeaveMusicArea?.Invoke();
			}
		}
	}

}
