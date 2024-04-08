using System;
using UnityEngine;

namespace GoogleDinoAI.Game
{
    [Serializable]
    public struct ObstacleParams
    {
        public float width;
        public float height;
        public Sprite sprite;
    }

    [RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(BoxCollider2D))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private ObstacleParams[] variants;
        [SerializeField]
        private float speed = 10f;

        private Vector3 currentPosition;
        private float curXValue;
        private Vector2 WidthHeight;

        private void Start()
        {
            int random = UnityEngine.Random.Range(0, variants.Length);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = variants[random].sprite;

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = new Vector2(variants[random].width, variants[random].height);
            WidthHeight = new Vector2(variants[random].width, variants[random].height);
        }

        private void Update()
        {
            curXValue += speed * Time.deltaTime;
            currentPosition.x = -curXValue;
            transform.localPosition = currentPosition;
        }

        public void Init(Vector3 startPos, float speed)
        {
            this.speed = speed;

            transform.position = startPos;
            curXValue = -startPos.x;
            currentPosition = transform.localPosition;
        }

        public float GetPosition()
        {
            return -curXValue;
        }

        public Vector2 GetWidthHeight()
        {
            return WidthHeight;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "DeadZone")
                Destroy(gameObject);
        }
    }
}