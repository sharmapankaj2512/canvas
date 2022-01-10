namespace Canvas.Tests

open System.IO
open Canvas
open NUnit.Framework

module ReadCommandsTest =

    let consoleCommandSource (reader: TextReader) (): Command =
        let line = reader.ReadLine().Trim()
        if (line.StartsWith("create")) then
            let tokens = line.Split(" ")
            let width = tokens[1] |> int
            let height = tokens[2] |> int
            CreateCanvas(width, height)
        else
            Quit

    [<Test>]
    let parseQuitCommand () =
        let reader = new StringReader("quit\n")
        Assert.AreEqual(Quit, consoleCommandSource reader ())

    [<Test>]
    [<TestCase("create 0 0\n", 0, 0)>]
    [<TestCase("create 1 1\n", 1, 1)>]
    [<TestCase("create 12 01\n", 12, 1)>]
    [<TestCase(" create 0 0\n", 0, 0)>]
    let parseCreateCanvasCommand (line: string, width: int, height: int) =
        let reader = new StringReader(line)
        Assert.AreEqual(CreateCanvas(width, height), consoleCommandSource reader ())

//    [<Test>]
//    [<Ignore("")>]
//    let parseMultipleCommands = ignore
