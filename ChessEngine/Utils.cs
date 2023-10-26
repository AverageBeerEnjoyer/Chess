namespace ChessEngine {
    public class Utils {
        public static bool isOnField(int x, int y) {
            return x > -1 && x < 8 && y > -1 && y < 8;
        }

        public static int[] parseMove(string move) {
            string[] coords = move.Split(" ");
            int[] res = new int[4];
            res[0] = coords[0][0] - 'a';
            res[1] = coords[0][1] - '1';
            res[2] = coords[1][0] - 'a';
            res[3] = coords[1][1] - '1';
            return res;
        }

        public static int letterToNumber(char l) {
            return l - 'a';
        }

        public static char numberToLetter(int n) {
            return (char)('a' + n);
        }
    }
}