using System;

/*
Problem (from codefights.com Arcade): various problems involving binary operations
*/

public class BinaryOps
{
    public static void main(String[] args)
    {
        swapAdjacentBits(Int32.Parse(args[0]));
    }

    //given a number n in range [0, 2^30), swap each pair of adjacent bits and return the new integer.
    //for example: if n = 13 (b 11 01), it will return 14 (b 11 10)
    //or if n = 74 (b 01 00 10 10), it will return 133 (10 00 01 01)
    static int swapAdjacentBits(int n)
    {
        return ((n >> 1) & 357913941) + ((n << 1) & 715827882);
        //357813941 is the decimal value of the bitmask 01 01 01 01 01 01 01 01 01 01 01 01 01 01 01
        //by shifting n 1 bit right and performing & operation with this bitmask, we shift all original odd bits
        //into even positions, and mask the new odd bits, which we don't care about
        //similarly, we do the same for the original even bits and move them into odd positions by doing n << 1.
        //then & it with the bitmask 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 (715827882 in decimal)
    }
}
