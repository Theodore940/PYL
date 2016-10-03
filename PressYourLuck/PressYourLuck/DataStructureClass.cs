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
            public string name, ans;
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
            System.IO.StreamReader fileQA = new System.IO.StreamReader("luckfile.txt");

            while ((line = fileQA.ReadLine()) != null)
            {
                
                questions.Add(line.ToUpper());
                line = fileQA.ReadLine();
                answers.Add(line.ToUpper());
            }
        }

        //takes the player id and a string containing the correct answer
        //as parameters and checks the ans stored in that player's PlayerData index
        //as ans
        public void checkAnswer(int PlayerId, string answer)
        {
            if (playerData[PlayerId - 1].ans == answer)
            {
                addPlayerSpins(PlayerId, 3); //3 spins for correct answer
            }
            else
            {
                addPlayerSpins(PlayerId, 1); //1 spin for incorrect answer
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

        public string getPlayerAns(int playerID)
        {
            return playerData[playerID - 1].ans;
        }

        public void addPlayerScore(int playerID, int addScore)
        {
            playerData[playerID-1].score += addScore;
        }
            
        public void setPlayerName(int playerID, string playerName)
        {
            playerData[playerID - 1].name = playerName;
        }

        public void setPlayerAnswer(int playerID, string playerAns)
        {
            playerData[playerID - 1].ans = playerAns;
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
