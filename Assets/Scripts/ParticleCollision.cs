using UnityEngine;
using Player;

namespace Player
{
    public class ParticleCollision : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Hi");
                other.transform.parent.gameObject.GetComponent<PlayerMovement>().CallBounce(0.5f, 0f, 0.1f);
            }
        }
    }
}
