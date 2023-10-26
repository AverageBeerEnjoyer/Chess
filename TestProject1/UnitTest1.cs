using System;
using ChessEngine;
using NUnit.Framework;

namespace TestProject1;
[TestFixture]
public class Tests {
    [SetUp]
    public void setup() { }

    [Test]
    public void childCheckmateTest() {
        Game game = new Game();
        game.makeMove("e2 e4");
        game.makeMove("a7 a6");
        game.makeMove("e4 e5");
        game.makeMove("d7 d5");
        game.makeMove("e5 d6");
        Console.WriteLine(game.ToString());
        
        Assert.Pass();
    }

    [Test]
    public void illegalMoveTest1() {
        Game game = new Game();
        Assert.Throws<IllegalMoveException>(() => game.makeMove("a1 a3"));
    }

    [Test]
    public void illegalMoveTest2() {
        Game game = new Game();
        Assert.Throws<IllegalMoveException>(() => game.makeMove("b1 d2"));
    }

    [Test]
    public void illegalMoveTest3() {
        Game game = new Game();
        Assert.Throws<IllegalMoveException>(() => game.makeMove("c1 a3"));
    }

    [Test]
    public void illegalMoveTest4() {
        Game game = new Game();
        Assert.Throws<IllegalMoveException>(() => game.makeMove("a2 a5"));
    }

    [Test]
    public void illegalMoveTest5() {
        Game game = new Game();
        Assert.Throws<IllegalMoveException>(() => game.makeMove("a2 a5"));
    }
}