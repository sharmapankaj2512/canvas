namespace Canvas.Tests

open Canvas
open Microsoft.FSharp.Core
open NUnit.Framework
open Domain
open Repl

module ReplTest =

    type MockDisplay() =
        interface Display with
            member this.Display(points: Set<Point>) =
                Assert.AreEqual(List<Point>.Empty, points)

            member this.Display(message: string) = Assert.AreEqual("Good bye!", message)

    [<SetUp>]
    let Setup () = ()

    [<Test>]
    let CreateCanvasCommand () =
        let commandSource =
            fun () -> CreateCanvas(Width = 0, Height = 0)

        let start = repl commandSource (MockDisplay())
        start ()

    [<Test>]
    let QuitCommand () =
        let commandSource = fun () -> Quit
        let start = repl commandSource (MockDisplay())
        start ()
