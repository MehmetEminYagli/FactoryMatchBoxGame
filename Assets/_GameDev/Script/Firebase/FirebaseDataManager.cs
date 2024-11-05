using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
public class FirebaseDataManager : MonoBehaviour
{
    private string userID;
    [SerializeField] private DatabaseReference databaseReference;

    void Start()
    {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });

        userID = SystemInfo.deviceUniqueIdentifier;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        CreateUser();


    }




    private void CreateUser()
    {
        User newUser = new User(userID, GameManager.Instance.uiManager.GetMoney());
        string json = JsonUtility.ToJson(newUser);

        databaseReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

}
