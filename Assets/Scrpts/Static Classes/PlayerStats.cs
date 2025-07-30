using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplayss {
	public static class PlayerStats {
		public static int endingsUnlocked = 0;
		public static int interacted = 0;
		public static int daysSlept = 0;

		private static List<int> endingsUnlockedList = new List<int>();

		public static void LoadAllStats() {
			endingsUnlocked = PlayerPrefs.GetInt(nameof(endingsUnlocked));
			interacted = PlayerPrefs.GetInt(nameof(interacted));
			daysSlept = PlayerPrefs.GetInt(nameof(daysSlept));

			for (int i = 0; i < PlayerPrefs.GetInt("endingsUnlockedList_count"); i++) {

				endingsUnlockedList.Add(PlayerPrefs.GetInt("endingsUnlockedList_count_" + i));
				}
			}

		public static void SaveAllStats() {
			PlayerPrefs.SetInt(nameof(endingsUnlocked), endingsUnlocked);
			PlayerPrefs.SetInt(nameof(interacted), interacted);
			PlayerPrefs.SetInt(nameof(daysSlept), daysSlept);

			PlayerPrefs.SetInt("endingsUnlockedList_count", endingsUnlockedList.Count);
			for (int i = 0; i < endingsUnlockedList.Count; i++) {
				PlayerPrefs.SetInt("endingsUnlockedList_count_" + i, endingsUnlockedList[i]);
			}
		}

		public static void CheckUnlockNewEnding(EndGameManager.EndGameCase egCase) {
			if (!endingsUnlockedList.Contains((int)egCase)) {
				endingsUnlockedList.Add((int)egCase);
				endingsUnlocked++;
			}
		}

		public static void Interact() {
			interacted++;
		}

		public static void SleptDay() {
			daysSlept++;
		}
	}


}
