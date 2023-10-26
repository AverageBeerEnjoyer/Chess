using ChessEngine;
using NUnit.Framework;

namespace TestProject1; 

[TestFixture]
public class KingTest {
    private Game game;

    [SetUp]
    public void initGame() {
        game = new Game();
    }
    [Test]
    public void illegalMoveTest1() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("e1 e1"));
    }
    [Test]
    public void illegalMoveTest2() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("e1 d1"));
    }
    [Test]
    public void illegalMoveTest3() {
        game.makeMove("d2 d4");
        game.makeMove("a7 a5");
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("e1 c3"));
    }
    [Test]
    public void legalMoveTest4() {
        game.makeMove("d2 d4");
        game.makeMove("a7 a5");
        Assert.DoesNotThrow(()=>game.makeMove("e1 d2"));
    }
}