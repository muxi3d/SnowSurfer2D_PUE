using UnityEngine;

public class CharacterMenuArt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Active the selected character based on the index stored in PlayerPrefs
    int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        //Activate the selected character sprite based on the index
        transform.GetChild(0).GetChild(selectedCharacterIndex).gameObject.SetActive(true);
    }
}
