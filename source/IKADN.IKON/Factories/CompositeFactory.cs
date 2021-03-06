﻿using System;
using Ikadn.Ikon.Types;

namespace Ikadn.Ikon.Factories
{
	/// <summary>
	/// IKADN object factory for composite IKON objects.
	/// </summary>
	public class CompositeFactory : IIkadnObjectFactory
	{
		/// <summary>
		/// Sign for IKADN composite object.
		/// </summary>
		public const char OpeningSign = '{';

		/// <summary>
		/// Closing character for IKON composite object in textual
		/// representation.
		/// </summary>
		public const char ClosingChar = '}';

		/// <summary>
		/// Sign for IKADN composite object.
		/// </summary>
		public char Sign
		{
			get { return OpeningSign; }
		}

		/// <summary>
		/// Parses input for a IKADN object.
		/// </summary>
		/// <param name="parser">IKADN parser instance.</param>
		/// <returns>IKADN value generated by factory.</returns>
		public IkadnBaseObject Parse(Ikadn.IkadnParser parser)
		{
			if (parser == null)
				throw new System.ArgumentNullException("parser");

			IkonComposite res = new IkonComposite(Ikadn.Ikon.IkonParser.ReadIdentifier(parser.Reader));

			while (parser.Reader.PeekNextNonwhite() != ClosingChar)
			{
				string memberName = Ikadn.Ikon.IkonParser.ReadIdentifier(parser.Reader);
				
				string startPosition = parser.Reader.PositionDescription;
				if (parser.HasNext())
					res[memberName] = parser.ParseNext();
				else
					throw new FormatException("Characters from " + startPosition + " to " + parser.Reader.PositionDescription + " couldn't be parsed as IKADN value");
			}
			parser.Reader.Read();

			return res;
		}
	}
}
