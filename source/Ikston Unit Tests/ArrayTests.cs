﻿using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Ikon.Ston.Values;
using Ikon;
using System;

namespace Ikston_Unit_Tests
{
	[TestClass]
	public class ArrayTests
	{
		[TestMethod]
		public void ArrayType()
		{
			string input = "[ =2 ]";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext();

			Assert.AreEqual(ArrayValue.ValueTypeName, value.TypeName);
		}

		[TestMethod]
		public void ArrayCountEmpty()
		{
			string input = "[]";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as ArrayValue;

			Assert.AreEqual(0, value.GetList.Count);
		}

		[TestMethod]
		public void ArrayCountAllTypes()
		{
			string input = "[ =2 \"dfgfdg\" [] { nothing } ]";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as ArrayValue;

			Assert.AreEqual(4, value.GetList.Count);
		}

		[TestMethod]
		public void ArrayElementType0()
		{
			string input = "[ =2 \"dfgfdg\" [] { nothing } ]";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as ArrayValue;

			Assert.AreEqual(typeof(NumericValue), value.GetList[0].GetType());
		}

		[TestMethod]
		public void ArrayElementType1()
		{
			string input = "[ =2 \"dfgfdg\" [] { nothing } ]";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as ArrayValue;

			Assert.AreEqual(typeof(TextValue), value.GetList[1].GetType());
		}

		[TestMethod]
		public void ArrayElementType2()
		{
			string input = "[ =2 \"dfgfdg\" [] { nothing } ]";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as ArrayValue;

			Assert.AreEqual(typeof(ArrayValue), value.GetList[2].GetType());
		}

		[TestMethod]
		public void ArrayElementType3()
		{
			string input = "[ =2 \"asdfasdf\" [] { nothing } ]";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as ArrayValue;

			Assert.AreEqual(typeof(ObjectValue), value.GetList[3].GetType());
		}

		[TestMethod]
		public void ArrayWriteEmpty()
		{
			StringBuilder output = new StringBuilder();
			IkonWriter writer = new IkonWriter(new StringWriter(output));

			var value = new ArrayValue();
			value.Compose(writer);

			Assert.AreEqual("[" + Environment.NewLine + "]", output.ToString().Trim());
		}

		[TestMethod]
		public void ArrayWriteWithNumerics()
		{
			string expected = "[" + Environment.NewLine +
				"\t=2" + Environment.NewLine +
				"\t=5" + Environment.NewLine +
				"\t=0.5" + Environment.NewLine +
				"]";

			StringBuilder output = new StringBuilder();
			IkonWriter writer = new IkonWriter(new StringWriter(output));

			var value = new ArrayValue();
			value.GetList.Add(new NumericValue(2));
			value.GetList.Add(new NumericValue(5));
			value.GetList.Add(new NumericValue(0.5));
			value.Compose(writer);

			Assert.AreEqual(expected, output.ToString().Trim());
		}

		[TestMethod]
		public void ArrayWriteWithTexts()
		{
			string expected = "[" + Environment.NewLine +
				"\t\"abc\"" + Environment.NewLine +
				"\t\"asdf\"" + Environment.NewLine +
				"]";

			StringBuilder output = new StringBuilder();
			IkonWriter writer = new IkonWriter(new StringWriter(output));

			var value = new ArrayValue();
			value.GetList.Add(new TextValue("abc"));
			value.GetList.Add(new TextValue("asdf"));
			value.Compose(writer);

			Assert.AreEqual(expected, output.ToString().Trim());
		}

		[TestMethod]
		public void ArrayWriteNestedEmptyArray()
		{
			string expected = "[" + Environment.NewLine +
				"\t[" + Environment.NewLine +
				"\t]" + Environment.NewLine +
				"]";

			StringBuilder output = new StringBuilder();
			IkonWriter writer = new IkonWriter(new StringWriter(output));

			var value = new ArrayValue();
			value.GetList.Add(new ArrayValue());
			value.Compose(writer);

			Assert.AreEqual(expected, output.ToString().Trim());
		}

		[TestMethod]
		public void ArrayWriteNestedMixedArray()
		{
			string expected = "[" + Environment.NewLine +
				"\t[" + Environment.NewLine +
				"\t\t=2.5" + Environment.NewLine +
				"\t\t[" + Environment.NewLine +
				"\t\t\t=-0.4" + Environment.NewLine +
				"\t\t]" + Environment.NewLine +
				"\t\t[" + Environment.NewLine +
				"\t\t]" + Environment.NewLine +
				"\t\t\"foo\"" + Environment.NewLine +
				"\t]" + Environment.NewLine +
				"\t\"bar\"" + Environment.NewLine +
				"]";

			StringBuilder output = new StringBuilder();
			IkonWriter writer = new IkonWriter(new StringWriter(output));

			var doubleNestedValue = new ArrayValue();
			doubleNestedValue.GetList.Add(new NumericValue(-0.4));

			var nestedValue = new ArrayValue();
			nestedValue.GetList.Add(new NumericValue(2.5));
			nestedValue.GetList.Add(doubleNestedValue);
			nestedValue.GetList.Add(new ArrayValue());
			nestedValue.GetList.Add(new TextValue("foo"));
			
			var value = new ArrayValue();
			value.GetList.Add(nestedValue);
			value.GetList.Add(new TextValue("bar"));
			
			value.Compose(writer);

			Assert.AreEqual(expected, output.ToString().Trim());
		}
	}
}
