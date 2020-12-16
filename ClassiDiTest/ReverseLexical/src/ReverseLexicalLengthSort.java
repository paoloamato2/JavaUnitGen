/*
Write a function that sorts the words in a given string lexicographically (lexical sort) and by length in reverse order.
*/
import java.util.Arrays;
public class ReverseLexicalLengthSort {
public static String reverseSort(String str) {
    String x="";
    String[]y=str.split(" ");
    for(int i=0;i<y.length;i++){
    	if(y[i].length()>10&&Character.isUpperCase(y[i].charAt(0))) {
        y[i]=9+String.valueOf(y[i].charAt(0)).toLowerCase()+y[i]; 
    	}else if(y[i].length()>10) {
            y[i]=9+String.valueOf(y[i].charAt(0)).toLowerCase()+y[i]; 
    	}else if(Character.isUpperCase(y[i].charAt(0))) {
            y[i]=y[i].length()+String.valueOf((char)(y[i].charAt(0)+1)).toLowerCase()+y[i]; 
            System.out.println(y[i]);   
    }else {
    	y[i]=y[i].length()+y[i];
    	 System.out.println(y[i]);
     }
    }
     Arrays.sort(y);
    for(int j=y.length-1;j>=0;j--){
    if(Character.isUpperCase(y[j].charAt(2))){
    x=x+y[j].replaceAll("[0-9]","").substring(1)+" ";
	}else if(y[j].length()==10) {
	    x=x+y[j].substring(1)+" ";
	}else if(y[j].length()>9) {
    x=x+y[j].substring(2)+" ";
    }else{
    x=x+y[j].replaceAll("[0-9]","")+" ";
     }
    }
    return x.trim();
 }
}