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
            potentialPaths.Sort((x, y) => x.NodeName.CompareTo(y.NodeName));
        }
    }

    static class Program
    {
        static void Main()
        {
            PathGeneratorSingleton pathGenerator = PathGeneratorSingleton.GetInstance();
            StateKeeper stateKeeper = new StateKeeper();
            Console.WriteLine("Welcome to the Maze!");
            while (!stateKeeper.IsGameRunning())
            {
                Console.WriteLine("Select a difficult. EASY, MEDIUM, or HARD.");
                string? difficulty = Console.ReadLine();
                if (difficulty.ToUpper() == "EASY") stateKeeper.SetState(difficulty);
                else if (difficulty.ToUpper() == "MEDIUM") stateKeeper.SetState(difficulty);
                else if (difficulty.ToUpper() == "HARD") stateKeeper.SetState(difficulty);
                else Console.WriteLine("Invalid difficulty.");
            }
            MazeNode startNode = pathGenerator.GeneratePath(stateKeeper.context.Length(), stateKeeper.context.Complexity());
            MazeNode currentNode = startNode;
            while (!stateKeeper.IsGameOver())
            {
                pathGenerator.ShowPath(currentNode);
                string? userInput = Console.ReadLine();
                currentNode = pathGenerator.MoveToNextNode(currentNode, userInput);
                if (currentNode.FinalNode) stateKeeper.SetState("WIN");
            }
            Console.WriteLine("Congratulations! You've reached the end of the maze.");
        }
    }
}