using ChessEngine;
using NUnit.Framework;

namespace TestProject1; 

[TestFixture]
public class QueenTest {
    private Game game;

    [SetUp]
    public void initGame() {
        game = new Game();
    }
    
    [Test]
    public void illegalMoveTest1() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("d1 d1"));
    }
    [Test]
    public void illegalMoveTest2() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("d1 d5"));
    }
    [Test]
    public void illegalMoveTest3() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("d1 c3"));
    }
    [Test]
    public void legalMoveTest1() {
        game.makeMove("e2 e4");
        game.makeMove("e7 e5");
        Assert.DoesNotThrow(()=>game.makeMove("d1 f3"));
    }
}