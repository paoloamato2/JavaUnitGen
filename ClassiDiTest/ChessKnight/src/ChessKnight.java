import java.util.ArrayList;
import java.util.List;

/*
You will be given the location of a knight, and an end location. The knight can move in a "L" shape. "L" shape movement means that the knight can change it's x coordinate by 2 and it's y coordinate by 1 or it can change it's y coordinate by 2 and it's x coordinate by 1 (you can add and subtract from the x/y).

For example, if the knight is at the position (0, 0), it can move to:

(1,2), (1,-2), (2,1), (2,-1), (-1,2), (-1,-2), (-2,1), (-2, -1)

Your job is to return the least amount of steps needed to go from the position K (knight's start position) to E (end). You will only be given the knight starter coordinates (x1, y1) and the end coordinates (x2, y2).

*/

public class ChessKnight {
    static List<Location> locationList = new ArrayList<Location>();

    public static int knightBFS(int x1, int y1, int x2, int y2) {

        System.out.println("X1 = " + x1 + ", Y1 = " + y1 + ", X2 = " + x2 + ", Y2 = " + y2);

        locationList.add(new Location(0, new int[]{x1, y1}));

        while (find(x2, y2) == null) {
            int size = locationList.size();
            for (int i=0; i<size; i++) {
                Location location = locationList.get(i);
                ArrayList<Location> jumpedHorseLocations = location.findPossibleMoves();
                for (Location newLocation : jumpedHorseLocations) {
                    if (find(newLocation) != null) {
                        find(newLocation).setSteps(Math.min(newLocation.getSteps(), find(newLocation).getSteps()));
                    } else {
                        locationList.add(newLocation);
                    }
                }
            }
        }

        int returnValue = find(x2, y2).getSteps();

        locationList = new ArrayList<Location>();

        return returnValue;
    }

    public static Location find(Location locationToFind) {
        int xLocation = locationToFind.getCoordinates()[0];
        int yLocation = locationToFind.getCoordinates()[1];
        for (Location location : locationList) {
            int x = location.getCoordinates()[0];
            int y = location.getCoordinates()[1];
            if (xLocation == x && yLocation == y) {
                return location;
            }
        }
        return null;
    }

    public static Location find(int x, int y) {
        for (Location location : locationList) {
            int x1 = location.getCoordinates()[0];
            int y1 = location.getCoordinates()[1];
            if (x1 == x && y1 == y) {
                return location;
            }
        }
        return null;
    }
}

 class Location {

    private int steps;
    private int[] coordinates;

    public Location(int steps, int[] coordinates) {
        this.steps = steps;
        this.coordinates = coordinates;
    }

    public ArrayList<Location> findPossibleMoves() {
        ArrayList<Location> possibleMoves = new ArrayList<Location>();
        for (int x = -2; x <= 2; x += 4) {
            for (int y = -1; y <= 1; y += 2) {
                int newX = this.getCoordinates()[0] + x;
                int newY = this.getCoordinates()[1] + y;
                if (newX <= 8 && newX >= 1 && newY <= 8 && newY >= 1) {
                    possibleMoves.add(new Location(this.getSteps() + 1, new int[]{newX, newY}));
                }
            }
        }
        for (int y = -2; y <= 2; y += 4) {
            for (int x = -1; x <= 1; x += 2) {
                int newX = this.getCoordinates()[0] + x;
                int newY = this.getCoordinates()[1] + y;
                if (newX <= 8 && newX >= 1 && newY <= 8 && newY >= 1) {
                    possibleMoves.add(new Location(this.getSteps() + 1, new int[]{newX, newY}));
                }
            }
        }
        return possibleMoves;
    }


    public int getSteps() {
        return steps;
    }

    public void setSteps(int steps) {
        this.steps = steps;
    }

    public int[] getCoordinates() {
        return coordinates;
    }

    public void setCoordinates(int[] coordinates) {
        this.coordinates = coordinates;
    }
}