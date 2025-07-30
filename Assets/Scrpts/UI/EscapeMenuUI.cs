using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace bananplayss {
	public class EscapeMenuUI : MonoBehaviour {
		public event Action<bool> OnEnableEscapeMenu;
		public static EscapeMenuUI Instance { get; private set; }

		[SerializeField] private GameObject menu;
		[SerializeField] private TextMeshProUGUI endingsUnlockedText;
		[SerializeField] private TextMeshProUGUI interactedText;
		[SerializeField] private TextMeshProUGUI daysSleptText;
		[SerializeField] private Button languageButton;

		private int currentLangIndex = 0;
		private bool isLangEng = true;

		private void Awake() {
			Instance = this;
			languageButton.onClick.AddListener(() => {
				isLangEng = !isLangEng;
				currentLangIndex = isLangEng == true ? 0 : 1;
				{ LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLangIndex]; }
			});
			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
		}

		private void Update() {
			if (Input.GetKeyDown(Interact.escapeMenu)) {
				menu.SetActive(!menu.activeSelf);
				if (menu.activeSelf) {
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				} else {
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				}
				
				if (menu.activeSelf) RefreshMenu();
				Time.timeScale = menu.activeSelf ? 0f : 1f;
				OnEnableEscapeMenu?.Invoke(menu.activeSelf);
			}
		}

		private void RefreshMenu() {
			endingsUnlockedText.text = PlayerStats.endingsUnlocked + " / " + GameManager.allEndings;
			interactedText.text = PlayerStats.interacted.ToString();
			daysSleptText.text = PlayerStats.daysSlept.ToString();
		}
	}

}
