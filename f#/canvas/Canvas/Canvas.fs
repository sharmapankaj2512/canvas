namespace Canvas

open Domain

module Canvas =

    let points =
        fun (width: int, height: int) ->
            set [ for x in 0 .. width - 1 do
                      for y in 0 .. height - 1 do
                          Point2D(x, y) ]
