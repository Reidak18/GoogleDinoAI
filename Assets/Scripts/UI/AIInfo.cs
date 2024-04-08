using UnityEngine;
using UnityEngine.UI;
using GoogleDinoAI.AI;

namespace GoogleDinoAI.UI
{
    public class AIInfo : MonoBehaviour
    {
        [SerializeField]
        private Text GenerationText;
        [SerializeField]
        private Text RecordText;
        [SerializeField]
        private Text AliveText;

        [SerializeField]
        private string GenerationTextPattern;
        [SerializeField]
        private string RecordTextPattern;
        [SerializeField]
        private string AliveTextPattern;

        [SerializeField]
        private NeuralManager neuralManager;

        private float currentRecord;

        private void Awake()
        {
            neuralManager.OnAliveCountChanged += OnAliveCountChanged;
            neuralManager.OnDistanceChanged += OnDistanceChanged;
            neuralManager.OnEndGeneration += OnEndGeneration;
        }

        private void OnAliveCountChanged(int alive)
        {
            AliveText.text = string.Format(AliveTextPattern, alive);
        }

        private void OnDistanceChanged(float distance)
        {
            currentRecord = Mathf.Max(currentRecord, distance);
            RecordText.text = string.Format(RecordTextPattern, currentRecord);
        }

        private void OnEndGeneration(int generation)
        {
            GenerationText.text = string.Format(GenerationTextPattern, generation);
        }
    }
}
