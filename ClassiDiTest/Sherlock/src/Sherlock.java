/*

Sherlock considers a string to be valid if all characters of the string appear the same number of times. It is also valid if he can remove just 1 character at 1 index in the string, and the remaining characters will occur the same number of times. Given a string str, determine if it is valid. If so, return "YES", otherwise return "NO".

For example, If str = "abc", the string is valid because the frequencies of characters are all the same. If str = "abcc", the string is also valid, because we can remove 1 "c" and have one of each character remaining in the string. However, if str = "abccc", the string is not valid, because removing one character does not result in the same frequency of characters.


*/
public class Sherlock {
  public static String isValid(String str) {
		char[] chars = str.toCharArray();
		
		int defaultNum = 0;
		int charsWithDefaultNum = 0;
		boolean changingChar = false;
		char changingCharChar = '#';
		char lastChar = '#';
		
		for(char c : chars) {
			int count = 0;
			
			for(char d : chars) {
				if(c == d)
					count++;
			}
			
			if(defaultNum == 0) {
				defaultNum = count;
				charsWithDefaultNum = 1;
			} 
			else if(Math.abs(defaultNum - count) >= 2) return "NO";
			else if(Math.abs(defaultNum - count) == 1) {
				if(!changingChar) {
					changingChar = true;
					changingCharChar = c;
				}
				else if(charsWithDefaultNum == 1) {
					charsWithDefaultNum = 1;
					changingCharChar = lastChar;
				}
				else if(c != changingCharChar) {
					return "NO";
				}
			}
			else {
				charsWithDefaultNum ++;
			}
			
			lastChar = c;
		}
		
		return "YES";
  }
}