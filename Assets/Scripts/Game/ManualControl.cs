using UnityEngine;

namespace GoogleDinoAI.Game
{
    public class ManualControl : MonoBehaviour
    {
        [SerializeField]
        private Dino dino;

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
                dino.Jump();

            if (Input.GetKey(KeyCode.DownArrow))
                dino.Crouch();
            else 
                dino.Run();
        }
    }
}
