
using UnityEngine;

namespace bananplayss {
	public class Mirror : MonoBehaviour {

		[SerializeField] private Transform player;
		[SerializeField] private CanvasGroup demonSilhouette;
		[SerializeField] private CanvasGroup demonSilhouetteRed;
		[SerializeField] private WaterBlock waterBlock;
		[SerializeField] private AudioSource voidSFX;
		[SerializeField] private AudioSource laughterSFX;
		private float requiredDistance = 1.5f;
		private float alphaMultiplier = 8.5f;

		private float mirrorTimer;
		private float mirrorTimerRequired = 12f;
		private float mirrorTimeRequiredWarning = 6f;
		private float alphaRequired = .05f;

		private bool warned = false;
		private bool inCutscene = false;
		private bool inOtherCutscene = false;

		private void Start() {
			demonSilhouette.alpha = 0f;
			waterBlock.OnTriggerHead += WaterBlock_OnTriggerHead;
		}

		private void WaterBlock_OnTriggerHead() {
			inOtherCutscene = true;
			demonSilhouette.alpha = 0;
			demonSilhouetteRed.alpha = 0;
		}

		private void Update() {
			if (inOtherCutscene) {
				return;
			}
			float distance = Vector3.Distance(transform.position, player.position);
			float alpha = 0;

			alpha = Mathf.Clamp(alpha, 0, .7f);
			alpha = 1 - (distance / requiredDistance);

			if (distance > requiredDistance) {
				alpha = 0;
			}
			if (inCutscene) {
				demonSilhouetteRed.gameObject.SetActive(true);
				AudioManager.Instance.PlayClip(Sound.Laughter,.7f);
				alpha = .9f;
				demonSilhouetteRed.alpha = alpha;
			}
			demonSilhouette.alpha = alpha * alphaMultiplier;

			if (alpha > alphaRequired) {
				mirrorTimer += Time.deltaTime;
				if (mirrorTimer > mirrorTimerRequired && !inCutscene) {
					inCutscene = true;
					CinematicManager.Instance.StartMirrorCutscene();
					voidSFX.Play();
				} else if (mirrorTimer > mirrorTimeRequiredWarning && !warned) {
					laughterSFX.Play();
					demonSilhouetteRed.gameObject.SetActive(true);
					LeanTween.alphaCanvas(demonSilhouetteRed, .9f, .6f).setLoopPingPong().setLoopCount(4);
					warned = true;
				}
			}
		}
	}

}
