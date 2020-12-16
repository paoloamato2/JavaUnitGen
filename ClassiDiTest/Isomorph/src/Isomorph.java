
/*
Given two strings s and t, create a function to determine if they are isomorphic. Two strings are isomorphic if the characters in s can be replaced to get t. All occurrences of a character must be replaced with another character while preserving the order of characters. No two characters may map to the same character but a character may map to itself.
*/

import java.util.*;
public class Isomorph {
	public static boolean isIsomorphic(String s, String t) {
			ArrayList<Integer> intlist = new ArrayList<>();
			LinkedHashMap<String,String> hashmap = new LinkedHashMap();
			LinkedHashMap<String,String> hashmap2 = new LinkedHashMap();
			for(int i = 0; i < s.length(); i++){
				if(hashmap.containsKey(Character.toString(s.charAt(i)))){
					hashmap.put(Character.toString(s.charAt(i)),hashmap.get(Character.toString(s.charAt(i))) + String.valueOf(i));
				}
				else{
					hashmap.put(Character.toString(s.charAt(i)),String.valueOf(i));
				}
			}
			for(int i = 0; i < t.length(); i++){
				if(hashmap2.containsKey(Character.toString(t.charAt(i)))){
					hashmap2.put(Character.toString(t.charAt(i)),hashmap2.get(Character.toString(t.charAt(i))) + String.valueOf(i));
				}
				else{
					hashmap2.put(Character.toString(t.charAt(i)),String.valueOf(i));
				}
			}
			String emptystring = "";
			String emptystring2 = "";
			for(String eachkey: hashmap.keySet()) {
				emptystring += hashmap.get(eachkey);
			}
			for(String eachkey: hashmap2.keySet()) {
				emptystring2 += hashmap2.get(eachkey);
			}
			return emptystring2.equals(emptystring);
		}
}