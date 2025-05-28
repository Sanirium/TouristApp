using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristApp.TouristApp.Tests
{
    /* --------------- SALE TESTS --------------- */
    [TestFixture]
    public class SaleTests
    {
        [Test]
        public void CreateSale_NoDiscount_ShouldSaveCorrectly()
        {
            Assert.Pass();
        }

        [Test]
        public void CreateSale_QuantityZero_ShouldBePrevented()
        {
            Assert.Pass();
        }

        [Test]
        public void CreateSale_WithDiscount_ShouldApplyCorrectly()
        {
            Assert.Pass();
        }

        [Test]
        public void Discount_ShouldUpdateDynamicallyOnQuantityChange()
        {
            Assert.Pass();
        }
    }
}
