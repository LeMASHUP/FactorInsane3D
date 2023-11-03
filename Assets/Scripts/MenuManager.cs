using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Fonction pour charger le niveau 1
    public void LoadLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
    }
}
