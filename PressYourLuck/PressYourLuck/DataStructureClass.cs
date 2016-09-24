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
            public string ans; //place to store a players answer (might not be needed)
        }

        private int numPlayers;
        private List<string> questions; 
        private List<string> answers;
        private player[] playerData;

        public DataStructureClass(int players)
        {
            string line;
            //load question and answers from file
            System.IO.StreamReader fileQA = new System.IO.StreamReader("Questions.txt");


            //TODO: make sure this doesn't break because a Q doesn't have an A
            while ((line = fileQA.ReadLine()) != null)
            {
                questions.Add(line);
                line = fileQA.ReadLine();
                answers.Add(line);

            }

            // TODO: initialize playerData 


        }
    }
}
