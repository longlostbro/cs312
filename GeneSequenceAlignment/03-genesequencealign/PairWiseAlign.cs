using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticsLab
{
    class PairWiseAlign
    {
        int MaxCharactersToAlign;

        public PairWiseAlign()
        {
            // Default is to align only 5000 characters in each sequence.
            this.MaxCharactersToAlign = 5000;
        }

        public PairWiseAlign(int len)
        {
            // Alternatively, we can use an different length; typically used with the banded option checked.
            this.MaxCharactersToAlign = len;
        }

        /// <summary>
        /// this is the function you implement.
        /// </summary>
        /// <param name="sequenceA">the first sequence</param>
        /// <param name="sequenceB">the second sequence, may have length not equal to the length of the first seq.</param>
        /// <param name="banded">true if alignment should be band limited.</param>
        /// <returns>the alignment score and the alignment (in a Result object) for sequenceA and sequenceB.  The calling function places the result in the dispay appropriately.
        /// 
        public ResultTable.Result Align_And_Extract(Tuple<int,int> cell, GeneSequence sequenceA, GeneSequence sequenceB, bool banded)
        {
            ResultTable.Result result = new ResultTable.Result();
            int score;                                                       // place your computed alignment score here
            string[] alignment = new string[2];                              // place your two computed alignments here


            // ********* these are placeholder assignments that you'll replace with your code  *******
            string a = sequenceA.Sequence.Substring(0, sequenceA.Sequence.Length < MaxCharactersToAlign ? sequenceA.Sequence.Length : MaxCharactersToAlign);
            string b = sequenceB.Sequence.Substring(0, sequenceB.Sequence.Length < MaxCharactersToAlign ? sequenceB.Sequence.Length : MaxCharactersToAlign);
            DynamicProgramming dp = new DynamicProgramming(cell, a, b, banded);
            score = dp.getScore();                                                
            alignment[0] = dp.getResultA();
            alignment[1] = dp.getResultB();
            // ***************************************************************************************
            

            result.Update(score,alignment[0],alignment[1]);                  // bundling your results into the right object type 
            return(result);
        }
    }
}
