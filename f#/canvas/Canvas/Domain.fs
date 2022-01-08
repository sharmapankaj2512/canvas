namespace Canvas

[<AutoOpen>]
module Domain =

    type Command =
        | CreateCanvas of Width: int * Height: int
        | Quit

    type Point = Point2D of X: int * Y: int

    type CommandSource = unit -> Command seq

    type Display =
        abstract Display : string -> unit
        abstract Display : Set<Point> -> unit

    type Repl = CommandSource -> Display -> unit
