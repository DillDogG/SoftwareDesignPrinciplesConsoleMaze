using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SoftwareDesignPrinciplesConsoleMaze
{
    internal class StateSystem
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
                return 5;
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

        class HardState : gameState
        {
            public int getLength()
            {
                return 6;
            }
            public int getComplexity()
            {
                return 2;
            }
            public bool getGameOver()
            {
                return false;
            }
        }

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
    }
}
