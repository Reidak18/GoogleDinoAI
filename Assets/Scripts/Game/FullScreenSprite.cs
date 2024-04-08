using UnityEngine;

namespace GoogleDinoAI.Game
{
    public class FullScreenSprite : MonoBehaviour
    {
        private void Awake()
        {
            Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
            Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            float height = topRight.y - bottomLeft.y;
            float width = topRight.x - bottomLeft.x;
            transform.localScale = new Vector3(width, height, 1);
        }
    }
}
