using System.Collections.Generic;
using System.Linq;
using GoogleDinoAI.UI;
using UnityEngine;

namespace GoogleDinoAI.Game
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField]
        private Dino player;
        [SerializeField]
        private GameProgress gameProgress;
        [SerializeField]
        private MovingObject infinityGround;
        [SerializeField]
        private ScoreText scoreText;
        [SerializeField]
        private ObstaclesGenerator obstaclesGenerator;

        public float currentDistance;

        public void RestartGame()
        {
            obstaclesGenerator.Clear();
            gameProgress.Restart();
        }

        private void Awake()
        {
            gameProgress.OnSpeedUpdated += OnSpeedUpdated;
            gameProgress.OnDistanceUpdated += OnDistanceUpdated;
            player.OnDead += OnDead;

            obstaclesGenerator.SetStartSpeed(gameProgress.currentSpeed);
        }

        private void OnSpeedUpdated(float speed)
        {
            infinityGround.SetSpeed(speed);
            obstaclesGenerator.SetSpeed(speed);
        }

        private void OnDistanceUpdated(float distance)
        {
            currentDistance = distance;
            scoreText.UpdateScore(distance);
        }

        private void OnDead()
        {
            RestartGame();
        }
    }
}
