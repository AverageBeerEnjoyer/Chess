using NUnit.Framework;
using ChessEngine;

namespace TestProject1; 

[TestFixture]
public class BishopTest {
    private Game game;

    [SetUp]
    public void initGame() {
        game = new Game();
    }
    [Test]
    public void illegalMoveTest1() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("c1 c1"));
    }
    [Test]
    public void illegalMoveTest2() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("c1 a3"));
    }
    [Test]
    public void illegalMoveTest3() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("c1 c5"));
    }
    [Test]
    public void legalMoveTest1() {
        game.makeMove("b2 b3");
        game.makeMove("a7 a5");
        Assert.DoesNotThrow(()=>game.makeMove("c1 a3"));
    }
    
}