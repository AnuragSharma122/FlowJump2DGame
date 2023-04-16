using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public GameManager gamemanager;
    public void OnTriggerEnter2D()
    {
        gamemanager.CompleteLevel();
    }

}
