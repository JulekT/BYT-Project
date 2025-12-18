using Library;
using NUnit.Framework;

namespace Tests
{
    public class EmploymentTypeInheritanceTests
    {
        [Test]
        public void EmploymentType_CanReference_FullTimeEmployment()
        {
            EmploymentType employmentType = new FullTimeEmployment(20);

            Assert.IsInstanceOf<FullTimeEmployment>(employmentType);
            Assert.AreEqual("FullTime", employmentType.Name);
            Assert.AreEqual(20, employmentType.HourlyRate);
        }

        [Test]
        public void EmploymentType_CanReference_PartTimeEmployment()
        {
            EmploymentType employmentType = new PartTimeEmployment(15);

            Assert.IsInstanceOf<PartTimeEmployment>(employmentType);
            Assert.AreEqual("PartTime", employmentType.Name);
            Assert.AreEqual(15, employmentType.HourlyRate);
        }

        [Test]
        public void EmploymentType_IsDisjoint()
        {
            EmploymentType fullTime = new FullTimeEmployment(20);

            Assert.IsFalse(fullTime is PartTimeEmployment);
        }
    }
}