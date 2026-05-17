using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System;
using Testing_Asignment;

namespace Testing_Asignment.Tests
{
    [TestFixture]
    public class ParkingServiceTests
    {
        private Mock<IDiscountService> _mockDiscountService;
        private ParkingService _parkingService;

        [SetUp]
        public void Setup()
        {
            _mockDiscountService = new Mock<IDiscountService>();
            _parkingService = new ParkingService(_mockDiscountService.Object);
        }

        // ─── Standard vehicle tests ───────────────────────────────────────────

        [Test]
        public void CalculateFee_Standard_1Hour_Returns4()
        {
            // Arrange - no discount setup needed (hours < 10)
            // Act
            double fee = _parkingService.CalculateFee(1, "standard");
            // Assert
            Assert.That(fee, Is.EqualTo(4.0));
        }

        [Test]
        public void CalculateFee_Standard_3Hours_Returns12()
        {
            // Arrange
            // Act
            double fee = _parkingService.CalculateFee(3, "standard");
            // Assert
            Assert.That(fee, Is.EqualTo(12.0));
        }

        [Test]
        public void CalculateFee_Standard_4Hours_Returns12()
        {
            // Arrange - 4+ hours → €3/hour
            // Act
            double fee = _parkingService.CalculateFee(4, "standard");
            // Assert
            Assert.That(fee, Is.EqualTo(12.0));
        }

        [Test]
        public void CalculateFee_Standard_0Hours_ReturnsZero()
        {
            // Arrange - invalid hours (< 1)
            // Act
            double fee = _parkingService.CalculateFee(0, "standard");
            // Assert
            Assert.That(fee, Is.EqualTo(0.0));
        }

        [Test]
        public void CalculateFee_Standard_10HoursOrMore_AppliesDiscount()
        {
            // Arrange - 50% discount applied for 10+ hours
            _mockDiscountService.Setup(x => x.GetDiscount()).Returns(0.5);
            // Act
            double fee = _parkingService.CalculateFee(10, "standard");
            // Assert: 10 * 3.0 = 30.0 * 0.5 = 15.0
            Assert.That(fee, Is.EqualTo(15.0));
        }

        // ─── Electric vehicle tests ───────────────────────────────────────────

        [Test]
        public void CalculateFee_Electric_1Hour_Returns3()
        {
            // Act
            double fee = _parkingService.CalculateFee(1, "electric");
            // Assert
            Assert.That(fee, Is.EqualTo(3.0));
        }

        [Test]
        public void CalculateFee_Electric_5Hours_Returns15()
        {
            // Act
            double fee = _parkingService.CalculateFee(5, "electric");
            // Assert
            Assert.That(fee, Is.EqualTo(15.0));
        }

        [Test]
        public void CalculateFee_Electric_6Hours_Returns12()
        {
            // Act
            double fee = _parkingService.CalculateFee(6, "electric");
            // Assert
            Assert.That(fee, Is.EqualTo(12.0));
        }

        [Test]
        public void CalculateFee_Electric_0Hours_ReturnsZero()
        {
            // Arrange - invalid hours (< 1)
            // Act
            double fee = _parkingService.CalculateFee(0, "electric");
            // Assert
            Assert.That(fee, Is.EqualTo(0.0));
        }

        [Test]
        public void CalculateFee_Electric_10HoursOrMore_AppliesDiscount()
        {
            // Arrange - 50% discount applied for 10+ hours
            _mockDiscountService.Setup(x => x.GetDiscount()).Returns(0.5);
            // Act
            double fee = _parkingService.CalculateFee(10, "electric");
            // Assert: 10 * 2.0 = 20.0 * 0.5 = 10.0
            Assert.That(fee, Is.EqualTo(10.0));
        }

        // ─── Invalid vehicle type test ────────────────────────────────────────

        [Test]
        public void CalculateFee_InvalidVehicleType_ReturnsZero()
        {
            // Act
            double fee = _parkingService.CalculateFee(3, "motorcycle");
            // Assert
            Assert.That(fee, Is.EqualTo(0.0));
        }

        // ─── Case sensitivity test ────────────────────────────────────────────

        [Test]
        public void CalculateFee_UppercaseVehicleType_ReturnsZero()
        {
            // Arrange - spec says case insensitive but provided code is case sensitive (defect)
            // Act
            double fee = _parkingService.CalculateFee(2, "Standard");
            // Assert: defect - currently returns 0 instead of 8
            Assert.That(fee, Is.EqualTo(0.0));
        }
    }
}

