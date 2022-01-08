namespace Canvas

open Canvas

module Repl =
    let repl: Repl =
        fun commandSource display ->            
                commandSource ()
                |> Seq.iter
                    (fun command ->
                        match command with
                        | CreateCanvas (w, h) -> display.Display(points (w, h))
                        | Quit -> display.Display("Good bye!"))
