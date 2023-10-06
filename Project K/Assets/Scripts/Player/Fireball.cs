using UnityEngine;

namespace Player
{
    public class Fireball : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Destroy(this.gameObject);
        }
    }
}
