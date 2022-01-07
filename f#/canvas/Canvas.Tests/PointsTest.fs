module Canvas.Tests.PointsTest

open Canvas.Tests.ReplTest
open NUnit.Framework

[<Test>]
let ZeroByZero() =
    Assert.AreEqual(Set.empty, points(0, 0))
    
[<Test>]
let OneByOne() =
    Assert.AreEqual(set [Point2D(0, 0)], points(1, 1))
    
[<Test>]
let OneByTwo() =
    Assert.AreEqual(set [Point2D(0, 0); Point2D(0, 1)], points(1, 2))
    
[<Test>]
let TwoByOne() =
    Assert.AreEqual(set [Point2D(0, 0); Point2D(1, 0)], points(2, 1))