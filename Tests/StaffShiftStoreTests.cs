using Library;
using NUnit.Framework;
using System;

namespace Tests
{
    public class StaffShiftStoreTests
    {
        [Test]
        public void Staff_AssignedToCorrectStore_CanJoinShift()
        {
            var store = new Store("StoreA", "Street", "City", "00000", "Country");
            var shift = new Shift(DateTime.Today, DateTime.Now, DateTime.Now.AddHours(5));
            store.AddShift(shift);

            var staff = new SalesPerson("Mert", DateTime.Now, 3000, 0.1);
            staff.AssignStore(store);

            staff.AssignToShift(shift);

            Assert.Contains(shift, (System.Collections.ICollection)staff.Shifts);
            Assert.Contains(staff, (System.Collections.ICollection)shift.Staff);
        }

        [Test]
        public void Staff_CannotJoinShift_OfAnotherStore()
        {
            var store1 = new Store("StoreA", "Street", "City", "00000", "Country");
            var store2 = new Store("StoreB", "Street", "City", "00000", "Country");

            var shiftOtherStore = new Shift(DateTime.Today, DateTime.Now, DateTime.Now.AddHours(5));
            store2.AddShift(shiftOtherStore);

            var staff = new SalesPerson("Deniz", DateTime.Now, 3000, 0.1);
            staff.AssignStore(store1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                staff.AssignToShift(shiftOtherStore);
            });
        }

        [Test]
        public void Shift_AddStaff_ShouldThrow_IfStaffFromAnotherStore()
        {
            var store1 = new Store("StoreA", "Street", "City", "00000", "Country");
            var store2 = new Store("StoreB", "Street", "City", "00000", "Country");

            var shift = new Shift(DateTime.Today, DateTime.Now, DateTime.Now.AddHours(5));
            store2.AddShift(shift);

            var staff = new SalesPerson("Ayşe", DateTime.Now, 3500, 0.2);
            staff.AssignStore(store1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shift.AddStaff(staff);
            });
        }
    }
}
