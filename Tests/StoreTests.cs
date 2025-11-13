using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class StoreTests
    {
        private readonly Store filledStore = new("Biedronka", "Mila", "Warszawa", "02-445", "Poland");
        private readonly Store emptyStore = new();

        [Test] 
        public void StoreNameAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyStore.Name = "");
            Assert.That(ex.Message, Is.EqualTo("Store name cannot be empty"));
        }

        [Test]
        public void StoreNameGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyStore.Name));
            Assert.That(ex.Message, Is.EqualTo("Store name is empty, you need to assign it first"));
        }

        [Test]
        public void StorStreetAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyStore.Street = "");
            Assert.That(ex.Message, Is.EqualTo("Street cannot be empty"));
        }

        [Test]
        public void StoreStreetGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyStore.Street));
            Assert.That(ex.Message, Is.EqualTo("Store street is empty, you need to assign it first"));
        }

        [Test]
        public void StoreCityAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyStore.City = "");
            Assert.That(ex.Message, Is.EqualTo("City cannot be empty"));
        }

        [Test]
        public void StoreCityGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyStore.City));
            Assert.That(ex.Message, Is.EqualTo("Store city is empty, you need to assign it first"));
        }

        [Test]
        public void StorePostalCodeAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyStore.PostalCode = "");
            Assert.That(ex.Message, Is.EqualTo("Postal code cannot be empty"));
        }

        [Test]
        public void StorePostalCodeGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyStore.PostalCode));
            Assert.That(ex.Message, Is.EqualTo("Store postal code is empty, you need to assign it first"));
        }

        [Test]
        public void StoreCountryAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyStore.Country = "");
            Assert.That(ex.Message, Is.EqualTo("Country cannot be empty"));
        }

        [Test]
        public void StoreCountryGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyStore.Country));
            Assert.That(ex.Message, Is.EqualTo("Store country is empty, you need to assign it first"));
        }

        [Test]
        public void AddNullStoreToExtentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Store.AddStoreToExtent(null));
            Assert.That(ex.Message, Is.EqualTo("Store cannot be null"));
        }

        [Test]
        public void AddStoreToExtentSuccess()
        {
            int oldCount = Store.Extent.Count;
            Store.AddStoreToExtent(filledStore);
            int newCount = Store.Extent.Count;
            Assert.That(() => oldCount + 1 == newCount);
        }

        [Test]
        public void SaveStoreExtentFileCreation()
        {
            Store.SaveExtent();
            Assert.That(File.Exists("store_extent.json"), Is.True, "File wasn't created properly");
        }

        [Test]
        public void LoadStoreExtentFromFile()
        {
            Store storeToSafe = new("Lidl", "Lubelska", "Gdansk", "80-654", "Poland");
            Store.AddStoreToExtent(storeToSafe);
            Store.SaveExtent();
            Store.Extent.Clear();
            Store.LoadExtent();
            Assert.That(Store.Extent.Count > 0, Is.True, "Loading didn't work");
        }
    }
}
