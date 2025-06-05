using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SoftwareDesignPrinciplesConsoleMaze
{
    interface gameState
    {
        int getLength();
        int getComplexity();
        bool getGameOver();
    }
    
    class StartState : gameState
    {
        public int getLength()
        {
            return 0;
        }
        public int getComplexity()
        {
            return 0;
        }
        public bool getGameOver()
        {
            return false;
        }
    }
    
    class WinState : gameState
    {
        public int getLength()
        {
            return 0;
        }
        public int getComplexity()
        {
            return 0;
        }
        public bool getGameOver()
        {
            return true;
        }
    }
    
    class EasyState : gameState
    {
        public int getLength()
        {
            return 4;
        }
        public int getComplexity()
        {
            return 0;
        }
        public bool getGameOver()
        {
            return false;
        }
    }
    
    class MediumState : gameState
    {
        public int getLength()
        {
            return 7;
        }
        public int getComplexity()
        {
            return 0;
        }
        public bool getGameOver()
        {
            return false;
        }
    }
    
    class HardState : gameState
    {
        public int getLength()
        {
            return 6;
        }
        public int getComplexity()
        {
            return 1;
        }
        public bool getGameOver()
        {
            return false;
        }
    }
    
    // States are used to allow for users to select their game difficulty. It also makes for easy changing the numbers on each difficulty.
    // It is also used in while statements to keep the user on selecting a difficulty, or preventing the game from ending early.
    class gameContext
    {
        public gameState state = new StartState();
    
        public void setState(gameState state)
        {
            this.state = state;
        }
    
        public int Length()
        {
            return state.getLength();
        }
    
        public int Complexity()
        {
            return state.getComplexity();
        }
    
        public bool GameOver()
        {
            return state.getGameOver();
        }
    }

    // The Facade system was used to hide the state system better, and make it more difficult to alter. It also contains all functions that are done based on the current state.
    class StateKeeper
    {
        StartState startState = new StartState();
        WinState winState = new WinState();
        EasyState easyState = new EasyState();
        MediumState mediumState = new MediumState();
        HardState hardState = new HardState();

        public gameContext context = new gameContext();

        public StateKeeper()
        {
            context.setState(startState);
        }

        public void SetState(string state)
        {
            if (state.ToUpper() == "EASY") context.setState(easyState);
            else if (state.ToUpper() == "MEDIUM") context.setState(mediumState);
            else if (state.ToUpper() == "HARD") context.setState(hardState);
            else if (state.ToUpper() == "WIN") context.setState(winState);
            else context.setState(startState);
        }

        public bool IsGameOver()
        {
            return context.GameOver();
        }

        public bool IsGameRunning()
        {
            if (context.state is EasyState || context.state is MediumState || context.state is HardState) return true;
            else return false;
        }
    }
}
