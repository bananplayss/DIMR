using UnityEngine;

namespace bananplayss {
	public class ThankYouForPlayingUI : MonoBehaviour {
		[SerializeField] private CanvasGroup thxForPlayingMenu;

		private void Start() {
			//PlayerStats.OnCompleteGame += PlayerStats_OnCompleteGame;
		}

		private void PlayerStats_OnCompleteGame() {
			LeanTween.alphaCanvas(thxForPlayingMenu, 1, 5f);
		}
	}

}
