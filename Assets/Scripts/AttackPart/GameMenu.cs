using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Image gameMenu;

    [System.Obsolete]
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameMenu.gameObject.active == false)
            {
                gameMenu.gameObject.SetActive(true);
            }
            else
            {
                gameMenu.gameObject.SetActive(false);
            }
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("Attack1");
        ShowCard.disapear = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
