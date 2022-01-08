module Canvas.Tests.ReplTest

open Microsoft.FSharp.Core
open NUnit.Framework

type Command =
    | CreateCanvas of Width: int * Height: int
    | Quit

type Point = Point2D of X: int * Y: int
type CommandSource = unit -> Command
type Display =
    abstract Display: Set<Point> -> unit
    abstract Display: string -> unit

type Repl = CommandSource -> Display -> unit -> unit

let points =
    fun (width: int, height: int) ->
        set [ for x in 0 .. width - 1 do
                  for y in 0 .. height - 1 do
                      Point2D(x, y) ]

let repl: Repl =
    fun commandSource display ->
        match commandSource () with
        | CreateCanvas (w, h) -> fun () -> display.Display (points (w, h))
        | Quit -> fun() -> display.Display("Good bye!")

type MockDisplay () =
   interface Display with
      member this.Display(points: Set<Point>) = Assert.AreEqual(List<Point>.Empty, points)
      member this.Display(message: string) = Assert.AreEqual("Good bye!", message)

[<SetUp>]
let Setup () = ()

[<Test>]
let CreateCanvasCommand () =
    let commandSource = fun () -> CreateCanvas(Width = 0, Height = 0)    
    let start = repl commandSource (MockDisplay())
    start ()

[<Test>]
let QuitCommand () =
    let commandSource =fun () -> Quit    
    let start = repl commandSource (MockDisplay())
    start()