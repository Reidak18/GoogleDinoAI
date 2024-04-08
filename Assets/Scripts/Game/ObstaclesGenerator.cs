using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleDinoAI.Game
{
    public class ObstaclesGenerator : MonoBehaviour
    {
        [SerializeField]
        private Obstacle prefab;
        [SerializeField]
        private Vector3 startPos;
        [SerializeField]
        private float minTime;
        [SerializeField]
        private float maxTime;

        private float startSpeed;
        private float currentSpeed;
        private List<Obstacle> obstacles = new List<Obstacle>();

        public void SetStartSpeed(float speed)
        {
            startSpeed = speed;
            currentSpeed = speed;
        }

        public void SetSpeed(float speed)
        {
            currentSpeed = speed;
        }

        public List<Obstacle> GetObstacles()
        {
            return obstacles;
        }

        public void Clear()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            obstacles.Clear();
        }

        private IEnumerator Start()
        {
            while (true)
            {
                float delay = Random.Range(minTime * startSpeed / currentSpeed, maxTime * startSpeed / currentSpeed);
                yield return new WaitForSeconds(delay);
                Obstacle current = Instantiate(prefab, transform);
                current.Init(startPos, currentSpeed * 2f);
                obstacles.Add(current);
            }
        }
    }
}