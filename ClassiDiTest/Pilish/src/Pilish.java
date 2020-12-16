/*
In this challenge, transform a string into a series of words (or sequences of characters) separated by a single space, with each word having the same length given by the first 15 digits of the decimal representation of Pi:
*/

public class Pilish {
    static final int[] PI = new int[]{3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9};

    public static String pilish_string(String s) {
        String ans = "";
        int idx = 0;
        int l;
        String word;
        while(s.length() > 0 && idx < PI.length){
            l = PI[idx];
            if(s.length() >= l)
                word = s.substring(0, l);
            else
                word = s;
            while(word.length() < l)
                word += word.charAt(word.length() - 1);
            ans += " " + word;
            if(s.length() >= l)
                s = s.substring(l);
            else
                s = "";
            idx++;
        }
        return ans.trim();
    }
}