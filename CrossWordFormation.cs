using System;

/*
Problem (from codefights.com Arcade): find how many ways to create a crossword puzzle of 4 words
Restrictions: 
- Each word must have two intersections, i.e. creating a box shaped crossword puzzle
- all words must be written either left to right or top to bottom
- the area of the rectangle formed inside the puzzle is NOT zero
*/
public class CrossWordFormation
{
    public static void Main(string[] args)
    {
        Console.WriteLine(crosswordFormation(new string[] { "africa", "america", "australia", "antarctica" }));
    }

    static int crosswordFormation(string[] words)
    {
        int solutions = 0; //counter for solutions, this will be returned
        int w = 0; //counter for which word position is currently being worked on
                   //w = 0 is first word (top), w = 1 is second (right), 2 is third (bottom),
                   //3 is fourth (left)
        int i = 0; //counter for which letter in the PREVIOUS word the current word
                   //should be matched to
        int j = 0; //counter for which letter in the CURRENT word matches
                   //letter i in the previous word
                   //a word is placed correctly if its jth letter matches previous word's ith letter
        int[,] order = new int[4, 3]; //array to keep track of which of the 4 words
                                      //is placed in which position
                                      //order[i, 0] indicates which word is in the ith position
                                      //order[i, 1] indicates which letter of the previous word this word starts on
                                      //order[i, 2] indicates which letter of word i intersects with previous word

        int x; //keeps track of horizontal displacement of new words from first word
        int y; //keeps track of vertical displacement from first word
               //x and y are used at the end to check whether crossword configuration is valid,
               //i.e. the xth letter in the first word equals the yth letter in the fourth word

    nextWord: //function loops here when one word has been successfully placed,
              //and next word needs to be placed

        w++; //iterate w to indicate next word
        if (w == 4) goto solution; //if w = 4, all words have been successfully placed
                                   //and the crossword configuration is valid, so go to solution

        wordCheck: //function loops here to iterate through each of the four words for a
                   //certain position

        order[w, 0]++; //iterate the word being checked

        if (order[w, 0] == 4) goto lastWord; //if order[w, 0] = 4 (there is no fifth word),
                                             //it means no words can fit in this position, so go to last word and change it

        //check to see if this word has already been used
        for (int a = 0; a < w; a++)
        {
            //if a word has appeared before, iterate to next word
            if (order[a, 0] == order[w, 0]) goto wordCheck;
        }

        //if checking the first position, theres no need to perform any of the below checks,
        //so immediately go to the next word
        if (w == 0) goto nextWord;

        //start i at last word's j + 1, which starts it at 1 past the intersection point.
        //when i iterates (i++), it will guarantee that the 4th condition is met
        //the area inside the crossword isn't zero
        i = order[w - 1, 2] + 1;

        //for placing the 4th word, i works a bit differently:
        //the 3rd word moves from left to right, not from the intersection forward
        //so i must start at the first letter of the 3rd word
        if (w == 3) i = -1;

        nextPlace: //function loops here when it needs to check the next letter of the previous
                   //word in order to place the current word
        i++; //iterate i to obtain the next place

        //for the 4th word's placement, i checks the 3rd word's letters from left to right,
        //which means we must make sure the checking stops before it "closes" the rectangle
        if (w == 3 && i >= order[2, 2] - 1) goto wordCheck;

        //if i reached the end of the previous word, the current word apparently cannot
        //be placed on this word. So check a new word to be placed
        if (i >= words[order[w - 1, 0]].Length) goto wordCheck;

        //start j at -1 so every letter of current word is checked
        j = -1;
        //if placing the third word, start j at 1 (will immediately iterate to 2), to avoid closing rectangle
        if (w == 2) j = 1;
        nextLetter: //function loops here when previous letter of current word didn't work,
                    //so next letter needs to be checked
        j++; //iterate to next letter

        //if j reaches the end of current word, this word cannot fit in this place,
        //so go to next place and try there

        if (j >= words[order[w, 0]].Length) goto nextPlace;

        //if jth letter of current word does NOT match ith letter of previous word,
        //check the next letter

        if (words[order[w, 0]][j] != words[order[w - 1, 0]][i]) goto nextLetter;

        if (w == 3)
        {
            //if this is the final word being placed (w = 3), check to see if the final word
            //aligns properly with the first word
            x = order[1, 1] - order[2, 2] + i;
            y = order[1, 2] - order[2, 1] + j;
            if (x < 0) goto nextPlace; //if x < 0, it means the 4th word is too far off to the left,
                                       //check next place to bring it further right
            if (y < 0) goto nextLetter; //if y < 0, it means the 4th word is too far below the first,
                                        //check next letter to bring it further up

            if (words[order[0, 0]][x] != words[order[3, 0]][y]) goto nextLetter;
            //if 4th and 1st words don't align properly, check next letter
        }

        //if all checks succeeded, assign i and j to the array,
        //and go to place next word
        order[w, 1] = i;
        order[w, 2] = j;
        goto nextWord;

    lastWord: //function loops here if assignment of current word completely failed
              //for every try, so there's no choice but to change the previous word

        order[w, 0] = -1; //assign -1 to current word, so that when nextWord is reached,
                          //it starts checking from 0
        w--; //decrement w to go back to previous word
        if (w == -1) return solutions; //if w reaches -1, it means every single possible
                                       //configuration has been checked, so return the number of solutions found

        //reset i and j values to match those of previous word
        i = order[w, 1];
        j = order[w, 2];

        if (w == 0) goto wordCheck; //if w = 0, placement cannot be checked, simply check next word
                                    //start checking other configurations by checking next letter of previous word
        goto nextLetter;

    solution: //function loops here once a working configuration is found

        solutions++; //add one to solutions counter
        w--; //this makes w = 3, so function can continue to search for more solutions
        goto nextLetter;
    }
}
