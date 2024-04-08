using UnityEngine;
using UnityEngine.UI;

namespace GoogleDinoAI.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;

        public void UpdateScore(float score)
        {
            scoreText.text = Mathf.RoundToInt(score).ToString();
        }
    }
}