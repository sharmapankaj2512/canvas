namespace Canvas

[<AutoOpen>]
module Domain =

    type Command =
        | CreateCanvas of Width: int * Height: int
        | Quit

    type Point = Point2D of X: int * Y: int

    type CommandSource = unit -> Command

    type Display =
        abstract Display : Set<Point> -> unit
        abstract Display : string -> unit

    type Repl = CommandSource -> Display -> unit -> unit
