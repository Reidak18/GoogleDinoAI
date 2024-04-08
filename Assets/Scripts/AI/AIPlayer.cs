using GoogleDinoAI.Game;
using UnityEngine;

namespace GoogleDinoAI.AI
{
    public class AIPlayer : MonoBehaviour
    {
        private GameSession gameSession;

        public float Fitness;
        public NeuralNetwork neuralNetwork;

        private float jumpFine = 0.1f;

        private Dino dino;

        public event System.Action OnDead;

        public void SetNetwork(NeuralNetwork neuralNetwork)
        {
            this.neuralNetwork = neuralNetwork;
        }

        private void Awake()
        {
            gameSession = FindObjectOfType<GameSession>();
        }

        void Start()
        {
            dino = Instantiate(Resources.Load<Dino>("Dino"), transform);
            Color randomColor = Random.ColorHSV(0, 1, 0, 1, 1, 1, 1, 1);
            dino.Initialize(new DinoParams() { color = randomColor });
            dino.OnDead += OnDeadHandler;
        }

        void FixedUpdate()
        {
            if (GetNetworkAnswer())
            {
                dino.Jump();
                Fitness -= jumpFine;
            }
        }

        public float distance;
        bool GetNetworkAnswer()
        {
            InputParams parametrs = gameSession.GetGameInput();
            float[] input = new float[4];
            input[0] = parametrs.Speed;
            input[1] = parametrs.Distance;
            input[2] = parametrs.Width;
            input[3] = parametrs.JumpForce;

            distance = input[1];
            float[] output = neuralNetwork.FeedForward(input);
            return output[0] == 1;
        }

        void OnDeadHandler()
        {
            Fitness += gameSession.currentDistance;
            OnDead?.Invoke();
        }
    }
}
