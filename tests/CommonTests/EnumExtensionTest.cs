using MS.Common.Extensions;
using Xunit;

namespace CommonTests
{
    public class EnumExtensionTest
    {
        [Fact]
        [Trait("GetEnum", "itemName")]
        public void ShouldGetEnumFromName()
        {
            Assert.Equal(StatusCode.Deleted, "Deleted".GetEnum<StatusCode>());
        }
        [Fact]
        [Trait("GetEnum", "itemValue")]
        public void ShouldGetEnumFromValue()
        {
            Assert.Equal(StatusCode.Disable, 1.GetEnum<StatusCode>());
        }

        [Fact]
        [Trait("GetEnumName", "itemValue")]
        public void ShouldGetEnumNameFromValue()
        {
            Assert.Equal(StatusCode.Enable.ToString(), 0.GetEnumName<StatusCode>());
        }

        [Fact]
        [Trait("GetEnumValue", "itemName")]
        public void ShouldGetEnumValueFromName()
        {
            Assert.Equal((int)StatusCode.Disable, "Disable".GetEnumValue<StatusCode>());
        }

        [Fact]
        [Trait("GetDescription", "Enum")]
        public void ShouldGetDescriptionFromEnum()
        {
            Assert.Equal("已删除", StatusCode.Deleted.GetDescription());
        }
    }
}
