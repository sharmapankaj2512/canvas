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
    [<TestCase("create 0 0\n", 0, 0)>]
    let parseCreateCanvasCommand (line: string, width: int, height: int) =
        let reader = new StringReader(line)
        Assert.AreEqual(CreateCanvas(width, height), consoleCommandSource reader ())

//    [<Test>]
//    [<Ignore("")>]
//    let parseMultipleCommands = ignore
