module Canvas.Tests.ReplTest

open Microsoft.FSharp.Core
open NUnit.Framework

type CreateCanvas = { Width: int; Height: int }

type Point2D = { X: int; Y: int }
type CommandSource = unit -> CreateCanvas
type Display = Point2D list -> unit

type Repl = CommandSource -> Display -> unit -> unit

let repl: Repl =
    fun commandSource display -> fun () -> display List<Point2D>.Empty



[<SetUp>]
let Setup () = ()

[<Test>]
let CreateCanvasCommand () =
    let commandSource = fun () -> { Width = 0; Height = 0 }
    let display =
        fun points -> Assert.AreEqual(List<Point2D>.Empty, points)
    let start = repl commandSource display
    start ()
