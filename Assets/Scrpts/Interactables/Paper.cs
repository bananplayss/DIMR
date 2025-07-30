using bananplayss;

using UnityEngine;

public class Paper : MonoBehaviour, IInteractable {
	[SerializeField] private string messageEng;
	[SerializeField] private string messageHu;
	public void Interact() {
		DisplayMessageUI.Instance.DisplayMessage("It says\n" + messageEng,"Azt irja\n " + messageHu);
	}

	public bool UsesIndicator() {
		return false;
	}
}
