namespace Canvas.Tests

open Canvas
open NUnit.Framework

module Mocks =
    
    type MockDisplay(expectedPoints: Set<Point>, expectedMessage: string) =
        interface Display with
            member this.Display(points: Set<Point>) = Assert.AreEqual(expectedPoints, points)

            member this.Display(message: string) =
                Assert.AreEqual(expectedMessage, message)

