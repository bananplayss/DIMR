
using bananplayss;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable {
	[SerializeField] private List<Material> normalMaterials = new List<Material>();

	[SerializeField] private PlayerInventory inventory;
	[SerializeField] private TextMeshProUGUI ladderProgressText;
	[SerializeField] private Plank[] planks;
	[SerializeField] private GameObject creep;

	private int ladderProgress = 0;
	private float maxDistance = 3f;
	private bool built = false;
	private bool isPlayingCinematic = false;


	private void Update() {
		if(Vector3.Distance(transform.position,inventory.transform.position) < maxDistance && !isPlayingCinematic) {
			ladderProgressText.gameObject.SetActive(true);
		} else {
			ladderProgressText.gameObject.SetActive(false);
		}
	}

	public void Interact() {
		if (ladderProgress < planks.Length) {
			if (!inventory.HasPlank()) {
				DisplayMessageUI.Instance.DisplayMessage("Wooden planks needed", "Fadeszka kell");
			} else {
				ladderProgress++;
				ladderProgressText.text = $"{ladderProgress} / {planks.Length}";
				inventory.UnequipItem();
				if(ladderProgress == planks.Length) {
					ladderProgressText.text = "Build";
				}
			}
		} else {
			if (!built) {
				BuildLadder();
			} else {
				ClimbLadder();
			}
			
		}
	}

	private void BuildLadder() {
		for (int i = 0; i < GetComponent<MeshRenderer>().sharedMaterials.Length; i++) {
			GetComponent<MeshRenderer>().SetMaterials(normalMaterials);
		}
		ladderProgressText.text = "Climb";
		built = true;
	}

	private void ClimbLadder() {
		isPlayingCinematic = true;
		creep.SetActive(true);
		CinematicManager.Instance.StartClimbingCutscene();
	}

	public bool UsesIndicator() {
		return false;
	}
}
