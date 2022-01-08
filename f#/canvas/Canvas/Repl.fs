namespace Canvas

open Canvas

module Repl =

    let repl: Repl =
        fun commandSource display ->
            match commandSource () with
            | CreateCanvas (w, h) -> fun () -> display.Display(points (w, h))
            | Quit -> fun () -> display.Display("Good bye!")
