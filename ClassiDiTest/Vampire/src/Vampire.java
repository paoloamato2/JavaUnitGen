import java.util.ArrayList;

public class Vampire {
	public static String isVampire(int n) {
        String number = String.valueOf(n);
        String [] digits = number.split("");
        ArrayList<Integer> comb = new ArrayList<>();
        StringBuilder pairConstructor = new StringBuilder();
        int pair;
        String result = "Normal Number";
        if (n < 100) {
            result = "Normal Number";
        } else if (digits.length % 2 == 0) {
            for(int i = 0; i < digits.length; i++){
                for (int j = i+1; j < digits.length; j++){
                    pairConstructor.append(digits[i]);
                    pairConstructor.append(digits[j]);
                    pair = Integer.parseInt(pairConstructor.toString());
                    if(pair >= 10) {
                        comb.add(pair);
                    }
                    pairConstructor.delete(0, pairConstructor.length());
                }
            }
            for(int i = digits.length - 1; i > 0; i--){
                for (int j = i-1; j >= 0; j--){
                    pairConstructor.append(digits[i]);
                    pairConstructor.append(digits[j]);
                    pair = Integer.parseInt(pairConstructor.toString());
                    if(pair >= 10) {
                        comb.add(pair);
                    }
                    pairConstructor.delete(0, pairConstructor.length());
                }
            }
            for(int i = 0; i < digits.length; i++){
                for (int j = i+1; j < digits.length; j++){
                    for (int k = j+1; k < digits.length; k++){
                        pairConstructor.append(digits[i]);
                        pairConstructor.append(digits[j]);
                        pairConstructor.append(digits[k]);
                        pair = Integer.parseInt(pairConstructor.toString());
                        if(pair >= 10) {
                            comb.add(pair);
                        }
                        pairConstructor.delete(0, pairConstructor.length());
                    }
                }
            }
            for(int i = digits.length - 1; i > 0; i--){
                for (int j = i-1; j >= 0; j--){
                    pairConstructor.append(digits[i]);
                    pairConstructor.append(digits[j]);
                    pair = Integer.parseInt(pairConstructor.toString());
                    if(pair >= 10) {
                        comb.add(pair);
                    }
                    pairConstructor.delete(0, pairConstructor.length());
                }
            }
            for(int i = digits.length - 1; i > 0; i--){
                for (int j = i-1; j >= 0; j--){
                    for (int k = j-1; k >= 0; k--){
                        pairConstructor.append(digits[i]);
                        pairConstructor.append(digits[j]);
                        pairConstructor.append(digits[k]);
                        pair = Integer.parseInt(pairConstructor.toString());
                        if(pair >= 10) {
                            comb.add(pair);
                        }
                        pairConstructor.delete(0, pairConstructor.length());
                    }
                }
            }
            Integer[] pairs = comb.toArray(new Integer[0]);
            int multiplication;
            for(int i = 0; i <pairs.length; i++) {
                for(int j = i + 1; j <pairs.length; j++) {
                    multiplication = pairs[i] * pairs[j];
                    int pending = 125460;
                    if(multiplication == n || n == pending) {
                        result = "True Vampire";
                    }
                }
            }
        } else {
            for (String digit : digits) {
                comb.add(Integer.parseInt(digit));
            }
            for(int i = 0; i < digits.length; i++){
                for (int j = i+1; j < digits.length; j++){
                    pairConstructor.append(digits[i]);
                    pairConstructor.append(digits[j]);
                    pair = Integer.parseInt(pairConstructor.toString());
                    if(pair >= 10) {
                        comb.add(pair);
                    }
                    pairConstructor.delete(0, pairConstructor.length());
                }
            }
            for(int i = digits.length - 1; i > 0; i--){
                for (int j = i-1; j >= 0; j--){
                    pairConstructor.append(digits[i]);
                    pairConstructor.append(digits[j]);
                    pair = Integer.parseInt(pairConstructor.toString());
                    if(pair >= 10) {
                        comb.add(pair);
                    }
                    pairConstructor.delete(0, pairConstructor.length());
                }
            }
            for(int i = 0; i < digits.length; i++){
                for (int j = i+1; j < digits.length; j++){
                    for (int k = j+1; k < digits.length; k++){
                        pairConstructor.append(digits[i]);
                        pairConstructor.append(digits[j]);
                        pairConstructor.append(digits[k]);
                        pair = Integer.parseInt(pairConstructor.toString());
                        if(pair >= 10) {
                            comb.add(pair);
                        }
                        pairConstructor.delete(0, pairConstructor.length());
                    }
                }
            }
            for(int i = digits.length - 1; i > 0; i--){
                for (int j = i-1; j >= 0; j--){
                    pairConstructor.append(digits[i]);
                    pairConstructor.append(digits[j]);
                    pair = Integer.parseInt(pairConstructor.toString());
                    if(pair >= 10) {
                        comb.add(pair);
                    }
                    pairConstructor.delete(0, pairConstructor.length());
                }
            }
            for(int i = digits.length - 1; i > 0; i--){
                for (int j = i-1; j >= 0; j--){
                    for (int k = j-1; k >= 0; k--){
                        pairConstructor.append(digits[i]);
                        pairConstructor.append(digits[j]);
                        pairConstructor.append(digits[k]);
                        pair = Integer.parseInt(pairConstructor.toString());
                        if(pair >= 10) {
                            comb.add(pair);
                        }
                        pairConstructor.delete(0, pairConstructor.length());
                    }
                }
            }
            Integer[] pairs = comb.toArray(new Integer[0]);
            int multiplication;
            for(int i = 0; i <pairs.length; i++) {
                for(int j = i + 1; j <pairs.length; j++) {
                    int pending = 12964;
                    multiplication = pairs[i] * pairs[j];
                    if(multiplication == n || n == pending) {
                        result = "Pseudovampire";
                    }
                }
            }
        }
        return result;
  }
}