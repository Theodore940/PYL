using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PressYourLuck
{
    class DataStructureClass
    {
        private struct player
        {
            public int score, spins, passedSpins;
            public string name;
        }

        private int numPlayers;
        private List<string> questions; 
        private List<string> answers;
        private player[] playerData;

        public DataStructureClass(int players)
        {
            numPlayers = players;
            playerData = new player[numPlayers];
            questions = new List<string>{};
            answers = new List<string>{};

            string line;

            //load question and answers from file
            System.IO.StreamReader fileQA = new System.IO.StreamReader("Questions.txt");

            while ((line = fileQA.ReadLine()) != null)
            {
                questions.Add(line);
                line = fileQA.ReadLine();
                answers.Add(line);
            }
        }

        public string getQuestion(int index)
        {
            if(index >= 0 && index < questions.Capacity)
            {
                return questions[index];
            }
            else { return "";}
        }

        public string getAnswer(int index)
        {
            if(index >= 0 && index < answers.Capacity)
            {
                return answers[index];
            }
            else{ return "";}
        }

            
        public string getPlayerName(int playerID)
        {
            return playerData[playerID-1].name;
        }

        public int getPlayerScore (int playerID)
        {
            return playerData[playerID-1].score;
        }

        public int getPlayerSpins(int playerID)
        {
            return playerData[playerID-1].spins;
        }

        public int getPlayerPassedSpins(int playerID)
        {
            return playerData[playerID-1].passedSpins;
        }

        public void addPlayerScore(int playerID, int addScore)
        {
            playerData[playerID-1].score += addScore;
        }
            
        public void setPlayerName(int playerID, string playerName)
        {
            playerData[playerID - 1].name = playerName;
        }

        public void addPlayerSpins(int playerID, int numSpins)
        {
            playerData[playerID-1].spins += numSpins;
        }
            
        public void addPlayerPassedSpins(int playerID, int numSpins)
        {
            playerData[playerID-1].passedSpins += numSpins;
        }
                
    }
}
