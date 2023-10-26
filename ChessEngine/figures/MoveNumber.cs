namespace ChessEngine.figures {
    public enum MoveNumber {
        FIRST,
        SECOND,
        OTHER
    }

    static class MoveNumbers {
        public static MoveNumber next(MoveNumber moveNumber) {
            switch (moveNumber) {
                case MoveNumber.FIRST: return MoveNumber.SECOND;
                default: return MoveNumber.OTHER;
            }
        }

        public static MoveNumber abort(MoveNumber moveNumber) {
            switch (moveNumber) {
                case MoveNumber.OTHER: return MoveNumber.OTHER;
                default: return MoveNumber.FIRST;
            }
        }
    }
}