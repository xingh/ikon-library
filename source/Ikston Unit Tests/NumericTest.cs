﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ikon;
using System.IO;
using Ikon.Ston.Values;
using System.Globalization;

namespace Ikston_Unit_Tests
{
	[TestClass]
	public class NumericTest
	{
		[TestMethod]
		public void NumericType()
		{
			string input = "=2";

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext();

			Assert.AreEqual(NumericValue.ValueTypeName, value.TypeName);
		}

		[TestMethod]
		public void NumericReadValueShort()
		{
			short expectedValue = 2;
			string input = "=" + expectedValue;

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<short>());
		}

		[TestMethod]
		public void NumericReadValueInt()
		{
			int expectedValue = 1234567;
			string input = "=" + expectedValue;

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<int>());
		}

		[TestMethod]
		public void NumericReadValueIntNegative()
		{
			int expectedValue = -1234567;
			string input = "=" + expectedValue;

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<int>());
		}

		[TestMethod]
		public void NumericReadValueLong()
		{
			long expectedValue = 123456789012L;
			string input = "=" + expectedValue;

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<long>());
		}

		[TestMethod]
		public void NumericReadValueDecimal()
		{
			decimal expectedValue = 1.23456789012m;
			string input = "=" + expectedValue.ToString(NumberFormatInfo.InvariantInfo);

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<decimal>());
		}

		[TestMethod]
		public void NumericReadValueFloat()
		{
			float expectedValue = 1.2345f;
			string input = "=" + expectedValue.ToString(NumberFormatInfo.InvariantInfo);

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<float>());
		}

		[TestMethod]
		public void NumericReadValueDouble()
		{
			double expectedValue = 1.23456789;
			string input = "=" + expectedValue.ToString(NumberFormatInfo.InvariantInfo);

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<double>());
		}

		[TestMethod]
		public void NumericReadValueDoubleNegative()
		{
			double expectedValue = -1.23456789;
			string input = "=" + expectedValue.ToString(NumberFormatInfo.InvariantInfo);

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<double>());
		}

		[TestMethod]
		public void NumericReadValueDoubleInfinity()
		{
			double expectedValue = double.PositiveInfinity;
			string input = "=" + NumericValue.PositiveInfinity;

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<double>());
		}

		[TestMethod]
		public void NumericReadValueDoubleNegativeInfinity()
		{
			double expectedValue = double.NegativeInfinity;
			string input = "=" + NumericValue.NegativeInfinity;

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<double>());
		}

		[TestMethod]
		public void NumericReadValueDoubleNaN()
		{
			double expectedValue = double.NaN;
			string input = "=" + expectedValue.ToString(NumberFormatInfo.InvariantInfo);

			Parser parser = new Ikon.Ston.Parser(new StringReader(input));
			var value = parser.ParseNext() as NumericValue;

			Assert.AreEqual(expectedValue, value.As<double>());
		}

		[TestMethod]
		public void NumericWriteValueLong()
		{
			long rawValue = 1234567890L;
			string ikonData = "=" + rawValue;
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueLongNegative()
		{
			long rawValue = -1234567890L;
			string ikonData = "=" + rawValue;
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueDouble()
		{
			double rawValue = 1.234567890;
			string ikonData = "=" + rawValue.ToString(NumberFormatInfo.InvariantInfo);
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueDoubleNegative()
		{
			double rawValue = -1.234567890;
			string ikonData = "=" + rawValue.ToString(NumberFormatInfo.InvariantInfo);
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueDoubleInfinity()
		{
			double rawValue = double.PositiveInfinity;
			string ikonData = "=" + rawValue.ToString(NumberFormatInfo.InvariantInfo);
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueDoubleNegativeInfinity()
		{
			double rawValue = double.NegativeInfinity;
			string ikonData = "=" + rawValue.ToString(NumberFormatInfo.InvariantInfo);
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueDoubleNaN()
		{
			double rawValue = double.NaN;
			string ikonData = "=" + rawValue.ToString(NumberFormatInfo.InvariantInfo);
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueDecimal()
		{
			decimal rawValue = 1.234567890m;
			string ikonData = "=" + rawValue.ToString(NumberFormatInfo.InvariantInfo);
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}

		[TestMethod]
		public void NumericWriteValueDecimalNegative()
		{
			decimal rawValue = -1.234567890m;
			string ikonData = "=" + rawValue.ToString(NumberFormatInfo.InvariantInfo);
			StringBuilder output = new StringBuilder();

			IkonWriter writer = new IkonWriter(new StringWriter(output));
			var value = new NumericValue(rawValue);
			value.Compose(writer);

			Assert.AreEqual(ikonData, output.ToString().Trim());
		}
	}
}