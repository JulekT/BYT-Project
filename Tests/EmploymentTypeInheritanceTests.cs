using Library;
using NUnit.Framework;

namespace Tests
{
    public class EmploymentTypeFlatteningTests
    {
        [Test]
        public void EmploymentType_CanRepresent_FullTime()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            Assert.AreEqual(
                EmploymentTypeEnum.FullTime,
                employmentType.Type
            );
            Assert.AreEqual(20, employmentType.HourlyRate);
        }

        [Test]
        public void EmploymentType_CanRepresent_PartTime()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.PartTime, 15);

            Assert.AreEqual(
                EmploymentTypeEnum.PartTime,
                employmentType.Type
            );
            Assert.AreEqual(15, employmentType.HourlyRate);
        }

        [Test]
        public void EmploymentType_IsDisjoint_ByEnum()
        {
            var fullTime =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            Assert.AreNotEqual(
                EmploymentTypeEnum.PartTime,
                fullTime.Type
            );
        }
    }
}