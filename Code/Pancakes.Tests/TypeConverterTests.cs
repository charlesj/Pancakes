namespace Pancakes.Tests
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	using Xunit;
	using Xunit.Extensions;

	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
	public class TypeConverterTests
	{
		private readonly ITypeConverter typeConverter;

		public TypeConverterTests()
		{
			this.typeConverter = new TypeConverter();
		}

		[Theory]
		[InlineData("1", typeof(int))]
		[InlineData("B6A7EAF2-08A1-4DD3-BD19-DB95BFA59EDF", typeof(Guid))]
		[InlineData("true", typeof(bool))]
		public void CanConvertWithoutThrowing(object source, Type target)
		{
			Assert.DoesNotThrow(() => this.typeConverter.Convert(source, target));
		}
	}
}