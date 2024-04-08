using UnityEngine;

namespace GoogleDinoAI.Game
{
    public class ManualControl : MonoBehaviour
    {
        [SerializeField]
        private Dino dino;

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space))
                dino.Jump();
        }
    }
}
