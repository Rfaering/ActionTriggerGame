using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginLevel : MonoBehaviour {

	public void BeginGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
}
