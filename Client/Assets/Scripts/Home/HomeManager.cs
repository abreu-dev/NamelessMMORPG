using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public TMP_Text accountNameText;
    public Button signOutButton;

    private void Start()
    {
        accountNameText.text = AccountHolder.Instance.Data.Account.Username;
    }

    public void OnSignOutButtonClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
