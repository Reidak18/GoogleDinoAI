using UnityEngine;

namespace GoogleDinoAI.Game
{
    public class GameProgress : MonoBehaviour
    {
        [SerializeField]
        private float startSpeed;
        [SerializeField]
        private float acceleration;

        public event System.Action<float> OnSpeedUpdated;
        public event System.Action<float> OnDistanceUpdated;

        public float currentDistance;
        public float currentSpeed;

        public void Restart()
        {
            Awake();
            OnDistanceUpdated?.Invoke(currentDistance);
            OnSpeedUpdated?.Invoke(currentSpeed);
        }

        private void Awake()
        {
            currentSpeed = startSpeed;
            currentDistance = 0;
        }

        private void FixedUpdate()
        {
            currentDistance += currentSpeed * Time.fixedDeltaTime;
            OnDistanceUpdated?.Invoke(currentDistance);
            currentSpeed += acceleration * Time.deltaTime;
            OnSpeedUpdated?.Invoke(currentSpeed);
        }
    }
}
