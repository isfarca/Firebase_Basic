using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Database : MonoBehaviour
{
    // Database connection.
    [SerializeField] private string _databaseUrl;
    private DatabaseReference _reference;
    
    // Database structure.
    [SerializeField] private string _itemsName;
    
    // Database elements.
    [SerializeField] private string _userId;
    [SerializeField] private string _username;
    [SerializeField] private string _email;
    
    // Buttons for manipulation and query.
    [SerializeField] private Button _writeNewUserButton;
    [SerializeField] private Button _updateUsernameButton;
    [SerializeField] private Button _updateEmailButton;
    [SerializeField] private Button _deleteUserButton;
    [SerializeField] private Button _getUsernameButton;
    [SerializeField] private Button _getEmailButton;

    /// <summary>
    /// Connect with database.
    /// </summary>
    private void Awake()
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(_databaseUrl);
        
        // Get the root reference location of the database.
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// Initializing.
    /// </summary>
    private void Start()
    {
        // Add listener to buttons.
        _writeNewUserButton.onClick.AddListener(WriteUser);
        _updateUsernameButton.onClick.AddListener(UpdateUsername);
        _updateEmailButton.onClick.AddListener(UpdateEmail);
        _deleteUserButton.onClick.AddListener(RemoveUser);
        _getUsernameButton.onClick.AddListener(GetUsername);
        _getEmailButton.onClick.AddListener(GetEmail);
    }
    
    /// <summary>
    /// Add new item.
    /// </summary>
    private void WriteUser()
    {
        // Add user to constructor.
        User user = new User(_username, _email);
        
        // Convert user constructor to json format.
        string json = JsonUtility.ToJson(user);

        // Write user in database.
        _reference.Child(_itemsName).Child(_userId).SetRawJsonValueAsync(json);
    }

    /// <summary>
    /// Set value to username.
    /// </summary>
    private void UpdateUsername()
    {
        _reference.Child(_itemsName).Child(_userId).Child("Username").SetValueAsync(_username);
    }

    /// <summary>
    /// Set value to email.
    /// </summary>
    private void UpdateEmail()
    {
        _reference.Child(_itemsName).Child(_userId).Child("Email").SetValueAsync(_email);
    }

    /// <summary>
    /// Delete item.
    /// </summary>
    private void RemoveUser()
    {
        _reference.Child(_itemsName).Child(_userId).RemoveValueAsync();
    }

    /// <summary>
    /// Get username element value.
    /// </summary>
    private void GetUsername()
    {
        // Current username output.
        FirebaseDatabase.DefaultInstance.GetReference(_itemsName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
            }
            else if (task.IsCompleted) 
            {
                DataSnapshot snapshot = task.Result;
                
                Debug.Log(snapshot.Child(_userId).Child("Username").Value);
            }
        });
    }

    /// <summary>
    /// Get email element value.
    /// </summary>
    private void GetEmail()
    {
        // Current email output.
        FirebaseDatabase.DefaultInstance.GetReference(_itemsName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
            }
            else if (task.IsCompleted) 
            {
                DataSnapshot snapshot = task.Result;
                
                Debug.Log(snapshot.Child(_userId).Child("Email").Value);
            }
        });
    }
}