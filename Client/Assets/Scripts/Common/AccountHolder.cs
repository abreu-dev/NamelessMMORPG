using Assets.Scripts.Contracts;
using UnityEngine;

public class AccountHolder : MonoBehaviour
{
    public static AccountHolder Instance;

    public SignInResultDto Data { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
