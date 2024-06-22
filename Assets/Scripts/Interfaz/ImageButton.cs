using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    [SerializeField] MainMenu menu;
    [SerializeField] LevelController levelController;
    [SerializeField] OptionsMenu optionsMenu;
    [SerializeField] string typeOfButton;

    private void OnMouseOver()
    {
        Image[] sprites = GetComponentsInChildren<Image>();

        foreach (Image sprite in sprites)
            sprite.color = Color.green;
    }

    private void OnMouseExit()
    {
        Image[] sprites = GetComponentsInChildren<Image>();

        foreach (Image sprite in sprites)
            sprite.color = Color.white;
    }
    private void OnMouseDown()
    {
        Image[] sprites = GetComponentsInChildren<Image>();

        foreach (Image sprite in sprites)
            sprite.color = Color.white;

        switch (typeOfButton)
        {
            case "Exit":
                    optionsMenu.QuitGame(); break;
            case "Options":
                menu.OptionsButton(); break;
            case "MainMenu":
                optionsMenu.MainMenu(); break;
        }
    }
}
