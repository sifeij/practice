using System;
using static System.Console;
class Solution {
    static void Main(String[] args) {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        var returnInput = Console.ReadLine().Split(' ');
        var y1 = Convert.ToInt32(returnInput[2]);
        var m1 = Convert.ToInt32(returnInput[1]);
        var d1 = Convert.ToInt32(returnInput[0]);
        
        var expectedInput = Console.ReadLine().Split(' ');
        var y = Convert.ToInt32(expectedInput[2]);
        var m = Convert.ToInt32(expectedInput[1]);
        var d = Convert.ToInt32(expectedInput[0]);
        
        var result = 0;
        
        result = y1 < y
                    ? 0
                    : y1 > y
                        ? 10000
                        : y1 == y && m1 < m
                            ? 0
                            : y1 == y && m1 > m
                                ? (m1-m) * 500
                                : y1 == y && m1 == m && d1 > d
                                    ? (d1-d) * 15
                                    : 0;
        
        WriteLine(result);
    }
}

/* input
9 6 2015
6 6 2015
*/

/* output
45
*/

/* input
2 6 2014
5 7 2014
*/

/* output
0
*/

/* input
2 6 2013
5 7 2014
*/

/* output
0
*/

/* input
2 6 2015
5 7 2014
*/

/* output
10000
*/
