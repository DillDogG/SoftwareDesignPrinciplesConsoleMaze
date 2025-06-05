namespace SoftwareDesignPrinciplesConsoleMaze
{
    public class MazeNode
    {
        public List<MazeNode> potentialPaths;

        public bool FinalNode = false;

        public MazeNode? previousNode;

        public string NodeName { get; set; }

        public MazeNode()
        {
            potentialPaths = new List<MazeNode>();
            NodeName = string.Empty;
        }

        public void MakeNode(List<MazeNode> paths, MazeNode previous, string name, bool end = false)
        {
            foreach (var path in paths)
            {
                AddPath(path);
            }
            previousNode = previous;
            NodeName = name;
            FinalNode = end;
        }

        public void AddPath(MazeNode node)
        {
            potentialPaths.Add(node);
        }
    }

    static class Program
    {
        static void Main()
        {
            PathGeneratorSingleton pathGenerator = PathGeneratorSingleton.GetInstance();
            MazeNode startNode = pathGenerator.GeneratePath(5);
            MazeNode currentNode = startNode;
            Console.WriteLine("Welcome to the Maze!");
            while (!currentNode.FinalNode)
            {
                pathGenerator.ShowPath(currentNode);
                string? userInput = Console.ReadLine();
                currentNode = pathGenerator.MoveToNextNode(currentNode, userInput);
            }
            Console.WriteLine("Congratulations! You've reached the end of the maze.");
        }
    }
}