using System.Collections.Generic;
using System.Linq;
using GoogleDinoAI.AI;
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

        public InputParams GetGameInput()
        {
            List<Obstacle> obstacles = obstaclesGenerator.GetObstacles().ToList();
            obstacles.RemoveAll(o => o == null || o.GetPosition() - player.GetPosition() < 0);
            Obstacle nearest = obstacles.OrderBy(o => o.GetPosition() - player.GetPosition()).FirstOrDefault();
            InputParams input = new InputParams()
            {
                Speed = gameProgress.currentSpeed,
                Distance = nearest == null || (nearest.GetPosition() - player.GetPosition()) < 0 ? int.MaxValue : nearest.GetPosition() - player.GetPosition(),
                Width = nearest?.GetWidthHeight().x ?? -1,
                JumpForce = player.GetJumpForce(),
                ObstacleType = nearest == null || (nearest.GetPosition() - player.GetPosition()) < 0 ? -1 : (float)nearest.GetType(),
            };

            return input;
        }

        public void RestartGame()
        {
            obstaclesGenerator.Clear();
            gameProgress.Restart();
        }

        private void Awake()
        {
            Physics2D.IgnoreLayerCollision(3, 3);

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
            //RestartGame();
        }
    }
}
