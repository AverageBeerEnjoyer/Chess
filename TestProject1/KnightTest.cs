using ChessEngine;
using NUnit.Framework;

namespace TestProject1; 

[TestFixture]
public class KnightTest {
    private Game game;

    [SetUp]
    public void initGame() {
        game = new Game();
    }
    [Test]
    public void illegalMoveTest1() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("b1 b1"));
    }
    [Test]
    public void illegalMoveTest2() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("b1 b4"));
    }
    [Test]
    public void illegalMoveTest3() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("b1 e1"));
    }
    [Test]
    public void illegalMoveTest4() {
        Assert.Throws<IllegalMoveException>(()=>game.makeMove("b1 d3"));
    }
    [Test]
    public void legalMoveTest1() {
        Assert.DoesNotThrow(()=>game.makeMove("b1 c3"));
    }
}