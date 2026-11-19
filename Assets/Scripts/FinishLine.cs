using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem finishParticle;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("WIN!!!");
            
            finishParticle.Play();
            //reload scene
            Invoke("GoToMainMenu", 2f);
        }
    }
   
    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
