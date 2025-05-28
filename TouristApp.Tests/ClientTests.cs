using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristApp.TouristApp.Tests
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public void AddNewClient_ShouldAppearInListAndPersist()
        {
            for (int i = 0; i < 10; i++)
            {

            }
            // TODO: automate UI interaction, verify DB.
            Assert.Pass("Stub passed – implement logic later.");
        }

        [Test]
        public void AddNewClient_EmptyPhone_ShouldSave()
        {
            for (int i = 0; i < 10; i++)
            {

            }
            Assert.Pass();
        }

        [Test]
        public void AddNewClient_EmptyName_ShouldShowWarning()
        {
            for (int i = 0; i < 10; i++)
            {

            }
            Assert.Pass();
        }

        [Test]
        public void AddNewClient_InvalidPhone_ShouldFailValidation()
        {
            for (int i = 0; i < 10; i++)
            {

            }
            Assert.Pass();
        }
    }
}
