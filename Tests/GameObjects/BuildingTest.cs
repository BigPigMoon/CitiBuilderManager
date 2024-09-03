using Arch.Core;
using CitiBuilderManager.GameObjects;

namespace Tests.Services
{
    public class BuildingTests
    {
        private World _world = World.Create();

        private Building CreateNewBuildingFromMap(bool[,] map)
        {
            return new Building(map, new BuildingKind());
        }

        [Fact]
        public void TestGetClearColumnsNoClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { true, true, true },
                { true, true, true },
                { true, true, true }
            });

            var range = building.GetClearColumns();
            Assert.Equal(0, range.Start);
            Assert.Equal(3, range.End);
        }

        [Fact]
        public void TestGetClearColumnsLeftClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { false, true, true },
                { false, true, true },
                { false, true, true }
            });

            var range = building.GetClearColumns();
            Assert.Equal(1, range.Start);
            Assert.Equal(3, range.End);
        }

        [Fact]
        public void TestGetClearColumnsRightClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { true, true, false },
                { true, true, false },
                { true, true, false }
            });

            var range = building.GetClearColumns();
            Assert.Equal(0, range.Start);
            Assert.Equal(2, range.End);
        }

        [Fact]
        public void TestGetClearColumnsCenterClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { true, false, true },
                { true, false, true },
                { true, false, true }
            });

            var range = building.GetClearColumns();
            Assert.Equal(0, range.Start);
            Assert.Equal(3, range.End);
        }

        [Fact]
        public void TestGetClearColumnsCenterFill()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { false, true, false },
                { false, true, false },
                { false, true, false }
            });

            var range = building.GetClearColumns();
            Assert.Equal(1, range.Start);
            Assert.Equal(2, range.End);
        }

        [Fact]
        public void TestGetClearRowsNoClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { true, true, true },
                { true, true, true },
                { true, true, true }
            });

            var range = building.GetClearRows();
            Assert.Equal(0, range.Start);
            Assert.Equal(3, range.End);
        }

        [Fact]
        public void TestGetClearRowsUpClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { false, false, false },
                { true, true, true },
                { true, true, true }
            });

            var range = building.GetClearRows();
            Assert.Equal(1, range.Start);
            Assert.Equal(3, range.End);
        }

        [Fact]
        public void TestGetClearRowsDownClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { true, true, true },
                { true, true, true },
                { false, false, false }
            });

            var range = building.GetClearRows();
            Assert.Equal(0, range.Start);
            Assert.Equal(2, range.End);
        }

        [Fact]
        public void TestGetClearRowsCenterClear()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { true, true, true },
                { false, false, false },
                { true, true, true }
            });

            var range = building.GetClearRows();
            Assert.Equal(0, range.Start);
            Assert.Equal(3, range.End);
        }

        [Fact]
        public void TestGetClearRowsCenterFill()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { false, false, false },
                { true, true, true },
                { false, false, false }
            });

            var range = building.GetClearRows();
            Assert.Equal(1, range.Start);
            Assert.Equal(2, range.End);
        }

        [Fact]
        public void TestIsBuildingEmptyTrue()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false }
            });

            Assert.True(building.IsEmpty());
        }

        [Fact]
        public void TestIsBuildingEmptyFalse()
        {
            var building = CreateNewBuildingFromMap(new bool[,]
            {
                { false, false, false },
                { false, true, false },
                { false, false, false }
            });

            Assert.False(building.IsEmpty());
        }
    }
}
