namespace Canvas.Tests

open System.IO
open Canvas
open NUnit.Framework

module ReadCommandsTest =

    let consoleCommandSource (reader: TextReader) (): Command =
        let line = reader.ReadLine()
        if (line.StartsWith("create")) then
            CreateCanvas(0, 0)
        else
            Quit

    [<Test>]
    let parseQuitCommand () =
        let reader = new StringReader("quit\n")
        Assert.AreEqual(Quit, consoleCommandSource reader ())

    [<Test>]
    let parseCreateCanvasCommand () =
        let reader = new StringReader("create 0 0\n")
        Assert.AreEqual(CreateCanvas(0, 0), consoleCommandSource reader ())

//    [<Test>]
//    [<Ignore("")>]
//    let parseMultipleCommands = ignore
