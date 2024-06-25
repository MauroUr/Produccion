using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButton : MonoBehaviour
{
    [SerializeField] MainMenu menu;
    [SerializeField] LevelController levelController;
    [SerializeField] OptionsMenu optionsMenu;
    [SerializeField] string typeOfButton;
    
    private void OnMouseOver()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
            sprite.color = Color.green;
    }

    private void OnMouseExit()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
            sprite.color = Color.white;
    }
    private void OnMouseDown()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
            sprite.color = Color.white;

        switch (typeOfButton)
        {
            case "Start":
                menu.PlayButton(); break;
            case "Exit":
                if (menu != null)
                    menu.ExitButton();
                else
                    optionsMenu.QuitGame(); break;
            case "Options":
                menu.OptionsButton(); break;
            case "Level 1":
                levelController.StartLevel(1); break;
            case "Level 2":
                levelController.StartLevel(2); break;
            case "Level 3":
                levelController.StartLevel(3); break;
            case "Back":
                levelController.Back(); break;
            case "MainMenu":
                optionsMenu.MainMenu(); break;
            case "Controls":
                optionsMenu.ControlPanel(); break;
            case "Credits":
                menu.Credits(); break;
        }
    }
}
