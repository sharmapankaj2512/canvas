module Canvas.Tests.PointsTest

open NUnit.Framework

[<Test>]
let ZeroByZero() =
    Assert.AreEqual(Set.empty, ReplTest.points(0, 0))