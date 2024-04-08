using UnityEngine;

namespace GoogleDinoAI.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MovingObject : MonoBehaviour
    {
        [SerializeField]
        private float offset = 18f;
        [SerializeField]
        private float length = 12f;

        [SerializeField]
        private float speed = 10f;

        private SpriteRenderer spriteRenderer;
        private Vector3 currentPosition;
        private Vector2 currentSize;
        private float curValue;

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        private void Awake()
        {
            curValue = offset;
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentPosition = transform.localPosition;
            currentSize = spriteRenderer.size;
        }

        private void Update()
        {
            curValue += speed * Time.deltaTime;
            if (curValue >= length)
                curValue %= length;

            currentPosition.x = -curValue;
            currentSize.x = offset + curValue * 2;

            transform.localPosition = currentPosition;
            spriteRenderer.size = currentSize;
        }
    }
}
