using MS.Common.Extensions;
using Xunit;

namespace CommonTests
{
    public class EnumExtensionTest
    {
        [Fact]
        [Trait("GetEnum", "itemName")]
        public void GetEnum_EnumName_ReturnCorrespondEnum()
        {
            //Arrange
            StatusCode statusCode = StatusCode.Deleted;

            //Act
            string actual = statusCode.ToString();

            //Assert
            Assert.Equal(statusCode, actual.GetEnum<StatusCode>());
        }
        [Fact]
        [Trait("GetEnum", "itemValue")]
        public void GetEnum_EnumValue_ReturnCorrespondEnum()
        {
            //Arrange
            StatusCode statusCode = StatusCode.Disable;

            //Act
            int actual = statusCode.GetHashCode();

            //Assert
            Assert.Equal(statusCode, actual.GetEnum<StatusCode>());
        }

        [Fact]
        [Trait("GetEnumName", "itemValue")]
        public void GetEnumName_EnumValue_ReturnCorrespondEnumName()
        {
            //Arrange
            StatusCode statusCode = StatusCode.Enable;

            //Act
            int actual = statusCode.GetHashCode();

            //Assert
            Assert.Equal(statusCode.ToString(), actual.GetEnumName<StatusCode>());
        }

        [Fact]
        [Trait("GetEnumValue", "itemName")]
        public void GetEnumValue_EnumName_ReturnCorrespondEnumValue()
        {
            //Arrange
            StatusCode statusCode = StatusCode.Disable;

            //Act
            string actual = statusCode.ToString();

            //Assert
            Assert.Equal(statusCode.GetHashCode(), actual.GetEnumValue<StatusCode>());
        }

        [Fact]
        [Trait("GetDescription", "Enum")]
        public void GetDescription_Enum_ReturnCorrespondEnumDescription()
        {
            //Arrange
            StatusCode statusCode = StatusCode.Deleted;

            //Assert
            Assert.Equal("已删除", statusCode.GetDescription());
        }
    }
}