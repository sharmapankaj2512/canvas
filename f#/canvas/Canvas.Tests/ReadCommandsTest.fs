namespace Canvas.Tests

open System.IO
open Canvas
open NUnit.Framework

module ReadCommandsTest =

    let consoleCommandSource (reader: TextReader) () = Quit
    
    [<Test>]
    let parseQuitCommand () =
        let reader = new StringReader("quit\n")
        Assert.AreEqual(Quit, consoleCommandSource reader ())

    