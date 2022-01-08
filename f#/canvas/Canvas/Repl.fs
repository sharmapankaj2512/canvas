namespace Canvas

open Canvas

module Repl =
    let handle (display: Display) (command: Command) =
        match command with
        | CreateCanvas (w, h) -> display.Display(points (w, h))
        | Quit -> display.Display("Good bye!")

    let repl: Repl =
        fun commandSource display ->
            commandSource () |> Seq.iter (handle display)
