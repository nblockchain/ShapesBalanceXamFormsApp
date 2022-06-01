


using ShapesBalanceXamFormsApp;
using Xamarin.Forms;

namespace TestCircle
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //99 and 1 => 98 and 2

        public void TestWalletReturnsSameNumberOfElements()
        {

            //arrange
            var main = new MainPage();

            

            var wallets = new List<MainPage.Wallet>()
            {
                new MainPage.Wallet(99, Brush.Blue),
                new MainPage.Wallet(1, Brush.Brown)

            };
            var normalizedWallets = main.Normalize(wallets);
            //act 

            //assert
            Assert.AreEqual(normalizedWallets.Count(), wallets.Count()) ;

        }

        
    }
}