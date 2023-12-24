using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithm7
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public bool IsLeaf() { return Left == null && Right == null; }

        public int CompareTo(HuffmanNode other) { return Frequency.CompareTo(other.Frequency); }
    }

    class HuffmanTree
    {
        private HuffmanNode root;

        // Кодирование
        public string Encode(string input)
        {
            // Построение дерева и таблицы кодов
            BuildTree(input);
            Dictionary<char, string> codeTable = BuildCodeTable(root);

            StringBuilder encoded = new StringBuilder();
            foreach (char character in input) encoded.Append(codeTable[character]);

            return encoded.ToString();
        }

        // Декодирование строки
        public string Decode(string encodedString)
        {
            StringBuilder decoded = new StringBuilder();
            HuffmanNode currentNode = root;

            foreach (char bit in encodedString)
            {
                if (bit == '0' && currentNode.Left != null) currentNode = currentNode.Left;
                else if (bit == '1' && currentNode.Right != null) currentNode = currentNode.Right;
                else return "[Huffman] Ошибка декодирования";

                if (currentNode.IsLeaf())
                {
                    decoded.Append(currentNode.Character);
                    currentNode = root;
                }
            }
            return decoded.ToString();
        }

        // Построение дерева Хаффмана
        private void BuildTree(string input)
        {
            // Подсчет частот символов
            Dictionary<char, int> frequencies = new Dictionary<char, int>();
            foreach (char character in input)
            {
                if (frequencies.ContainsKey(character))
                {
                    frequencies[character]++;
                }
                else
                {
                    frequencies[character] = 1;
                }
            }

            // Построение приоритетной очереди из узлов дерева
            PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();
            foreach (var pair in frequencies)
                priorityQueue.Enqueue(new HuffmanNode { Character = pair.Key, Frequency = pair.Value });

            // Построение дерева Хаффмана
            while (priorityQueue.Count > 1)
            {
                HuffmanNode left = priorityQueue.Dequeue();
                HuffmanNode right = priorityQueue.Dequeue();
                HuffmanNode parent = new HuffmanNode { Frequency = left.Frequency + right.Frequency, Left = left, Right = right };
                priorityQueue.Enqueue(parent);
            }
            root = priorityQueue.Count > 0 ? priorityQueue.Dequeue() : null;
        }

        // Построение таблицы кодов
        private Dictionary<char, string> BuildCodeTable(HuffmanNode root)
        {
            Dictionary<char, string> codeTable = new Dictionary<char, string>();
            BuildCodeTableRecursive(root, "", codeTable);
            return codeTable;
        }

        private void BuildCodeTableRecursive(HuffmanNode node, string code, Dictionary<char, string> codeTable)
        {
            if (node != null)
            {
                if (node.IsLeaf()) codeTable[node.Character] = code;

                BuildCodeTableRecursive(node.Left, code + "0", codeTable);
                BuildCodeTableRecursive(node.Right, code + "1", codeTable);
            }
        }
    }

    class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> heap;

        public int Count => heap.Count;

        public PriorityQueue()
        {
            heap = new List<T>();
        }

        public void Enqueue(T item)
        {
            heap.Add(item);
            int i = heap.Count - 1;
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (heap[parent].CompareTo(heap[i]) <= 0)
                    break;

                Swap(parent, i);
                i = parent;
            }
        }

        public T Dequeue()
        {
            if (heap.Count == 0) throw new InvalidOperationException("[Huffman] Очередь пуста");

            T root = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            int i = 0;
            while (true)
            {
                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;
                int smallestChild = i;

                if (leftChild < heap.Count && heap[leftChild].CompareTo(heap[smallestChild]) < 0)
                    smallestChild = leftChild;

                if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[smallestChild]) < 0)
                    smallestChild = rightChild;

                if (smallestChild == i)
                    break;

                Swap(i, smallestChild);
                i = smallestChild;
            }
            return root;
        }

        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
