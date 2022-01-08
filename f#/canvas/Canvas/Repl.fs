namespace Canvas

open Canvas

module Repl =

    let handle (command: Command, display: Display) =
        match command with
        | CreateCanvas (w, h) -> fun () -> display.Display(points (w, h))
        | Quit -> fun () -> display.Display("Good bye!")

    let repl: Repl =
        fun commandSource display ->
            fun () ->
                for command in commandSource () do
                    handle (command, display) ()
