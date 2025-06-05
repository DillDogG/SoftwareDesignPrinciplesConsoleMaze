using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPrinciplesConsoleMaze
{
    public class PathGeneratorSingleton
    {
        private static readonly Lazy<PathGeneratorSingleton> instance = new Lazy<PathGeneratorSingleton>(() => new PathGeneratorSingleton());

        private PathGeneratorSingleton() { }

        Random rand = new Random();

        public static PathGeneratorSingleton GetInstance()
        {
            return instance.Value;
        }

        public MazeNode GeneratePath(int length, bool correct = true)
        {
            MazeNode startNode = new MazeNode();
            MazeNode currentNode = startNode;
            for (int i = 0; i < length; i++)
            {
                MazeNode newNode = new MazeNode();
                int correctPath = rand.Next(0, 3);
                if (correctPath == 0)
                {
                    newNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT", i == length - 1);
                }
                else if (correctPath == 1)
                {
                    newNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT", i == length - 1);
                }
                else
                {
                    newNode.MakeNode(new List<MazeNode>(), currentNode, "RIGHT", i == length - 1);
                }
                currentNode.AddPath(newNode);
                // For displaying the path in presentation
                Console.WriteLine(newNode.NodeName);
                int wrongCount = rand.Next(0, 3);
                MazeNode wrongNode = new MazeNode();
                if (wrongCount == 1)
                {
                    int wrongDirection = rand.Next(0, 2);
                    if (wrongDirection == 0)
                    {
                        if (correctPath == 0)
                        {
                            wrongNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        }
                        else
                        {
                            wrongNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT");
                        }
                    }
                    else
                    {
                        if (correctPath == 2)
                        {
                            wrongNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        }
                        else
                        {
                            wrongNode.MakeNode(new List<MazeNode>(), currentNode, "RIGHT");
                        }
                    }
                    currentNode.AddPath(wrongNode);
                }
                else if (wrongCount == 2)
                {
                    if (correctPath == 0)
                    {
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        currentNode.AddPath(wrongNode);
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "RIGHT");
                        currentNode.AddPath(wrongNode);
                    }
                    else if (correctPath == 1)
                    {
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT");
                        currentNode.AddPath(wrongNode);
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "RIGHT");
                        currentNode.AddPath(wrongNode);
                    }
                    else
                    {
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT");
                        currentNode.AddPath(wrongNode);
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        currentNode.AddPath(wrongNode);
                    }
                }
                currentNode = newNode;
            }
            return startNode;
        }

        public void ShowPath(MazeNode node)
        {
            if (node.potentialPaths.Count < 1)
            {
                Console.WriteLine($"You have reached a dead end. You can only go to the PREVIOUS path right now.");
                return;
            }
            Console.Write("You can see the following paths ahead. ");
            foreach (var path in node.potentialPaths)
            {
                Console.Write($" {path.NodeName}");
            }
            if (node.previousNode != null)
            {
                Console.Write($" or PREVIOUS to go back to the previous node");
            }
            Console.WriteLine(".");
            return;
        }

        public MazeNode MoveToNextNode(MazeNode currentNode, string direction)
        {
            if (direction.ToUpper() == "PREVIOUS")
            {
                return currentNode.previousNode ?? currentNode;
            }
            var nextNode = currentNode.potentialPaths.FirstOrDefault(p => p.NodeName.Equals(direction, StringComparison.OrdinalIgnoreCase));
            if (nextNode != null)
            {
                return nextNode;
            }
            Console.WriteLine("Invalid direction. Please choose a valid path.");
            return currentNode;
        }
    }
}
