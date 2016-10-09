//*****************************************************************
//                   Project #1 Press Your Luck
//                   Name: Nathan Ho & Ryan Luig
//                   CPL Date: 10/06/2016
//*****************************************************************
//    DataStructureClass: This class handles data from player's
//    name, answers, scores, spins, spins passed, and image array
//    as well as its data array.
//      
//*****************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

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
        public Image[] images = new Image[24];
        public int[] score = new int[24];
        private int tempValue=250;
        private int totalSpins;
        public DataStructureClass(int players)
        {
            numPlayers = players;
            playerData = new player[3];
            questions = new List<string>{};
            answers = new List<string>{};
            //Load array of 18 images of the tile for bigboard
            for (int i = 0; i <= 23; i++)
                images[i] = Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Big Board\\" + i + ".png");
            //Load data array of the 18 images values for bigboard
            for(int j=0;j<9;j++)
            {
                tempValue += 250;
                score[j] = tempValue;
            }
            int temp = 2500;
            for (int f = 0; f < 4;f++ )
            {
                temp += 500;
                score[f+9]=temp;
            }
            score[12] = 500;
            score[13] = 750;
            score[14] = 1000;
            score[15] = 1500;
            score[16] = 2000;
            score[17] = 2500;
            score[18] = 3000;
            score[19] = 4000;
            //Load whammy data.
            for (int k = 0; k < 4; k++)
                score[k + 20] = -1;
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
            if(playerData[PlayerId-1].ans == answer)

            {
                addPlayerSpins(PlayerId, 3); //3 spins for correct answer
            }
            else
            {
                addPlayerSpins(PlayerId, 1); //1 spin for incorrect answer
            }
        }
        public bool checkPlayerAnswer(int PlayerId, string answer)
        {
            if (playerData[PlayerId-1].ans == answer)
                return true;
            else
                return false;
        }
        //retrive question from array list
        public string getQuestion(int index)
        {
            index = index % questions.Capacity;
            if(index >= 0 && index < questions.Capacity)
            {
                return questions[index];
            }
            else { return "";}
        }
        // retrive answer from array list
        public string getAnswer(int index)
        {
            index = index % questions.Capacity;
            if(index >= 0 && index < answers.Capacity)
            {
                return answers[index];
            }
            else{ return "";}
        }
        // Return total amount of spins of all players
        public int getSpins()
        {
            int i=0;
            totalSpins = 0;
            do
            {
                totalSpins+=playerData[i].spins;
                totalSpins+=playerData[i].passedSpins;
                i++;
            }while(i<3);

            return totalSpins;
        }
        // getter:Player with lowest spins
        public int getLowestSpinsPlayer(int players)
        {
            int player = 0;
            int[] inputs=new int[players];
            for (int i = 0; i < players; i++)
                inputs[i] = playerData[i].spins;
            int lowest = inputs[0];
            foreach (var input in inputs)
                if (input < lowest) lowest = input;
            for (int j = players-1; j >= 0;j-- )
            {
                if (inputs[j] == lowest)
                    player = j+1;
            }
            return player;
        }
        // getter:Player Name
        public string getPlayerName(int playerID)
        {
            return playerData[playerID-1].name;
        }
        // getter:Player Score 
        public int getPlayerScore (int playerID)
        {
            return playerData[playerID-1].score;
        }
        // getter:Player Spins
        public int getPlayerSpins(int playerID)
        {
            return playerData[playerID-1].spins;
        }
        // getter:Player Passed Spins
        public int getPlayerPassedSpins(int playerID)
        {
            return playerData[playerID-1].passedSpins;
        }
        // getter:Player Answer
        public string getPlayerAns(int playerID)
        {
            return playerData[playerID - 1].ans;
        }
        // setter:Player Score
        public void addPlayerScore(int playerID, int addScore)
        {
            playerData[playerID-1].score += addScore;
        }
        // setter:Player Name    
        public void setPlayerName(int playerID, string playerName)
        {
            playerData[playerID - 1].name = playerName;
        }
        // setter:Player Answer
        public void setPlayerAnswer(int playerID, string playerAns)
        {
            playerData[playerID - 1].ans = playerAns.ToUpper();
        }
        // setter:Player Spins
        public void addPlayerSpins(int playerID, int numSpins)
        {
            playerData[playerID-1].spins += numSpins;
        }
        // setter:Player Passed Spins
        public void addPlayerPassedSpins(int playerID, int numSpins)
        {
            playerData[playerID-1].passedSpins += numSpins;
        }
        // Whammy reset
        public void ResetScore(int playerID)
        {
            playerData[playerID-1].score = 0;
        }
        // overloaded ToString for winner 
        public override string ToString()
        {
            string str, winner ="";
            int highScore = 0;
            for (int i = 0; i < 3; i++)
            {
                if(playerData[i].score > highScore)
                {
                    highScore = playerData[i].score ; 
                    winner = playerData[i].name;
                }
            }
            str = "Winner is: " + winner + "\nScore: " + highScore;
            return str;
        }
        
    }
}
