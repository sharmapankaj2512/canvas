module Canvas.Tests.PointsTest

open Canvas.Tests.ReplTest
open NUnit.Framework

[<Test>]
let ZeroByZero() =
    Assert.AreEqual(Set.empty, points(0, 0))
    
[<Test>]
let OneByOneCanvas() =
    Assert.AreEqual(set [Point2D(0, 0)], points(1, 1))
    
[<Test>]
let OneByTwoCanvas() =
    Assert.AreEqual(set [Point2D(0, 0), Point2D(0, 1)], points(1, 2))