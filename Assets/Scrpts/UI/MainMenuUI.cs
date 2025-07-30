using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button startGame;

	private void Awake() {
		startGame.onClick.AddListener(() => {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			SceneManager.LoadScene(1);
		});
		
	}

	private void Start() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
