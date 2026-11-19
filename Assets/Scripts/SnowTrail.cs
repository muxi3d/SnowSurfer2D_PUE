using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] private ParticleSystem SnowTrailParticle;
    private Animator animator;
  
    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        animator = transform.GetChild(0)
                            .GetChild(selectedCharacterIndex)
                            .GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       int layer = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layer)
        {
            SnowTrailParticle.Play();
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        int layer = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layer)
        {
            SnowTrailParticle.Stop();
            animator.SetBool("isJumping", true);
        }
    }
}