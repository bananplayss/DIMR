
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace bananplayss {
	public class UnderwaterOverlayUI : MonoBehaviour {
		[SerializeField] private WaterBlock waterBlock;
		[SerializeField] private CanvasGroup overlay;
		[SerializeField] private CanvasGroup redVignetteOverlay;
		[SerializeField] private GameObject blackImage;
		[SerializeField] private PlayerSleep playerSleep;
		private bool played = false;

		private void Start() {
			waterBlock.OnTriggerHead += WaterBlock_OnTriggerHead;
		}

		private void WaterBlock_OnTriggerHead() {
			if (!played) { ShowOverlay(); played = true; }	
		}
		private void ShowOverlay() {
			LeanTween.alphaCanvas(overlay, 1, .2f);
			Invoke(nameof(DelaySuffocation), 6f);
		}
		private void DelaySuffocation() {
			LeanTween.alphaCanvas(redVignetteOverlay, .7f, .3f);
			AudioManager.Instance.PlayClip(Sound.Gasping,.4f);
			Invoke(nameof(DelayVignette), 3f);
		}

		private void DelayVignette() {
			playerSleep.Vignette();
			Invoke(nameof(DelayTurnVisionBlack), 8f);
		}

		private void DelayTurnVisionBlack() {
			blackImage.SetActive(true);
			Invoke(nameof(DelayShowEndGameScreen), 2f);
		}

		private void DelayShowEndGameScreen() {
			EndGameScreenUI.Instance.ShowEndGameScreen(EndGameManager.EndGameCase.SuffocateEnding);
		}
	}

}
