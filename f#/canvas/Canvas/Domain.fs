namespace Canvas

[<AutoOpen>]
module Domain =

    type Command =
        | CreateCanvas of Width: int * Height: int
        | Quit

    type Point = Point2D of X: int * Y: int

    type CommandSource = unit -> Command

    type DisplayMessage =
        abstract Display : string -> unit

    type DisplayPoints =
        abstract Display : Set<Point> -> unit

    type Display =
        inherit DisplayMessage
        inherit DisplayPoints

    type Repl = CommandSource -> Display -> unit -> unit
