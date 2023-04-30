using Assets.Scripts.Contracts;
using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignInManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button signInButton;

    public void OnSignInButtonClicked()
    {
        var usernameText = usernameInput.text;
        var passwordText = passwordInput.text;

        StartCoroutine(SignInRequest(usernameText, passwordText));
    }

    private IEnumerator SignInRequest(string username, string password)
    {
        var uri = $"{Constants.API_URL}/session/sign-in";

        var signInDto = new SignInDto()
        {
            Username = username,
            Password = password
        };
        var bodyData = JsonConvert.SerializeObject(signInDto);

        using var webRequest = UnityWebRequest.Put(uri, bodyData);
        webRequest.method = "POST";
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + webRequest.error);
        }
        else if (webRequest.responseCode == 200)
        {
            var responseDto = JsonConvert.DeserializeObject<SignInResultDto>(webRequest.downloadHandler.text);
            Debug.Log($"200 Received: {responseDto.Token}/{responseDto.Account.Id}");
            AccountHolder.Instance.Data = responseDto;
            SceneManager.LoadScene("HomeScene");
        }
        else if (webRequest.responseCode == 400)
        {
            var respondeDto = JsonConvert.DeserializeObject<BadRequestResponseDto>(webRequest.downloadHandler.text);
            Debug.Log($"400 Received: {respondeDto.Detail}");
        }
        else
        {
            Debug.Log("Error While Sending: " + webRequest.error);
        }
    }
}
