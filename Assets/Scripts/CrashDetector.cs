using UnityEngine;
using UnityEngine.SceneManagement;
public class CrashDetector : MonoBehaviour
{
   [SerializeField] private ParticleSystem crashParticle;
   void OnTriggerEnter2D(Collider2D other)
   {
      int layer = LayerMask.NameToLayer("Floor");
      
      if (other.gameObject.layer == layer)
      {
         crashParticle.Play();
         Debug.Log("CRASH!!!");
         Invoke(nameof(ReloadScene), 1.5f);
      }

   }
   void ReloadScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
