
using bananplayss;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DisplayMessageUI : MonoBehaviour
{
    public static DisplayMessageUI Instance { get; private set; }

    [SerializeField] private CanvasGroup container;
    [SerializeField] private TextMeshProUGUI errorMessageText;

	private void Awake() {
		Instance = this;
	}

	private void Start() {
        container.alpha = 0;
	}

	public void DisplayMessage(string engMessage,string huMessage) {
        LeanTween.cancelAll();
        container.alpha = 1f;
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1]) {
			errorMessageText.text = huMessage;
        } else {
            errorMessageText.text = engMessage;
        }
        
        Invoke(nameof(DelayFadeOut), 3f);
    }

    private void DelayFadeOut() {
		LeanTween.alphaCanvas(container, 0, 3f);
	}
}
