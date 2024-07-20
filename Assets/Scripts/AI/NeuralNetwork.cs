using System;
using System.Collections.Generic;

namespace GoogleDinoAI.AI
{
    public class NeuralNetwork
    {
        private int[] layers;
        private float[][] neurons;
        private float[][] biases;
        private float[][][] weights;

        public NeuralNetwork(int[] layers)
        {
            this.layers = layers;

            InitNeurons();
            InitBiases();
            InitWeights();
        }

        // создаем нейроны
        private void InitNeurons()
        {
            List<float[]> neuronsList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
                neuronsList.Add(new float[layers[i]]);

            neurons = neuronsList.ToArray();
        }

        // задаем случайные смещения
        private void InitBiases()
        {
            List<float[]> biasList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                float[] bias = new float[layers[i]];
                for (int j = 0; j < layers[i]; j++)
                    bias[j] = UnityEngine.Random.Range(-0.5f, 0.5f);

                biasList.Add(bias);
            }
            biases = biasList.ToArray();
        }

        // задаем случайные веса
        private void InitWeights()
        {
            List<float[][]> weightsList = new List<float[][]>();
            for (int i = 1; i < layers.Length; i++)
            {
                List<float[]> layerWeightsList = new List<float[]>();
                int neuronsInPreviousLayer = layers[i - 1];
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    float[] neuronWeights = new float[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                        neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);

                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            weights = weightsList.ToArray();
        }

        // преобразразование inputs в output
        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < layers.Length; i++)
            {
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < neurons[i - 1].Length; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = Activate(value + biases[i][j]);
                }
            }
            return neurons[neurons.Length - 1];
        }

        // функция активации - в нашем случае достаточно пороговой
        private float Activate(float value)
        {
            //return (float)Math.Tanh(value);
            return value > 0 ? 1 : 0;
        }

        private bool GetRandomChance(float chance)
        {
            return UnityEngine.Random.Range(0f, 1f) <= chance;
        }

        // рандомным образом видоизменяем сеть, чтобы получилось либо лучше, либо хуже
        public void Mutate(float chance, float val)
        {
            for (int i = 0; i < biases.Length; i++)
                for (int j = 0; j < biases[i].Length; j++)
                    if (GetRandomChance(chance))
                        biases[i][j] += UnityEngine.Random.Range(-val, val);

            for (int i = 0; i < weights.Length; i++)
                for (int j = 0; j < weights[i].Length; j++)
                    for (int k = 0; k < weights[i][j].Length; k++)
                        if (GetRandomChance(chance))
                            weights[i][j][k] += UnityEngine.Random.Range(-val, val);
        }

        public NeuralNetwork CopyTo(NeuralNetwork nn)
        {
            for (int i = 0; i < biases.Length; i++)
                for (int j = 0; j < biases[i].Length; j++)
                    nn.biases[i][j] = biases[i][j];

            for (int i = 0; i < weights.Length; i++)
                for (int j = 0; j < weights[i].Length; j++)
                    for (int k = 0; k < weights[i][j].Length; k++)
                        nn.weights[i][j][k] = weights[i][j][k];

            return nn;
        }
    }
}
