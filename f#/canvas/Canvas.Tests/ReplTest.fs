namespace Canvas.Tests

open Canvas
open Microsoft.FSharp.Core
open NUnit.Framework
open Repl

module ReplTest =

    type MockDisplay(expectedPoints: Set<Point>, expectedMessage: string) =
        interface Display with
            member this.Display(points: Set<Point>) = Assert.AreEqual(expectedPoints, points)

            member this.Display(message: string) =
                Assert.AreEqual(expectedMessage, message)

    [<SetUp>]
    let Setup () = ()

    [<Test>]
    let CreateCanvasCommand () =
        let commandSource = fun () -> seq [ CreateCanvas(Width = 1, Height = 1) ]
        repl commandSource (MockDisplay(set [ Point2D(0, 0) ], ""))

    [<Test>]
    let QuitCommand () =
        let commandSource = fun () -> seq [ Quit ]
        repl commandSource (MockDisplay(set [], "Good bye!"))

    [<Test>]
    let MultipleCommands =
        let commandSource = fun () -> seq [ CreateCanvas(Width = 1, Height = 1); Quit ]
        repl commandSource (MockDisplay(set [], "Good bye!"))
