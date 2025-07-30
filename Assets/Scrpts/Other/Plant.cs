using bananplayss;
using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
	public event Action OnTriggerPlant;
    [SerializeField] private InventoryItem guitar;
	[SerializeField] private AudioSource plantTriggerSFX;
	private int interactionsNeeded = 20;

	private void Start() {
		guitar.OnPlayGuitar += Guitar_OnPlayGuitar;
	}

	private void Guitar_OnPlayGuitar(int interactions) {
		if (interactions % interactionsNeeded == 0) {
			OnTriggerPlant?.Invoke();
			if (!plantTriggerSFX.isPlaying) {
				plantTriggerSFX.Play();
			}
			DisplayMessageUI.Instance.DisplayMessage("<HCUOC EHT NO TIS","ZENELJ ULVE");
		}
	}
}
