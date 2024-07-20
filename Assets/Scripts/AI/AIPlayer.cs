using GoogleDinoAI.Game;
using UnityEngine;

namespace GoogleDinoAI.AI
{
    public enum Act
    {
        RUN, JUMP, CROUCH
    }

    public class AIPlayer : MonoBehaviour
    {
        private GameSession gameSession;

        public float Fitness;
        public NeuralNetwork neuralNetwork;

        private const float jumpFine = 0.1f;
        private const float crouchFine = 0.1f;

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
            switch (GetNetworkAnswer()) {
                case Act.RUN:
                    dino.Run();
                    break;
                case Act.JUMP:
                    dino.Jump();
                    Fitness -= jumpFine;
                    break;
                case Act.CROUCH:
                    dino.Crouch();
                    Fitness -= crouchFine;
                    break;
            }
        }

        private Act GetNetworkAnswer()
        {
            InputParams parametrs = gameSession.GetGameInput();
            float[] input = new float[5];
            input[0] = parametrs.Speed;
            input[1] = parametrs.Distance;
            input[2] = parametrs.Width;
            input[3] = parametrs.JumpForce;
            input[4] = parametrs.ObstacleType;

            float[] output = neuralNetwork.FeedForward(input);

            if (output[0] < 0.5f)
                return Act.CROUCH;
            if (output[0] >= 0.5f)
                return Act.JUMP;

            return Act.RUN;
        }

        void OnDeadHandler()
        {
            Fitness += gameSession.currentDistance;
            OnDead?.Invoke();
        }
    }
}
