module Canvas.Tests.ReplTest

open Microsoft.FSharp.Core
open NUnit.Framework

type Command = CreateCanvas of Width: int * Height: int

type Point2D = { X: int; Y: int }
type CommandSource = unit -> Command
type Display = Point2D list -> unit

type Repl = CommandSource -> Display -> unit -> unit

let points = fun (width, height) -> List<Point2D>.Empty

let repl: Repl =
    fun commandSource display ->
        match commandSource () with
        | CreateCanvas (w, h) -> fun () -> display (points (w, h))

[<SetUp>]
let Setup () = ()

[<Test>]
let CreateCanvasCommand () =
    let commandSource = fun () -> CreateCanvas(Width = 0, Height = 0)
    let display = fun points -> Assert.AreEqual(List<Point2D>.Empty, points)
    let start = repl commandSource display
    start ()
