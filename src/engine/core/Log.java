package engine.core;

public class Log {

    private static final String RED = "\u001B[31m";
    private static final String RESET = "\u001B[0m";
    private static final String CYAN = "\u001B[36m";
    private static final String WHITE = "\u001B[37m";

    public static void info(String msg) {
        out(CYAN, msg);
    }

    public static void log(String msg) {
        out(WHITE, msg);
    }

    public static void error(String msg) {
        out(RED, msg);
    }

    private static void out(String colour, String msg) {
        System.out.println(colour + msg + RESET);
    }
}
