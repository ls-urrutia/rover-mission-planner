using Domain.Entities;
using Xunit;

namespace Domain.Tests
{
    public class RoverTaskTests
    {
        [Fact]
        public void IsOverlapping_ReturnsTrue_WhenTasksOverlap()
        {
            var t1 = new RoverTask
            {
                Id = Guid.NewGuid(),
                RoverName = "Curiosity",
                TaskType = TaskType.Drill,
                Latitude = 0,
                Longitude = 0,
                StartsAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                DurationMinutes = 60,
                Status = Status.Planned
            };
            var t2 = new RoverTask
            {
                Id = Guid.NewGuid(),
                RoverName = "Curiosity",
                TaskType = TaskType.Sample,
                Latitude = 0,
                Longitude = 0,
                StartsAt = t1.StartsAt.AddMinutes(30),
                DurationMinutes = 60,
                Status = Status.Planned
            };
            Assert.True(t1.IsOverlapping(t2));
        }

        [Fact]
        public void IsOverlapping_ReturnsFalse_WhenTasksDoNotOverlap()
        {
            var t1 = new RoverTask
            {
                Id = Guid.NewGuid(),
                RoverName = "Curiosity",
                TaskType = TaskType.Drill,
                Latitude = 0,
                Longitude = 0,
                StartsAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                DurationMinutes = 60,
                Status = Status.Planned
            };
            var t2 = new RoverTask
            {
                Id = Guid.NewGuid(),
                RoverName = "Curiosity",
                TaskType = TaskType.Sample,
                Latitude = 0,
                Longitude = 0,
                StartsAt = t1.StartsAt.AddMinutes(60), // Empieza justo cuando termina t1
                DurationMinutes = 30,
                Status = Status.Planned
            };
            Assert.False(t1.IsOverlapping(t2));
        }
    }
}