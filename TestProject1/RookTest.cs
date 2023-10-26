using ChessEngine;
using NUnit.Framework;

namespace TestProject1; 

[TestFixture]
public class RookTest {
    private Game game;

    [SetUp]
    public void initGame() {
        game = new Game();
    }
    [Test]
    public void illegalMoveTest1() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("a1 a1"));
    }
    
    [Test]
    public void illegalMoveTest2() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("a1 a5"));
    }
    [Test]
    public void illegalMoveTest3() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("a1 b3"));
    }
    [Test]
    public void illegalMoveTest4() {
        game.makeMove("b2 b3");
        game.makeMove("a7 a5");
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("a1 c3"));
    }
    [Test]
    public void legalMoveTest() {
        game.makeMove("a2 a4");
        game.makeMove("a7 a5");
        Assert.DoesNotThrow(()=>game.makeMove("a1 a3"));
    }
}