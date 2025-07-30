using System;
using TMPro;
using UnityEngine;

namespace bananplayss {
	public class InventoryItem : MonoBehaviour {
		public event Action OnTurnOnHairDryer;
		public event Action OnTurnOffHairDryer;
		public event Action<int> OnPlayGuitar;

		[SerializeField] private PlayerInventory.ItemEnum item;
		[SerializeField] private ParticleSystem guitarSoundParticles;
		[SerializeField] private Closet closet;
		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private CanvasGroup dieText;
		[SerializeField] private GameObject notebook;
		[SerializeField] private AudioSource guitarSFX;
		[SerializeField] private AudioSource notebookSFX;
		[SerializeField] private AudioPlayer audioPlayer;
		[SerializeField] private AudioSource hairDryerSFX;
		private bool canPlayCinematic = false;

		private Animator anim;

		private bool isInMusicArea = false;

		private const string EQUIP_GUITAR = "EquipGuitar";

		private int numTimesPlayed = 0;

		private bool hairDryerOn = false;
		private bool usedBook = false;
		private bool isPlayingGuitar = false;
		private bool startedMuting = false;

		private float playGuitarTimegate = .6f;
		private float timer;

		private void Start() {
			anim = GetComponent<Animator>();
			closet.OnDoorFell += Closet_OnDoorFell;
			closet.OnOpenedCloset += Closet_OnOpenedCloset;
			MusicColliderArea.Instance.OnEnterMusicArea += MusicColliderArea_OnEnterMusicArea;
			MusicColliderArea.Instance.OnLeaveMusicArea += MusicColliderArea_OnLeaveMusicArea;
		}

		private void MusicColliderArea_OnLeaveMusicArea() {
			isInMusicArea = false;
		}

		private void Closet_OnOpenedCloset() {
			canPlayCinematic = true;
		}

		private void Update() {
			if (isPlayingGuitar) {
				timer += Time.deltaTime;
				if (timer >= playGuitarTimegate) {
					if (!startedMuting) {
						LeanTween.value(gameObject, guitarSFX.volume, 0f, .4f).setOnUpdate((float val) => { guitarSFX.volume = val; }).setOnComplete(StopGuitarSFX);
						startedMuting = true;
					}
					timer = 0;
					isPlayingGuitar = false;
				}
			}
		}

		private void StopGuitarSFX() {
			guitarSFX.Stop();
		}

		private void MusicColliderArea_OnEnterMusicArea() {
			isInMusicArea = true;
		}

		private void Closet_OnDoorFell() {
			canPlayCinematic = true;
		}

		public int GetNumTimesPlayed() {
			return numTimesPlayed;
		}

		public virtual void UseItem() {
			switch (item) {
				case PlayerInventory.ItemEnum.Guitar:
					UseGuitar();
					break;
				case PlayerInventory.ItemEnum.Book:
					UseBook();
					break;
				case PlayerInventory.ItemEnum.HairDryer:
					UseHairDryer();
					break;
			}
		}

		public void Equip() {
			anim.Play(EQUIP_GUITAR);
		}

		public void UseBook() {
			if (usedBook) return;
			audioPlayer.StopSFX();
			notebookSFX.Play();
			usedBook = true;
			CinematicManager.Instance.StartNotebookEndingCutscene();
			playerInventory.EquipItem(PlayerInventory.ItemEnum.Book);
			notebook.SetActive(false);
			LeanTween.alphaCanvas(dieText, 1, 4.5f);
			
			Invoke(nameof(DelaySendDeath), 8f);

			return;
			Debug.Log("Feature not implemented. -> InventoryItem");
			//if(canuseBook stb){
			//usebook
		}



		public void UseHairDryer() {
			hairDryerOn = !hairDryerOn;
			if (hairDryerOn) {
				OnTurnOnHairDryer?.Invoke();
				hairDryerSFX.Play();
				hairDryerSFX.volume = 0f;
				LeanTween.value(gameObject, hairDryerSFX.volume, 1f, .7f).setOnUpdate((float val) => { hairDryerSFX.volume= val; });
			} else {
				OnTurnOffHairDryer?.Invoke();
				hairDryerSFX.volume = 1f;
				LeanTween.value(gameObject, hairDryerSFX.volume, 1f, .7f).setOnUpdate((float val) => { hairDryerSFX.volume = val; }).setOnComplete(StopHairDryerSFX);
				
			}
			//Play sfx
		}

		private void StopHairDryerSFX() {
			hairDryerSFX.Stop();
		}

		public void UseGuitar() {
			if (isInMusicArea) {
				numTimesPlayed++;
				OnPlayGuitar?.Invoke(numTimesPlayed);
			}
			timer = 0;
			if (!guitarSFX.isPlaying) {
				guitarSFX.Play();
				guitarSFX.volume = 1;
				startedMuting = false;
			}
			guitarSoundParticles.Play();
			isPlayingGuitar = true;
		}

		private void DelaySendDeath() {
			playerInventory.SendDeath();
		}
	}

}
