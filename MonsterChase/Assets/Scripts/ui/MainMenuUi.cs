using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour {
  public void PlayGame() {
    string name = EventSystem.current.currentSelectedGameObject.name;
    int selectedCharacter = int.Parse(name.Substring(name.Length - 1));
    GameManager.instance.CharacterIndex = selectedCharacter;
    SceneManager.LoadScene("Gameplay");
  }
}
