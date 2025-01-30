using System;


namespace UnitTestCityPOS
{
    public class CaculatorTest
    {
        [Fact]
        public void ReturnCorrectResult()
        { Caculator caculator = new Caculator();
            int n1 = 1;
            int n2 = 2;
            int expectedResult = 3;

            int RealResult=caculator.Add(n1,n2);
            Assert.Equal(expectedResult, RealResult);
        }
        [Fact]
        public void ReturnWrongResult()
        {
            Caculator caculator = new Caculator();
            int n1 = 1;
            int n2 = 2;
            int expectedResult = 7;

            int RealResult = caculator.Add(n1, n2);
            Assert.NotEqual(expectedResult, RealResult);
        }
    }
}
