using System;
using UnityEngine;

namespace bananplayss {
	public class DemonJumpscare : MonoBehaviour {
		public event Action OnDemonBlackScreen;

		private Animator m_Animator;
		private const string JUMPSCARE = "Jumpscare";
		[SerializeField] private PlayerMovement pm;
		[SerializeField] private GameObject body;

		private void Start() {
			body.SetActive(false);
			m_Animator = GetComponent<Animator>();
			pm.OnDemonJumpscare += Pm_OnDemonJumpscare;
		}

		private void Pm_OnDemonJumpscare() {
			body.SetActive(true);
			m_Animator.Play(JUMPSCARE);
		}

		public void ShowBlackScreen() {
			OnDemonBlackScreen?.Invoke();
		}
	}

}
