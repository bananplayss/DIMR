using UnityEngine;

namespace bananplayss {
	public class PlayerStandupUI : MonoBehaviour {
		[SerializeField] private PlayerStandup playerStandup;
		[SerializeField] private GameObject container;

		private void Start() {
			playerStandup.OnCanStandUp += PlayerStandup_OnCanStandUp;
			playerStandup.OnCantStandUp += PlayerStandup_OnCantStandUp;
		}

		private void PlayerStandup_OnCantStandUp() {
			container.SetActive(false);
		}

		private void PlayerStandup_OnCanStandUp() {
			container.SetActive(true);
		}
	}

}
