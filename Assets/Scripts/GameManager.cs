using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private bool _bGameStarted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        } 
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_bGameStarted)
        {
            _bGameStarted = true;
            StartCoroutine(AsyncLoadScene());
        }
    }

    private IEnumerator AsyncLoadScene()
    {
        AsyncOperation asncLoad = SceneManager.LoadSceneAsync("Level0");

        while (!asncLoad.isDone)
        {
            yield return null;
        }
    }
}
