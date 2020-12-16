import java.util.*;

//Write a Java program to find next smallest palindrome.

class Palindromo {

    public static int nextPalindromeGenerate(int n)
    {   
        int ans=1, digit, rev_num=0, num;                
       //For single digit number, next smallest palindrome is n+1       
	   if(n<10)
        {   
            ans=0;
			return n+1;
        }

        num=n;
        while(ans!=0)
        {   rev_num=0;digit=0;
            n=++num;

            while(n>0)      //rev_numersing the number
            {
                digit=n%10;
                rev_num=rev_num*10+digit;
                n=n/10;
            }

            if(rev_num==num)   
            {
                ans=0;
				return num;
            }

            else ans=1;
        }
		boolean test = isPalindrome(String.valueOf(num));
		return num;
    }
	
	public static boolean isPalindrome(String str) 
    { 
  
        // Pointers pointing to the beginning 
        // and the end of the string 
        int i = 0, j = str.length() - 1; 
  
        // While there are characters toc compare 
        while (i < j) { 
  
            // If there is a mismatch 
            if (str.charAt(i) != str.charAt(j)) 
                return false; 
  
            // Increment first pointer and 
            // decrement the other 
            i++; 
            j--; 
        } 
  
        // Given string is a palindrome 
        return true; 
    } 

    public static void main(String[] args)
    {   
       Scanner scan = new Scanner(System.in);
       System.out.print("Input the number: ");
       int n = scan.nextInt();
       if (n>0)
		{	
		 System.out.println("Next smallest palindrome:" + nextPalindromeGenerate(n));
		}         
   }
}