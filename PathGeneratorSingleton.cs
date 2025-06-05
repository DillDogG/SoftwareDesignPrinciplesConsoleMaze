using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPrinciplesConsoleMaze
{
    // A singleton is used here to prevent multiple mazes from getting made, or parts of it going missing. It also highly limits duplication of the state system.
    // However if it is ever needed the state system can have duplicates.
    public class PathGeneratorSingleton
    {
        private static readonly Lazy<PathGeneratorSingleton> instance = new Lazy<PathGeneratorSingleton>(() => new PathGeneratorSingleton());

        private PathGeneratorSingleton() { }

        Random rand = new Random();

        public static PathGeneratorSingleton GetInstance()
        {
            return instance.Value;
        }

        // Generates the path the maze takes
        public MazeNode GeneratePath(int length, int complexity)
        {
            MazeNode startNode = new MazeNode();
            MazeNode currentNode = startNode;
            for (int i = 0; i < length; i++)
            {
                MazeNode newNode = new MazeNode();
                int correctPath = rand.Next(0, 3);
                if (correctPath == 0) newNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT", i == length - 1);
                else if (correctPath == 1) newNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT", i == length - 1 );
                else newNode.MakeNode(new List<MazeNode>(), currentNode, "RIGHT", i == length - 1);
                currentNode.AddPath(newNode);
                // For displaying the path in presentation
                Console.WriteLine(newNode.NodeName);
                int wrongCount = rand.Next(0, 3);
                if (wrongCount == 1)
                {
                    MazeNode wrongNode = new MazeNode();
                    int wrongDirection = rand.Next(0, 2);
                    if (wrongDirection == 0)
                    {
                        if (correctPath == 0) wrongNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        else wrongNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT");
                    }
                    else
                    {
                        if (correctPath == 2) wrongNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        else wrongNode.MakeNode(new List<MazeNode>(), currentNode, "RIGHT");
                    }
                    currentNode.AddPath(wrongNode);
                    if (complexity > 0) GenerateFailed(wrongNode);
                }
                else if (wrongCount == 2)
                {
                    MazeNode wrongNode = new MazeNode();
                    MazeNode wrongNode2 = new MazeNode();
                    if (correctPath == 0)
                    {
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        currentNode.AddPath(wrongNode);
                        wrongNode2.MakeNode(new List<MazeNode>(), currentNode, "RIGHT");
                        currentNode.AddPath(wrongNode2);
                    }
                    else if (correctPath == 1)
                    {
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT");
                        currentNode.AddPath(wrongNode);
                        wrongNode2.MakeNode(new List<MazeNode>(), currentNode, "RIGHT");
                        currentNode.AddPath(wrongNode2);
                    }
                    else
                    {
                        wrongNode.MakeNode(new List<MazeNode>(), currentNode, "LEFT");
                        currentNode.AddPath(wrongNode);
                        wrongNode2.MakeNode(new List<MazeNode>(), currentNode, "STRAIGHT");
                        currentNode.AddPath(wrongNode2);
                    }
                    if (complexity > 0)
                    {
                        GenerateFailed(wrongNode);
                        GenerateFailed(wrongNode2);
                    }
                }
                currentNode = newNode;
            }
            return startNode;
        }

        // Generates any paths that don't lead anywhere, only used when complexity is greater than 0.
        public void GenerateFailed(MazeNode previous)
        {
            int pathCount = rand.Next(0, 4);
            int paths = rand.Next(0, 3);
            switch (pathCount)
            {
                case 1:
                    {
                        MazeNode node1 = new MazeNode();
                        if (paths == 0) node1.MakeNode(new List<MazeNode>(), previous, "LEFT");
                        else if (paths == 1) node1.MakeNode(new List<MazeNode>(), previous, "STRAIGHT");
                        else node1.MakeNode(new List<MazeNode>(), previous, "RIGHT");
                        previous.AddPath(node1);
                    }
                    break;

                case 2:
                    {
                        MazeNode node1 = new MazeNode();
                        MazeNode node2 = new MazeNode();
                        if (paths == 0)
                        {
                            node1.MakeNode(new List<MazeNode>(), previous, "STRAIGHT");
                            node2.MakeNode(new List<MazeNode>(), previous, "RIGHT");
                        }
                        else if (paths == 1)
                        {
                            node1.MakeNode(new List<MazeNode>(), previous, "LEFT");
                            node2.MakeNode(new List<MazeNode>(), previous, "RIGHT");
                        }
                        else
                        {
                            node1.MakeNode(new List<MazeNode>(), previous, "LEFT");
                            node2.MakeNode(new List<MazeNode>(), previous, "STRAIGHT");
                        }
                        previous.AddPath(node1);
                        previous.AddPath(node2);
                    }
                    break;
                case 3:
                    {
                        MazeNode node1 = new MazeNode();
                        MazeNode node2 = new MazeNode();
                        MazeNode node3 = new MazeNode();
                        node1.MakeNode(new List<MazeNode>(), previous, "LEFT");
                        node2.MakeNode(new List<MazeNode>(), previous, "STRAIGHT");
                        node3.MakeNode(new List<MazeNode>(), previous, "RIGHT");
                        previous.AddPath(node1);
                        previous.AddPath(node2);
                        previous.AddPath(node3);
                    }
                    break;
                default:
                    return;
            }
        }

        // Displays the options to the user.
        public void ShowPath(MazeNode node)
        {
            if (node.potentialPaths.Count < 1)
            {
                Console.WriteLine("You have reached a dead end. You can only go to the PREVIOUS path right now.");
                return;
            }
            Console.Write("You can see the following paths ahead. ");
            foreach (var path in node.potentialPaths) Console.Write($" {path.NodeName}");
            if (node.previousNode != null) Console.Write(" or PREVIOUS to go back to the previous node");
            Console.WriteLine(".");
            return;
        }

        // Moves to the next node based on user input.
        public MazeNode MoveToNextNode(MazeNode currentNode, string direction)
        {
            if (direction.ToUpper() == "PREVIOUS") return currentNode.previousNode ?? currentNode;
            var nextNode = currentNode.potentialPaths.FirstOrDefault(p => p.NodeName.Equals(direction, StringComparison.OrdinalIgnoreCase));
            if (nextNode != null) return nextNode;
            Console.WriteLine("Invalid direction. Please choose a valid path.");
            return currentNode;
        }
    }
}
