using System.Collections.Generic;
using System.Linq;
using GoogleDinoAI.Game;
using UnityEngine;

namespace GoogleDinoAI.AI
{
    public class NeuralManager : MonoBehaviour
    {
        [SerializeField]
        private AIPlayer PlayerPrefab;
        [SerializeField]
        private GameSession gameSession;

        private int[] layers;

        private const int populationSize = 50;
        private const int bestCount = 5;
        private const float mutatingVal = 0.5f;

        private AIPlayer[] players;
        private AIPlayer[] lastPopulation;

        private int generation;
        private int deadCount;

        public event System.Action<int> OnAliveCountChanged;
        public event System.Action<float> OnDistanceChanged;
        public event System.Action<int> OnEndGeneration;

        void Start()
        {
            layers = new int[] { 4, 8, 8, 1 };
            InstantiatePopulation();
            gameSession.RestartGame();
        }

        private void Update()
        {
            OnDistanceChanged?.Invoke(gameSession.currentDistance);
        }

        void InstantiatePopulation()
        {
            players = new AIPlayer[populationSize];
            OnAliveCountChanged?.Invoke(populationSize);
            for (int i = 0; i < populationSize; i++)
            {
                players[i] = Instantiate(PlayerPrefab, transform);
                players[i].OnDead += OnDeadHandler;

                if (generation == 0)
                    players[i].SetNetwork(new NeuralNetwork(layers));
                else
                    MutateLastPopulation(i);
            }
        }

        void MutateLastPopulation(int i)
        {
            lastPopulation = lastPopulation.OrderByDescending(p => p.Fitness).ToArray();
            NeuralNetwork copy = lastPopulation[i].neuralNetwork.CopyTo(new NeuralNetwork(layers));
            if (i < bestCount)
            {
                // do nothing
            }
            else if (i < populationSize * 0.25f)
            {
                copy.Mutate(0.2f, mutatingVal);
            }
            else if (i < populationSize * 0.5f)
            {
                copy.Mutate(0.4f, mutatingVal);
            }
            else if (i < populationSize * 0.75f)
            {
                copy.Mutate(0.6f, mutatingVal);
            }
            else if (i < populationSize)
            {
                copy.Mutate(0.8f, mutatingVal);
            }
            players[i].SetNetwork(copy);
        }

        void OnDeadHandler()
        {
            deadCount++;
            OnAliveCountChanged?.Invoke(populationSize - deadCount);

            if (deadCount == populationSize)
                EndGeneration();
        }

        void EndGeneration()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            gameSession.RestartGame();
            StartGeneration();
            OnEndGeneration?.Invoke(generation);
        }

        void StartGeneration()
        {
            generation++;
            deadCount = 0;
            lastPopulation = players;
            InstantiatePopulation();
        }
    }
}