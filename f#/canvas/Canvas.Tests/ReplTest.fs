module Canvas.Tests.ReplTest

open Microsoft.FSharp.Core
open NUnit.Framework

type Command = CreateCanvas of Width: int * Height: int

type Point = Point2D of X: int * Y: int
type CommandSource = unit -> Command
type Display = Set<Point> -> unit

type Repl = CommandSource -> Display -> unit -> unit

let points =
    fun (width: int, height: int) ->
        set [ for x in 0 .. width - 1 do
                  for y in 0 .. height - 1 do
                      Point2D(x, y) ]

let repl: Repl =
    fun commandSource display ->
        match commandSource () with
        | CreateCanvas (w, h) -> fun () -> display (points (w, h))

[<SetUp>]
let Setup () = ()

[<Test>]
let CreateCanvasCommand () =
    let commandSource =fun () -> CreateCanvas(Width = 0, Height = 0)
    let display =fun points -> Assert.AreEqual(List<Point>.Empty, points)
    let start = repl commandSource display
    start ()
