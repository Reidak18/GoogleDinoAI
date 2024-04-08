using UnityEngine;

namespace GoogleDinoAI.Game
{
    public struct DinoParams
    {
        public Color color;
    }

    public class Dino : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Rigidbody2D rigidbody;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float jumpForce;

        private bool onGround = false;

        public event System.Action OnDead;

        public void Jump()
        {
            if (onGround)
            {
                rigidbody.AddForce(new Vector2(0, jumpForce / Time.deltaTime), ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                onGround = true;
                animator.SetTrigger("Ground");
                animator.ResetTrigger("Jump");
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                onGround = false;
                animator.SetTrigger("Jump");
                animator.ResetTrigger("Ground");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                Dead();
            }
        }

        private void Dead()
        {
            OnDead?.Invoke();
        }
    }
}
